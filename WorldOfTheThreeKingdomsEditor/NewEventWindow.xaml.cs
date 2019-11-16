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
using Microsoft.Win32;
using System.IO;

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

        private Dictionary<int, List<Person>> tempPerson;
        private Dictionary<int, List<GameObjects.Conditions.Condition>> tempPersonCond;
        private Dictionary<int, List<GameObjects.ArchitectureDetail.EventEffect.EventEffect>> tempEffect;
        private List<PersonIdDialog> tempBiography;
        private Dictionary<int, List<GameObjects.ArchitectureDetail.EventEffect.EventEffect>> tempYesEffect;
        private Dictionary<int, List<GameObjects.ArchitectureDetail.EventEffect.EventEffect>> tempNoEffect;

        private ArchitectureList tempArchitecture;
        private List<GameObjects.Conditions.Condition> tempArchitectureCond;
        private List<GameObjects.ArchitectureDetail.EventEffect.EventEffect> tempArchEffect;
        private List<GameObjects.ArchitectureDetail.EventEffect.EventEffect> tempArchYesEffect;
        private List<GameObjects.ArchitectureDetail.EventEffect.EventEffect> tempArchNoEffect;

        private FactionList tempFactions;
        private List<GameObjects.Conditions.Condition> tempFactionCond;
        private List<GameObjects.ArchitectureDetail.EventEffect.EventEffect> tempFactionEffect;

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
            else
            {
                ev.ID = scen.AllEvents.GetFreeGameObjectID();
            }

            tempDialog = ev.dialog;
            if (tempDialog == null)
            {
                tempDialog = new List<PersonIdDialog>();
            }
            tempYesDialog = ev.yesdialog;
            if (tempYesDialog == null)
            {
                tempYesDialog = new List<PersonIdDialog>();
            }
            tempNoDialog = ev.nodialog;
            if (tempNoDialog == null)
            {
                tempNoDialog = new List<PersonIdDialog>();
            }

            tempPerson = ev.person;
            if (tempPerson == null)
            {
                tempPerson = new Dictionary<int, List<Person>>();
            }
            tempPersonCond = ev.personCond;
            if (tempPersonCond == null)
            {
                tempPersonCond = new Dictionary<int, List<GameObjects.Conditions.Condition>>();
            }
            tempEffect = ev.effect;
            if (tempEffect == null)
            {
                tempEffect = new Dictionary<int, List<GameObjects.ArchitectureDetail.EventEffect.EventEffect>>();
            }
            tempBiography = ev.scenBiography;
            if (tempBiography == null)
            {
                tempBiography = new List<PersonIdDialog>();
            }
            tempYesEffect = ev.yesEffect;
            if (tempYesEffect == null)
            {
                tempYesEffect = new Dictionary<int, List<GameObjects.ArchitectureDetail.EventEffect.EventEffect>>();
            }
            tempNoEffect = ev.noEffect;
            if (tempNoEffect == null)
            {
                tempNoEffect = new Dictionary<int, List<GameObjects.ArchitectureDetail.EventEffect.EventEffect>>();
            }

            tempArchitecture = ev.architecture;
            if (tempArchitecture == null)
            {
                tempArchitecture = new ArchitectureList();
            }
            tempArchitectureCond = ev.architectureCond;
            if (tempArchitectureCond == null)
            {
                tempArchitectureCond = new List<GameObjects.Conditions.Condition>();
            }
            tempArchEffect = ev.architectureEffect;
            if (tempArchEffect == null)
            {
                tempArchEffect = new List<GameObjects.ArchitectureDetail.EventEffect.EventEffect>();
            }
            tempArchYesEffect = ev.yesArchitectureEffect;
            if (tempArchYesEffect == null)
            {
                tempArchYesEffect = new List<GameObjects.ArchitectureDetail.EventEffect.EventEffect>();
            }
            tempArchNoEffect = ev.noArchitectureEffect;
            if (tempArchNoEffect == null)
            {
                tempArchNoEffect = new List<GameObjects.ArchitectureDetail.EventEffect.EventEffect>();
            }

            tempFactions = ev.faction;
            if (tempFactions == null)
            {
                tempFactions = new FactionList();
            }
            tempFactionCond = ev.factionCond;
            if (tempFactionCond == null)
            {
                tempFactionCond = new List<GameObjects.Conditions.Condition>();
            }
            tempFactionEffect = ev.factionEffect;
            if (tempFactionEffect == null)
            {
                tempFactionEffect = new List<GameObjects.ArchitectureDetail.EventEffect.EventEffect>();
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

            PopulateAllPersonData();

            lblArchitcture.Content = String.Join(" ", tempArchitecture.GameObjects.Select(p => p.Name));
            lblArchitctureCond.Content = String.Join(" ", tempArchitectureCond.Select(p => p.Name));
            lblArchitctureEffect.Content = String.Join(" ", tempArchEffect.Select(p => p.Name));
            lblArchitctureYesEffect.Content = String.Join(" ", tempArchYesEffect.Select(p => p.Name));
            lblArchitctureNoEffect.Content = String.Join(" ", tempArchNoEffect.Select(p => p.Name));
            lblFaction.Content = String.Join(" ", tempFactions.GameObjects.Select(p => p.Name));
            lblFactionCond.Content = String.Join(" ", tempFactionCond.Select(p => p.Name));
            lblFactionEffect.Content = String.Join(" ", tempFactionEffect.Select(p => p.Name));

            List<string> events = new List<string>();
            events.Add("-1 无");
            events.AddRange(scen.AllEvents.GameObjects.Select(e => e.ID + " " + e.Name));
            cbAfterEventHappened.ItemsSource = events;
            GameObject currentAfterHappened = scen.AllEvents.GetGameObject(ev.AfterEventHappened);
            cbAfterEventHappened.SelectedItem = ev.AfterEventHappened + " " + (currentAfterHappened == null ? "无" : currentAfterHappened.Name);
        }

        private void PopulateAllPersonData()
        {
            PopulatePersonData(0, lblPerson0, lblPersonCond0, lblEffect0, lblBiography0, lblYesEffect0, lblNoEffect0);
            PopulatePersonData(1, lblPerson1, lblPersonCond1, lblEffect1, lblBiography1, lblYesEffect1, lblNoEffect1);
            PopulatePersonData(2, lblPerson2, lblPersonCond2, lblEffect2, lblBiography2, lblYesEffect2, lblNoEffect2);
            PopulatePersonData(3, lblPerson3, lblPersonCond3, lblEffect3, lblBiography3, lblYesEffect3, lblNoEffect3);
            PopulatePersonData(4, lblPerson4, lblPersonCond4, lblEffect4, lblBiography4, lblYesEffect4, lblNoEffect4);
            PopulatePersonData(5, lblPerson5, lblPersonCond5, lblEffect5, lblBiography5, lblYesEffect5, lblNoEffect5);
        }

        private void PopulatePersonData(int index, Label person, Label personCond, Label effect, Label biography, Label yesEffect, Label noEffect)
        {
            if (tempPerson.ContainsKey(index))
            {
                person.Content = String.Join(" ", tempPerson[index].Select(p => p == null ? "任何" : p.Name));
            }
            else
            {
                person.Content = "";
            }
            if (tempPersonCond.ContainsKey(index))
            {
                personCond.Content = String.Join(" ", tempPersonCond[index].Select(c => c.Name));
            }
            else
            {
                personCond.Content = "";
            }
            if (tempEffect.ContainsKey(index))
            {
                effect.Content = String.Join(" ", tempEffect[index].Select(e => e.Name));
            }
            else
            {
                effect.Content = "";
            }
            if (tempBiography.Count > index)
            {
                biography.Content = tempBiography[index].dialog;
            }
            else
            {
                biography.Content = "";
            }
            if (tempYesEffect.ContainsKey(index))
            {
                yesEffect.Content = String.Join(" ", tempYesEffect[index].Select(e => e.Name));
            }
            else
            {
                yesEffect.Content = "";
            }
            if (tempNoEffect.ContainsKey(index))
            {
                noEffect.Content = String.Join(" ", tempNoEffect[index].Select(e => e.Name));
            }
            else
            {
                noEffect.Content = "";
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
                ev.dialogString = ev.SaveDialogToString();
            }
            if (tempYesDialog != null)
            {
                ev.yesdialog = tempYesDialog;
                ev.yesdialogString = ev.SaveyesDialogToString();
            }
            if (tempNoDialog != null)
            {
                ev.nodialog = tempNoDialog;
                ev.nodialogString = ev.SavenoDialogToString();
            }
            if (tempBiography != null)
            {
                ev.scenBiography = tempBiography;
                ev.scenBiographyString = ev.SaveScenBiographyToString();
            }
            ev.person = tempPerson;
            ev.personString = ev.SavePersonIdToString();
            ev.personCond = tempPersonCond;
            ev.PersonCondString = ev.SavePersonCondToString();
            ev.effect = tempEffect;
            ev.effectString = ev.SaveEventEffectToString();
            ev.yesEffect = tempYesEffect;
            ev.yesEffectString = ev.SaveYesEffectToString();
            ev.noEffect = tempNoEffect;
            ev.noEffectString = ev.SaveNoEffectToString();

            ev.architecture = tempArchitecture;
            ev.architectureString = ev.architecture.SaveToString();
            ev.architectureCond = tempArchitectureCond;
            ev.architectureCondString = ev.SaveArchitecureCondToString();
            ev.architectureEffect = tempArchEffect;
            ev.architectureEffectString = ev.SaveArchitectureEffectToString();
            ev.yesArchitectureEffect = tempArchYesEffect;
            ev.yesArchitectureEffectString = ev.SaveYesArchitectureEffectToString();
            ev.noArchitectureEffect = tempArchNoEffect;
            ev.noArchitectureEffectString = ev.SaveNoArchitectureEffectToString();

            ev.faction = tempFactions;
            ev.factionString = ev.faction.SaveToString();
            ev.factionCond = tempFactionCond;
            ev.factionCondString = ev.SaveFactionCondToString();
            ev.factionEffect = tempFactionEffect;
            ev.factionEffectIDString = ev.SaveFactionEffectToString();

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
            if (type == "dialog")
            {
                window.Title = "設置事件武將對話";
            }
            else if (type == "yes")
            {
                window.Title = "設置選「是」事件武將對話";
            }
            else if (type == "no")
            {
                window.Title = "設置選「否」事件武將對話";
            }
            else if (type == "biography")
            {
                window.Title = "設置事件武將列傳";
            }
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
                else if (type == "biography")
                {
                    dr["對話"] = i.dialog;
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
                    if (int.TryParse(item[0].ToString(), out dialog.id)) {
                        if (item[1] is string)
                        {
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
                            else if (type == "biography")
                            {
                                dialog.dialog = (string)item[1];
                            }
                            tempDialog.Add(dialog);
                        }
                    }
                }
            }
        }

        private void BtnDialog_Click(object sender, RoutedEventArgs e)
        {
            _BtnDialog_Click(tempDialog, "dialog");
        }

        private void BtnYesDialog_Click(object sender, RoutedEventArgs e)
        {
            _BtnDialog_Click(tempYesDialog, "yes");
        }

        private void BtnNoDialog_Click(object sender, RoutedEventArgs e)
        {
            _BtnDialog_Click(tempNoDialog, "no");
        }

        private void Btn_PersonClick(object sender, RoutedEventArgs e)
        {
            string indexName = ((Button)sender).Name;
            int index = int.Parse(indexName.Substring(indexName.Length - 1));

            if (!tempPerson.ContainsKey(index))
            {
                tempPerson.Add(index, new List<Person>());
            }

            Window window = new Window();
            window.Title = "選擇事件能觸發的武將，留空代表所有武將";
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Width = 800;
            window.Height = 600;

            Grid grid = new Grid();
            window.Content = grid;

            DataGrid dataGrid1 = new DataGrid();
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("點選", typeof(Boolean));
            dt2.Columns.Add("ID", typeof(int));
            dt2.Columns["ID"].ReadOnly = true;
            dt2.Columns.Add("姓名");
            dt2.Columns["姓名"].ReadOnly = true;
            dt2.Columns.Add("性别");
            dt2.Columns["性别"].ReadOnly = true;
            dt2.Columns.Add("登場年");
            dt2.Columns["登場年"].ReadOnly = true;
            dt2.Columns.Add("出生年");
            dt2.Columns["出生年"].ReadOnly = true;
            dt2.Columns.Add("死亡年");
            dt2.Columns["死亡年"].ReadOnly = true;

            foreach (Person person in scen.Persons)
            {
                DataRow dr = dt2.NewRow();
                if (tempPerson.Count > index)
                {
                    dr["點選"] = tempPerson[index].Contains(person);
                }
                else
                {
                    dr["點選"] = false;
                }
                dr["ID"] = person.ID;
                dr["姓名"] = person.Name;
                dr["性别"] = person.SexString;
                dr["登場年"] = person.YearAvailable;
                dr["出生年"] = person.YearBorn;
                dr["死亡年"] = person.YearDead;
                dt2.Rows.Add(dr);
            }
            dataGrid1.ItemsSource = dt2.DefaultView;

            grid.Children.Add(dataGrid1);
            dataGrid1.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            window.Closed += Closed;
            window.ShowDialog();

            void Closed(object source, EventArgs e2)
            {
                if (!tempPerson.ContainsKey(index))
                {
                    tempPerson.Add(index, new List<Person>());
                }
                else
                {
                    tempPerson[index].Clear();
                }
                for (int r = 0; r < dataGrid1.Items.Count; r++)
                {
                    DataRowView item = dataGrid1.Items[r] as DataRowView;
                    if (item == null)
                    {
                        continue;
                    }

                    if ((bool) item["點選"])
                    {
                        tempPerson[index].Add((Person)scen.Persons.GetGameObject((int)item["ID"]));
                    }
                }

                if (tempPerson[index].Count <= 0)
                {
                    tempPerson[index].Add(null);
                }

                PopulateAllPersonData();
            }
        }

        private Window _CondEdit(List<GameObjects.Conditions.Condition> conds, String title)
        {
            Window window = new Window();
            window.Title = title;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Width = 800;
            window.Height = 600;

            Grid grid = new Grid();
            window.Content = grid;

            DataGrid dataGrid1 = new DataGrid();
            DataTable dt1 = new DataTable();
            dataGrid1.Width = 360;
            dataGrid1.Margin = new Thickness(0, 0, 400, 0);
            dt1.Columns.Add("ID", typeof(int));
            dt1.Columns.Add("名稱");
            foreach (var cond in conds)
            {
                DataRow dr = dt1.NewRow();
                dr["ID"] = cond.ID;
                dr["名稱"] = cond.Name;
                dt1.Rows.Add(dr);
            }
            dataGrid1.ItemsSource = dt1.DefaultView;
            dataGrid1.IsReadOnly = true;
            dataGrid1.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            grid.Children.Add(dataGrid1);
            dataGrid1.Loaded += SetWidth;
            dataGrid1.MouseDoubleClick += Grid1_DoublcClick;
            void Grid1_DoublcClick(object sender2, MouseButtonEventArgs e2)
            {
                if (dataGrid1.SelectedItem != null)
                {
                    int id = (int)((DataRowView)dataGrid1.SelectedItem)["ID"];
                    var cond = scen.GameCommonData.AllConditions.GetCondition(id);
                    conds.Remove(cond);

                    ((DataRowView)dataGrid1.SelectedItem).Delete();
                }
            }

            DataGrid dataGrid2 = new DataGrid();
            DataTable dt2 = new DataTable();
            dataGrid2.Width = 360;
            dataGrid2.Margin = new Thickness(400, 0, 0, 0);
            dt2.Columns.Add("ID", typeof(int));
            dt2.Columns.Add("名稱");
            foreach (var cond in scen.GameCommonData.AllConditions.Conditions.Values)
            {
                DataRow dr = dt2.NewRow();
                dr["ID"] = cond.ID;
                dr["名稱"] = cond.Name;
                dt2.Rows.Add(dr);
            }
            dataGrid2.ItemsSource = dt2.DefaultView;
            dataGrid2.IsReadOnly = true;
            dataGrid2.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            grid.Children.Add(dataGrid2);
            dataGrid2.Loaded += SetWidth;
            dataGrid2.MouseDoubleClick += Grid2_DoublcClick;
            void Grid2_DoublcClick(object sender2, MouseButtonEventArgs e2)
            {
                if (dataGrid2.SelectedItem != null)
                {
                    int id = (int)((DataRowView)dataGrid2.SelectedItem)["ID"];
                    var cond = scen.GameCommonData.AllConditions.GetCondition(id);
                    conds.Add(cond);

                    DataRow dr = dt1.NewRow();
                    dr["ID"] = cond.ID;
                    dr["名稱"] = cond.Name;
                    dt1.Rows.Add(dr);
                }
            }

            void SetWidth(object source, EventArgs e2)
            {
                var g = (DataGrid)source;
                g.Columns[0].Width = 80;
            }

            return window;
        }

        private void Btn_PersonCondClick(object sender, RoutedEventArgs e)
        {
            string indexName = ((Button)sender).Name;
            int index = int.Parse(indexName.Substring(indexName.Length - 1));

            if (!tempPersonCond.ContainsKey(index))
            {
                tempPersonCond.Add(index, new List<GameObjects.Conditions.Condition>());
            }

            Window window = _CondEdit(tempPersonCond[index], "選擇能觸發的武將需符合的條件。雙按加入/取消");
            window.Closed += Closed;
            window.ShowDialog();

            void Closed(object source, EventArgs e2)
            {
                PopulateAllPersonData();
            }
        }

        private Window _Btn_EffectClick(List<GameObjects.ArchitectureDetail.EventEffect.EventEffect> effects, string title)
        {
            Window window = new Window();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Width = 800;
            window.Height = 600;

            Grid grid = new Grid();
            window.Content = grid;

            DataGrid dataGrid1 = new DataGrid();
            DataTable dt1 = new DataTable();
            dataGrid1.Width = 360;
            dataGrid1.Margin = new Thickness(0, 0, 400, 0);
            dt1.Columns.Add("ID", typeof(int));
            dt1.Columns.Add("名稱");
            
            foreach (var cond in effects)
            {
                DataRow dr = dt1.NewRow();
                dr["ID"] = cond.ID;
                dr["名稱"] = cond.Name;
                dt1.Rows.Add(dr);
            }
            dataGrid1.ItemsSource = dt1.DefaultView;
            dataGrid1.IsReadOnly = true;
            dataGrid1.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            grid.Children.Add(dataGrid1);
            dataGrid1.Loaded += SetWidth;
            dataGrid1.MouseDoubleClick += Grid1_DoublcClick;
            void Grid1_DoublcClick(object sender2, MouseButtonEventArgs e2)
            {
                if (dataGrid1.SelectedItem != null)
                {
                    int id = (int)((DataRowView)dataGrid1.SelectedItem)["ID"];
                    var cond = scen.GameCommonData.AllEventEffects.GetEventEffect(id);
                    effects.Remove(cond);

                    ((DataRowView)dataGrid1.SelectedItem).Delete();
                }
            }

            DataGrid dataGrid2 = new DataGrid();
            DataTable dt2 = new DataTable();
            dataGrid2.Width = 360;
            dataGrid2.Margin = new Thickness(400, 0, 0, 0);
            dt2.Columns.Add("ID", typeof(int));
            dt2.Columns.Add("名稱");
            foreach (var cond in scen.GameCommonData.AllEventEffects.EventEffects.Values)
            {
                DataRow dr = dt2.NewRow();
                dr["ID"] = cond.ID;
                dr["名稱"] = cond.Name;
                dt2.Rows.Add(dr);
            }
            dataGrid2.ItemsSource = dt2.DefaultView;
            dataGrid2.IsReadOnly = true;
            dataGrid2.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            grid.Children.Add(dataGrid2);
            dataGrid2.Loaded += SetWidth;
            dataGrid2.MouseDoubleClick += Grid2_DoublcClick;
            void Grid2_DoublcClick(object sender2, MouseButtonEventArgs e2)
            {
                if (dataGrid2.SelectedItem != null)
                {
                    int id = (int)((DataRowView)dataGrid2.SelectedItem)["ID"];
                    var cond = scen.GameCommonData.AllEventEffects.GetEventEffect(id);
                    effects.Add(cond);

                    DataRow dr = dt1.NewRow();
                    dr["ID"] = cond.ID;
                    dr["名稱"] = cond.Name;
                    dt1.Rows.Add(dr);
                }
            }

            void SetWidth(object source, EventArgs e2)
            {
                var g = (DataGrid)source;
                g.Columns[0].Width = 80;
            }

            return window;
        }

        private void Btn_EffectClick(object sender, RoutedEventArgs e)
        {
            string indexName = ((Button)sender).Name;
            int index = int.Parse(indexName.Substring(indexName.Length - 1));

            if (!tempEffect.ContainsKey(index))
            {
                tempEffect.Add(index, new List<GameObjects.ArchitectureDetail.EventEffect.EventEffect>());
            }

            Window window = _Btn_EffectClick(tempEffect[index], "選擇對武將的事件效果。雙按加入/取消");

            window.Closed += Closed;
            window.ShowDialog();

            void Closed(object source, EventArgs e2)
            {
                PopulateAllPersonData();
            }
        }

        private void Btn_BiographyClick(object sender, RoutedEventArgs e)
        {
            _BtnDialog_Click(tempBiography, "biography");
            PopulateAllPersonData();
        }

        private void Btn_YesEffectClick(object sender, RoutedEventArgs e)
        {
            string indexName = ((Button)sender).Name;
            int index = int.Parse(indexName.Substring(indexName.Length - 1));

            if (!tempYesEffect.ContainsKey(index))
            {
                tempYesEffect.Add(index, new List<GameObjects.ArchitectureDetail.EventEffect.EventEffect>());
            }

            Window window = _Btn_EffectClick(tempYesEffect[index], "選擇答「是」時對武將的事件效果。雙按加入/取消");

            window.Closed += Closed;
            window.ShowDialog();

            void Closed(object source, EventArgs e2)
            {
                PopulateAllPersonData();
            }
        }

        private void Btn_NoEffectClick(object sender, RoutedEventArgs e)
        {
            string indexName = ((Button)sender).Name;
            int index = int.Parse(indexName.Substring(indexName.Length - 1));

            if (!tempNoEffect.ContainsKey(index))
            {
                tempNoEffect.Add(index, new List<GameObjects.ArchitectureDetail.EventEffect.EventEffect>());
            }

            Window window = _Btn_EffectClick(tempNoEffect[index], "選擇答「否」時對武將的事件效果。雙按加入/取消");

            window.Closed += Closed;
            window.ShowDialog();

            void Closed(object source, EventArgs e2)
            {
                PopulateAllPersonData();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            string indexName = ((Button)sender).Name;
            int index = int.Parse(indexName.Substring(indexName.Length - 1));

            tempPerson.Remove(index);
            tempPersonCond.Remove(index);
            tempEffect.Remove(index);
            tempYesEffect.Remove(index);
            tempNoEffect.Remove(index);

            PopulateAllPersonData();
        }

        private void BtnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.InitialDirectory = Directory.GetCurrentDirectory() + @"\Content\Textures\GameComponents\tupianwenzi\Data\tupian";
            dialog.DefaultExt = ".jpg";
            dialog.Filter = "JPEG檔案 (*.jpg)|*.jpg";

            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                tbImage.Text = dialog.SafeFileName;
            }
        }

        private void BtnSound_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.InitialDirectory = Directory.GetCurrentDirectory() + @"\Content\Sound\Yinxiao";
            dialog.DefaultExt = ".wav";
            dialog.Filter = "WAV檔案 (*.wav)|*.wav";

            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                tbSound.Text = dialog.SafeFileName;
            }
        }

        private void BtnArchitecture_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            window.Title = "選擇事件能觸發的建築";
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Width = 800;
            window.Height = 600;

            Grid grid = new Grid();
            window.Content = grid;

            DataGrid dataGrid1 = new DataGrid();
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("點選", typeof(Boolean));
            dt2.Columns.Add("ID", typeof(int));
            dt2.Columns["ID"].ReadOnly = true;
            dt2.Columns.Add("名稱");
            dt2.Columns["名稱"].ReadOnly = true;
            dt2.Columns.Add("種類");
            dt2.Columns["種類"].ReadOnly = true;
            dt2.Columns.Add("州");
            dt2.Columns["州"].ReadOnly = true;
            dt2.Columns.Add("地域");
            dt2.Columns["地域"].ReadOnly = true;

            foreach (Architecture arch in scen.Architectures)
            {
                DataRow dr = dt2.NewRow();
                dr["點選"] = tempArchitecture.GameObjects.Contains(arch);
                dr["ID"] = arch.ID;
                dr["名稱"] = arch.Name;
                dr["種類"] = arch.KindString;
                dr["州"] = arch.StateString;
                dr["地域"] = arch.RegionString;
                dt2.Rows.Add(dr);
            }
            dataGrid1.ItemsSource = dt2.DefaultView;

            grid.Children.Add(dataGrid1);
            dataGrid1.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            window.Closed += Closed;
            window.ShowDialog();

            void Closed(object source, EventArgs e2)
            {
                tempArchitecture.Clear();
                for (int r = 0; r < dataGrid1.Items.Count; r++)
                {
                    DataRowView item = dataGrid1.Items[r] as DataRowView;
                    if (item == null)
                    {
                        continue;
                    }

                    if ((bool)item["點選"])
                    {
                        tempArchitecture.Add((Architecture)scen.Architectures.GetGameObject((int)item["ID"]));
                    }
                }

                lblArchitcture.Content = String.Join(" ", tempArchitecture.GameObjects.Select(p => p.Name));
            }
        }

        private void BtnArchitectureCond_Click(object sender, RoutedEventArgs e)
        {
            Window window = _CondEdit(tempArchitectureCond, "選擇能觸發的建築需符合的條件。雙按加入/取消");
            window.Closed += Closed;
            window.ShowDialog();

            void Closed(object source, EventArgs e2)
            {
                lblArchitctureCond.Content = String.Join(" ", tempArchitectureCond.Select(p => p.Name));
            }
        }

        private void BtnArchitectureEffect_Click(object sender, RoutedEventArgs e)
        {
            Window window = _Btn_EffectClick(tempArchEffect, "選擇對武將建築的事件效果。雙按加入/取消");

            window.Closed += Closed;
            window.ShowDialog();

            void Closed(object source, EventArgs e2)
            {
                lblArchitctureEffect.Content = String.Join(" ", tempArchEffect.Select(p => p.Name));
            }
        }

        private void BtnArchitectureYesEffect_Click(object sender, RoutedEventArgs e)
        {
            Window window = _Btn_EffectClick(tempArchYesEffect, "選擇答「是」時對武將建築的事件效果。雙按加入/取消");

            window.Closed += Closed;
            window.ShowDialog();

            void Closed(object source, EventArgs e2)
            {
                lblArchitctureYesEffect.Content = String.Join(" ", tempArchYesEffect.Select(p => p.Name));
            }
        }

        private void BtnArchitectureNoEffect_Click(object sender, RoutedEventArgs e)
        {
            Window window = _Btn_EffectClick(tempArchNoEffect, "選擇答「否」時對武將建築的事件效果。雙按加入/取消");

            window.Closed += Closed;
            window.ShowDialog();

            void Closed(object source, EventArgs e2)
            {
                lblArchitctureNoEffect.Content = String.Join(" ", tempArchNoEffect.Select(p => p.Name));
            }
        }

        private void BtnFaction_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            window.Title = "選擇事件能觸發的勢力";
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Width = 800;
            window.Height = 600;

            Grid grid = new Grid();
            window.Content = grid;

            DataGrid dataGrid1 = new DataGrid();
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("點選", typeof(Boolean));
            dt2.Columns.Add("ID", typeof(int));
            dt2.Columns["ID"].ReadOnly = true;
            dt2.Columns.Add("名稱");
            dt2.Columns["名稱"].ReadOnly = true;
            dt2.Columns.Add("君主");
            dt2.Columns["君主"].ReadOnly = true;
            dt2.Columns.Add("建築數");
            dt2.Columns["建築數"].ReadOnly = true;
            dt2.Columns.Add("武將數");
            dt2.Columns["武將數"].ReadOnly = true;

            foreach (Faction f in scen.Factions)
            {
                DataRow dr = dt2.NewRow();
                dr["點選"] = tempFactions.GameObjects.Contains(f);
                dr["ID"] = f.ID;
                dr["名稱"] = f.Name;
                dr["君主"] = f.Leader.Name;
                dr["建築數"] = f.ArchitectureCount;
                dr["武將數"] = f.PersonCount;
                dt2.Rows.Add(dr);
            }
            dataGrid1.ItemsSource = dt2.DefaultView;

            grid.Children.Add(dataGrid1);
            dataGrid1.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

            window.Closed += Closed;
            window.ShowDialog();

            void Closed(object source, EventArgs e2)
            {
                tempFactions.Clear();
                for (int r = 0; r < dataGrid1.Items.Count; r++)
                {
                    DataRowView item = dataGrid1.Items[r] as DataRowView;
                    if (item == null)
                    {
                        continue;
                    }

                    if ((bool)item["點選"])
                    {
                        tempFactions.Add((Faction)scen.Factions.GetGameObject((int)item["ID"]));
                    }
                }

                lblFaction.Content = String.Join(" ", tempFactions.GameObjects.Select(p => p.Name));
            }
        }

        private void BtnFactionCond_Click(object sender, RoutedEventArgs e)
        {
            Window window = _CondEdit(tempFactionCond, "選擇能觸發的勢力需符合的條件。雙按加入/取消");
            window.Closed += Closed;
            window.ShowDialog();

            void Closed(object source, EventArgs e2)
            {
                lblFactionCond.Content = String.Join(" ", tempFactionCond.Select(p => p.Name));
            }
        }

        private void BtnFactionEffect_Click(object sender, RoutedEventArgs e)
        {
            Window window = _Btn_EffectClick(tempFactionEffect, "選擇對武將勢力的事件效果。雙按加入/取消");

            window.Closed += Closed;
            window.ShowDialog();

            void Closed(object source, EventArgs e2)
            {
                lblFactionEffect.Content = String.Join(" ", tempFactionEffect.Select(p => p.Name));
            }
        }
    }
}
