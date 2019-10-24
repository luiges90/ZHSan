using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WorldOfTheThreeKingdomsEditor
{
    /// <summary>
    /// Window1.xaml 的互動邏輯
    /// </summary>
    public partial class NewEventWindow : Window
    {
        private GameScenario scen;
        private bool edit = false;

        private Event e;

        public NewEventWindow(bool editing, DataGrid dataGrid, GameScenario scen)
        {
            this.scen = scen;
            edit = editing;
            InitializeComponent();

            e = new Event();

            if (editing)
            {
                e = scen.AllEvents.GetGameObject(int.Parse(((DataRowView)dataGrid.SelectedItem).Row["ID"].ToString())) as Event;
            }

            txname.Text = e.Name;
            cbHappened.IsChecked = e.happened;
            cbRepeatable.IsChecked = e.repeatable;
            cbMinor.IsChecked = e.Minor;
            cbGloballyDisplayed.IsChecked = e.GloballyDisplayed;
            tbHappenChance.Text = e.happenChance.ToString();
            tbStartYear.Text = e.StartYear.ToString();
            tbStartMonth.Text = e.StartMonth.ToString();
            tbEndYear.Text = e.EndYear.ToString();
            tbEndMonth.Text = e.EndMonth.ToString();
        }

        private void BtSave_Click(object sender, RoutedEventArgs rea)
        {
            e.Name = txname.Text;
            e.happened = cbHappened.IsChecked.GetValueOrDefault(false);
            e.repeatable = cbRepeatable.IsChecked.GetValueOrDefault(false);
            e.Minor = cbMinor.IsChecked.GetValueOrDefault(false);
            e.GloballyDisplayed = cbGloballyDisplayed.IsChecked.GetValueOrDefault(false);
            int.TryParse(tbHappenChance.Text, out e.happenChance);
            int.TryParse(tbStartYear.Text, out e.StartYear);
            int.TryParse(tbStartMonth.Text, out e.StartMonth);
            int.TryParse(tbEndYear.Text, out e.EndYear);
            int.TryParse(tbEndMonth.Text, out e.EndMonth);

            if (!edit)
            {
                scen.AllEvents.Add(e);
            }
            this.Close();
        }

        private void BtExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
