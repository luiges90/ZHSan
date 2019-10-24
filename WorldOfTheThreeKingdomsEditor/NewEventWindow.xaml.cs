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
            tbImage.Text = e.Image;
            tbSound.Text = e.Sound;

            PopulatePersonData(0, lblPerson0, lblPersonCond0, lblEffect0, lblBiography0, lblYesEffect0, lblNoEffect0);
            PopulatePersonData(1, lblPerson1, lblPersonCond1, lblEffect1, lblBiography1, lblYesEffect1, lblNoEffect1);
            PopulatePersonData(2, lblPerson2, lblPersonCond2, lblEffect2, lblBiography2, lblYesEffect2, lblNoEffect2);
            PopulatePersonData(3, lblPerson3, lblPersonCond3, lblEffect3, lblBiography3, lblYesEffect3, lblNoEffect3);
            PopulatePersonData(4, lblPerson4, lblPersonCond4, lblEffect4, lblBiography4, lblYesEffect4, lblNoEffect4);
            PopulatePersonData(5, lblPerson5, lblPersonCond5, lblEffect5, lblBiography5, lblYesEffect5, lblNoEffect5);

            lblArchitcture.Content = String.Join(" ", e.architecture.GameObjects.Select(p => p.Name));
            lblArchitctureCond.Content = String.Join(" ", e.architectureCond.Select(p => p.Name));
            lblArchitctureEffect.Content = String.Join(" ", e.architectureEffect.Select(p => p.Name));
            lblArchitctureYesEffect.Content = String.Join(" ", e.yesArchitectureEffect.Select(p => p.Name));
            lblArchitctureNoEffect.Content = String.Join(" ", e.noArchitectureEffect.Select(p => p.Name));
            lblFaction.Content = String.Join(" ", e.faction.GameObjects.Select(p => p.Name));
            lblFactionCond.Content = String.Join(" ", e.factionCond.Select(p => p.Name));
            lblFactionEffect.Content = String.Join(" ", e.factionEffect.Select(p => p.Name));
        }

        private void PopulatePersonData(int index, Label person, Label personCond, Label effect, Label biography, Label yesEffect, Label noEffect)
        {
            if (e.person.ContainsKey(index))
            {
                person.Content = String.Join(" ", e.person[index].Select(p => p == null ? "任何" : p.Name));
            }
            if (e.personCond.ContainsKey(index))
            {
                personCond.Content = String.Join(" ", e.personCond[index].Select(c => c.Name));
            }
            if (e.effect.ContainsKey(index))
            {
                effect.Content = String.Join(" ", e.effect[index].Select(e => e.Name));
            }
            if (e.scenBiography.Count > index)
            {
                biography.Content = e.scenBiography[index];
            }
            if (e.yesEffect.ContainsKey(index))
            {
                yesEffect.Content = e.yesEffect[index];
            }
            if (e.noEffect.ContainsKey(index))
            {
                noEffect.Content = e.noEffect[index];
            }
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
            e.Image = tbImage.Text;
            e.Sound = tbSound.Text;

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
