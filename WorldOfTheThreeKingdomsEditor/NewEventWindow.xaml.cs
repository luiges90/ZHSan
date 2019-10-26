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

        private Event ev;

        private List<PersonIdDialog> tempDialog;
        private List<PersonIdDialog> tempYesDialog;
        private List<PersonIdDialog> tempNoDialog;

        public NewEventWindow(bool editing, DataGrid dataGrid, GameScenario scen)
        {
            this.scen = scen;
            edit = editing;
            InitializeComponent();

            ev = new Event();

            if (editing)
            {
                ev = scen.AllEvents.GetGameObject(int.Parse(((DataRowView)dataGrid.SelectedItem).Row["ID"].ToString())) as Event;
            }

            txname.Text = ev.Name;
            cbHappened.IsChecked = ev.happened;
            cbRepeatable.IsChecked = ev.repeatable;
            cbMinor.IsChecked = ev.Minor;
            cbGloballyDisplayed.IsChecked = ev.GloballyDisplayed;
            tbHappenChance.Text = ev.happenChance.ToString();
            tbStartYear.Text = ev.StartYear.ToString();
            tbStartMonth.Text = ev.StartMonth.ToString();
            tbEndYear.Text = ev.EndYear.ToString();
            tbEndMonth.Text = ev.EndMonth.ToString();
            tbImage.Text = ev.Image;
            tbSound.Text = ev.Sound;

            PopulatePersonData(0, lblPerson0, lblPersonCond0, lblEffect0, lblBiography0, lblYesEffect0, lblNoEffect0);
            PopulatePersonData(1, lblPerson1, lblPersonCond1, lblEffect1, lblBiography1, lblYesEffect1, lblNoEffect1);
            PopulatePersonData(2, lblPerson2, lblPersonCond2, lblEffect2, lblBiography2, lblYesEffect2, lblNoEffect2);
            PopulatePersonData(3, lblPerson3, lblPersonCond3, lblEffect3, lblBiography3, lblYesEffect3, lblNoEffect3);
            PopulatePersonData(4, lblPerson4, lblPersonCond4, lblEffect4, lblBiography4, lblYesEffect4, lblNoEffect4);
            PopulatePersonData(5, lblPerson5, lblPersonCond5, lblEffect5, lblBiography5, lblYesEffect5, lblNoEffect5);

            lblArchitcture.Content = String.Join(" ", ev.architecture.GameObjects.Select(p => p.Name));
            lblArchitctureCond.Content = String.Join(" ", ev.architectureCond.Select(p => p.Name));
            lblArchitctureEffect.Content = String.Join(" ", ev.architectureEffect.Select(p => p.Name));
            lblArchitctureYesEffect.Content = String.Join(" ", ev.yesArchitectureEffect.Select(p => p.Name));
            lblArchitctureNoEffect.Content = String.Join(" ", ev.noArchitectureEffect.Select(p => p.Name));
            lblFaction.Content = String.Join(" ", ev.faction.GameObjects.Select(p => p.Name));
            lblFactionCond.Content = String.Join(" ", ev.factionCond.Select(p => p.Name));
            lblFactionEffect.Content = String.Join(" ", ev.factionEffect.Select(p => p.Name));

            List<string> events = new List<string>();
            events.Add("-1 无");
            events.AddRange(scen.AllEvents.GameObjects.Select(e => e.ID + " " + e.Name));
            cbAfterEventHappened.ItemsSource = events;
            GameObject currentAfterHappened = scen.AllEvents.GetGameObject(ev.AfterEventHappened);
            cbAfterEventHappened.SelectedItem = ev.AfterEventHappened + " " + (currentAfterHappened == null ? "无" : currentAfterHappened.Name);
        }

        private void PopulatePersonData(int index, Label person, Label personCond, Label effect, Label biography, Label yesEffect, Label noEffect)
        {
            if (ev.person.ContainsKey(index))
            {
                person.Content = String.Join(" ", ev.person[index].Select(p => p == null ? "任何" : p.Name));
            }
            if (ev.personCond.ContainsKey(index))
            {
                personCond.Content = String.Join(" ", ev.personCond[index].Select(c => c.Name));
            }
            if (ev.effect.ContainsKey(index))
            {
                effect.Content = String.Join(" ", ev.effect[index].Select(e => e.Name));
            }
            if (ev.scenBiography.Count > index)
            {
                biography.Content = ev.scenBiography[index];
            }
            if (ev.yesEffect.ContainsKey(index))
            {
                yesEffect.Content = ev.yesEffect[index];
            }
            if (ev.noEffect.ContainsKey(index))
            {
                noEffect.Content = ev.noEffect[index];
            }
        }

        private void BtSave_Click(object sender, RoutedEventArgs rea)
        {
            ev.Name = txname.Text;
            ev.happened = cbHappened.IsChecked.GetValueOrDefault(false);
            ev.repeatable = cbRepeatable.IsChecked.GetValueOrDefault(false);
            ev.Minor = cbMinor.IsChecked.GetValueOrDefault(false);
            ev.GloballyDisplayed = cbGloballyDisplayed.IsChecked.GetValueOrDefault(false);
            int.TryParse(tbHappenChance.Text, out ev.happenChance);
            int.TryParse(tbStartYear.Text, out ev.StartYear);
            int.TryParse(tbStartMonth.Text, out ev.StartMonth);
            int.TryParse(tbEndYear.Text, out ev.EndYear);
            int.TryParse(tbEndMonth.Text, out ev.EndMonth);
            ev.Image = tbImage.Text;
            ev.Sound = tbSound.Text;

            ev.AfterEventHappened = int.Parse(cbAfterEventHappened.Text.Split(' ')[0]);

            if (tempDialog != null)
            {
                ev.dialog = tempDialog;
            }
            if (tempYesDialog != null)
            {
                ev.yesdialog = tempYesDialog;
            }
            if (tempNoDialog != null)
            {
                ev.nodialog = tempNoDialog;
            }

            if (!edit)
            {
                scen.AllEvents.Add(ev);
            }
            this.Close();
        }

        private void BtExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void _BtnDialog_Click( List<PersonIdDialog> tempDialog, String type)
        {
            Window window = new Window();
            window.Title = type == "dialog" ? "設置事件對話" : (type == "yes" ? "設置選「是」事件對話" : "設置選「否」事件對話");
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Width = 800;
            window.Height = 600;

            Grid grid = new Grid();
            window.Content = grid;

            DataGrid dataGrid1 = new DataGrid();
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("武將編號", typeof(int));
            dt2.Columns.Add("對話");

            foreach (PersonIdDialog i in tempDialog)
            {
                DataRow dr = dt2.NewRow();
                dr["武將編號"] = i.id;
                if (type == "dialog")
                {
                    dr["對話"] = i.dialog;
                }
                else if (type == "yes")
                {
                    dr["對話"] = i.yesdialog;
                }
                else if (type == "no")
                {
                    dr["對話"] = i.nodialog;
                }
                dt2.Rows.Add(dr);
            }
            dataGrid1.ItemsSource = dt2.DefaultView;

            dataGrid1.Loaded += SetWidth;
            void SetWidth(object source, EventArgs e2)
            {
                var g = (DataGrid)source;
                g.Columns[0].Width = 80;
            }

            grid.Children.Add(dataGrid1);
            dataGrid1.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            window.Closed += Closed;
            window.ShowDialog();

            void Closed(object source, EventArgs e2)
            {
                tempDialog.Clear();
                for (int r = 0; r < dataGrid1.Items.Count; r++)
                {
                    DataRowView item = dataGrid1.Items[r] as DataRowView;
                    if (item == null)
                    {
                        continue;
                    }

                    var dialog = new PersonIdDialog();
                    dialog.id = (int)item[0];
                    if (type == "dialog")
                    {
                        dialog.dialog = (string)item[1];
                    }
                    else if (type == "yes")
                    {
                        dialog.yesdialog = (string)item[1];
                    }
                    else if (type == "no")
                    {
                        dialog.nodialog = (string)item[1];
                    }
                    tempDialog.Add(dialog);
                }
            }
        }

        private void BtnDialog_Click(object sender, RoutedEventArgs e)
        {
            if (tempDialog == null)
            {
                tempDialog = this.ev.dialog;
            }
            _BtnDialog_Click(tempDialog, "dialog");
        }

        private void BtnYesDialog_Click(object sender, RoutedEventArgs e)
        {
            if (tempYesDialog == null)
            {
                tempYesDialog = this.ev.yesdialog;
            }
            _BtnDialog_Click(tempYesDialog, "yes");
        }

        private void BtnNoDialog_Click(object sender, RoutedEventArgs e)
        {
            if (tempNoDialog == null)
            {
                tempNoDialog = this.ev.nodialog;
            }
            _BtnDialog_Click(tempNoDialog, "no");
        }

        private void Btn_PersonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
