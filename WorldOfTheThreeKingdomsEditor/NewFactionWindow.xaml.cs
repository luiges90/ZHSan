﻿using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using GameObjects.FactionDetail;
using GameObjects.TroopDetail;

namespace WorldOfTheThreeKingdomsEditor
{
    /// <summary>
    /// NewFactionWindow.xaml 的互動邏輯
    /// </summary>
    public partial class NewFactionWindow : Window
    {
        private GameScenario scen;
        private Window mainwindow;
        private List<int> listArchis = new List<int>();
        private Faction faction;
        private DataTable dt;
        private GameObjectList list;
        public NewFactionWindow(GameScenario scen,Window window,Faction faction2)
        {
            this.scen = scen;
            InitializeComponent();
            mainwindow = window;
            faction = faction2;
            BtFcationColor.Background = new SolidColorBrush(Color.FromArgb(scen.GameCommonData.AllColors[faction.ColorIndex].A, scen.GameCommonData.AllColors[faction.ColorIndex].R, scen.GameCommonData.AllColors[faction.ColorIndex].G, scen.GameCommonData.AllColors[faction.ColorIndex].B));
            InitArchis();
            InitTechniques();
            InitMilkinds();
            InitDipRelations();
            //this.Closed += NewFactionWindow_Closed;

            txname.SetBinding(TextBox.TextProperty, new Binding("Name") { Source = faction });
            btleader.SetBinding(Button.ContentProperty, new Binding("Leader") { Source = faction });
            btPrince.SetBinding(Button.ContentProperty, new Binding("Prince") { Source = faction });
            txTechniquePoint.SetBinding(TextBox.TextProperty, new Binding("TechniquePoint") { Source = faction });
            txReputation.SetBinding(TextBox.TextProperty, new Binding("Reputation") { Source = faction });
            txgongxiandu.SetBinding(TextBox.TextProperty, new Binding("chaotinggongxiandu") { Source = faction });
            btguanjuezi.SetBinding(Button.ContentProperty, new Binding("guanjuezifuchuan") { Source = faction });
            btCapital.SetBinding(Button.ContentProperty, new Binding("Capital") { Source = faction });
            cmUpingTec.SelectedIndex = faction.UpgradingTechnique;
            txleftdays.SetBinding(TextBox.TextProperty, new Binding("UpgradingDaysLeft") { Source = faction });
            cbIsAlien.SetBinding(CheckBox.IsCheckedProperty, new Binding("IsAlien") { Source = faction });
            cbNoPSelectable.SetBinding(CheckBox.IsCheckedProperty, new Binding("NotPlayerSelectable") { Source = faction});
            // btleader.Content = faction.Leader;
        }

        public void InitArchis()
        {
            CheckBox check = new CheckBox();
            foreach (Architecture a in faction.Architectures)
            {
                check = new CheckBox();
                check.Content = a;
                lbArchis.Items.Add(check);
            }
        }

        public void InitTechniques()
        {
            int maxrow = 0;
            int maxcolumn = 0;
            foreach(GameObjects.FactionDetail.Technique  technique in CommonData.Current.AllTechniques.GetTechniqueList())
            {
                if(technique.DisplayRow> maxrow)
                {
                    maxrow = technique.DisplayRow;
                }
                if (technique.DisplayCol>maxcolumn)
                {
                    maxcolumn = technique.DisplayCol;
                }
            }
            for (int i = 0; i < maxcolumn+1; i++)
            {
               // gridTechniques.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star), MinWidth = 100 });
                gridTechniques.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star)});
            }
            for (int i = 0; i < maxrow+1; i++)
            {
               // gridTechniques.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star), MinHeight = 25 });
                gridTechniques.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star)});
            }
            foreach (GameObjects.FactionDetail.Technique technique in CommonData.Current.AllTechniques.GetTechniqueList())
            {
                cmUpingTec.Items.Add(technique);
                CheckBox checkBox = new CheckBox();
                checkBox.Content = technique;
                if(faction.AvailableTechniques.Techniques.Values.Contains(technique))
                {
                    checkBox.IsChecked = true;
                }
                checkBox.Click += checkBox_check;
                void checkBox_check(object sender2, RoutedEventArgs e2)
                {
                    if (checkBox.IsChecked ==true)
                    {
                        faction.AvailableTechniques.AddTechnique(technique);
                        faction.AvailableTechniquesString = faction.AvailableTechniques.SaveToString();
                    }
                    else
                    {
                       bool temp= faction.AvailableTechniques.RemoveTechniuqe(technique.ID);
                        faction.AvailableTechniquesString = faction.AvailableTechniques.SaveToString();
                    }
                }
                gridTechniques.Children.Add(checkBox);
                Grid.SetColumn(checkBox, technique.DisplayCol);
                Grid.SetRow(checkBox, technique.DisplayRow);
            }
        }

        private void InitMilkinds()
        {
            foreach (GameObjects.TroopDetail.MilitaryKind militaryKind in faction.BaseMilitaryKinds.MilitaryKinds.Values)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = militaryKind;
                lbMiliKind.Items.Add(checkBox);
            }
        }

        private void InitDipRelations()
        {
            dt = new DataTable();
            dt.Columns.Add("势力");
            dt.Columns.Add("关系", typeof(int));
            dt.Columns.Add("停战", typeof(int));
            dt.Columns[0].ReadOnly = true;
              list = new GameObjectList();
            foreach (DiplomaticRelation relation in scen.DiplomaticRelations.DiplomaticRelations.Values)
            {
                if (relation.RelationFaction1ID == faction.ID)
                {
                    list.Add(relation);
                    DataRow dr = dt.NewRow();
                    dr[0] = relation.RelationFaction2String;
                    dr[1] = relation.Relation;
                    dr[2] = relation.Truce;
                    dt.Rows.Add(dr);
                }
                else if (relation.RelationFaction2ID == faction.ID)
                {
                       list.Add(relation);
                    DataRow dr = dt.NewRow();
                    dr[0] = relation.RelationFaction1String;
                    dr[1] = relation.Relation;
                    dr[2] = relation.Truce;
                    dt.Rows.Add(dr);
                }
            }
            dt.DefaultView.AllowNew = false;
            dgDipRelation.ItemsSource = dt.DefaultView;
            dgDipRelation.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
        }


        private void Btleader_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            window.Title = "请选择势力君主--双击确认";
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Width = 800;
            window.Height = 600;
            Grid grid = new Grid();
            window.Content = grid;
            grid.Margin = new Thickness(50);
            ListBox listBox = new ListBox();
            DataGrid dataGrid1 = new DataGrid();
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("ID",typeof(int));
            dt2.Columns.Add("姓名");
            dt2.Columns.Add("性别");
            dt2.Columns.Add("所在");
            dt2.Columns.Add("所属势力");
            dt2.Columns.Add("武学");
            dt2.Columns.Add("将略");
            dt2.Columns.Add("谋略");
            dt2.Columns.Add("政理");
            dt2.Columns.Add("风度");
            GameObjectList list2 = new GameObjectList();
            foreach (Faction f in scen.Factions)
            {
                list2.Add(f.Leader);
            }
            foreach (Person person in scen.AvailablePersons)
            {
                if (!list2.HasGameObject(person))
                {
                    DataRow dr = dt2.NewRow();
                    dr["ID"] = person.ID;
                    dr["姓名"] = person.Name;
                    dr["性别"] = person.SexString;
                    dr["所在"] = person.Location;
                    dr["所属势力"] = person.BelongedFaction;
                    dr["武学"] = person.Strength;
                    dr["将略"] = person.Command;
                    dr["谋略"] = person.Intelligence;
                    dr["政理"] = person.Politics;
                    dr["风度"] = person.Glamour;
                    dt2.Rows.Add(dr);
                }
            }
            dataGrid1.ItemsSource = dt2.DefaultView;
            dataGrid1.MouseDoubleClick += dataGrid1_MouseDoubleClick;
            void dataGrid1_MouseDoubleClick(object sender2, MouseButtonEventArgs e2)
            {
                if (dataGrid1.SelectedItem != null)
                {
                    DataTable dt5= ((DataView)dataGrid1.ItemsSource).ToTable();
                    Person person = scen.Persons.GetGameObject(int.Parse(dt5.Rows[dataGrid1.SelectedIndex]["ID"].ToString())) as Person;
                    faction.Leader = person;//这里还要处理一下，武将原有势力或原有所属城池的可能bug
                    btleader.SetBinding(Button.ContentProperty, new Binding("Leader") { Source = faction });
                    window.Close();
                }
            }
            dataGrid1.IsReadOnly = true;
            grid.Children.Add(dataGrid1);
            dataGrid1.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
            window.ShowDialog();
        }

        private void BtFcationColor_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            window.Title = "请选择势力颜色";
            window.Height = 500;
            window.Width = 500;
            window.ResizeMode = ResizeMode.NoResize;
            Grid grid1 = new Grid();
            window.Content = grid1;
            ListBox listBox = new ListBox();
            listBox.ItemsPanel = (ItemsPanelTemplate)this.FindResource("ItemsPanelTemplate1");
            listBox.Width = 0.5 * window.Width;
            listBox.Height = 0.5 * window.Height;
            listBox.HorizontalAlignment = HorizontalAlignment.Center;
            listBox.VerticalAlignment = VerticalAlignment.Center;
            for (int i = 0; i < scen.GameCommonData.AllColors.Count; i++)
            {
                listBox.Items.Add(new TextBox() {MaxWidth=listBox.Width/9, Background = new SolidColorBrush(Color.FromArgb(scen.GameCommonData.AllColors[i].A, scen.GameCommonData.AllColors[i].R, scen.GameCommonData.AllColors[i].G, scen.GameCommonData.AllColors[i].B)), Height = listBox.Width / 8, Text = "   ", Width = listBox.Width, IsHitTestVisible = false });
            }
            Button button = new Button();
            button.Content = "确定";
            button.Click += Button_Click_2;
            void Button_Click_2(object sender2, RoutedEventArgs e2)
            {
                if (listBox.SelectedItem != null)
                {
                    int ii = listBox.SelectedIndex;
                    this.BtFcationColor.Background = new SolidColorBrush(Color.FromArgb(scen.GameCommonData.AllColors[ii].A, scen.GameCommonData.AllColors[ii].R, scen.GameCommonData.AllColors[ii].G, scen.GameCommonData.AllColors[ii].B));
                    //tbColorID.Text = ii.ToString();
                    faction.ColorIndex = ii;
                    //DataTable dt = ((DataView)dg.ItemsSource).Table;
                    //dt.Rows[3]["建筑列表"] = "1 1 1 1";//有效
                    //(scen.Factions[3] as Faction).Name = "3232";//无效
                }
                window.Close();
            };
            grid1.Children.Add(listBox);
            button.Width = 100;
            button.Height = 30;
            button.HorizontalAlignment = HorizontalAlignment.Right;
            button.VerticalAlignment = VerticalAlignment.Bottom;
            grid1.Children.Add(button);
            window.Closed += window_close;
            void window_close(object sender3, EventArgs e3)
            {
                txname.Focus();
            }
            window.ShowDialog();
            window.Owner = this;
        }

        private void BtPrince_Click(object sender, RoutedEventArgs e)
        {
            if (faction.Leader == null)
            {
                MessageBox.Show("请先选择势力君主");
            }
            else
            {
                Window window = new Window();
                window.Title = "请选择势力储君--双击确认";
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Width = 800;
                window.Height = 600;
                Grid grid = new Grid();
                window.Content = grid;
                grid.Margin = new Thickness(50);
                ListBox listBox = new ListBox();


                foreach (Person person in faction.Leader.ChildrenList)
                {
                    if (person.Available && person.Alive)
                    {
                        listBox.Items.Add(person);
                    }
                }
                listBox.Items.Add("无");

                listBox.MouseDoubleClick += listBox__MouseDoubleClick;
                void listBox__MouseDoubleClick(object sender2, MouseButtonEventArgs e2)
                {
                    if (listBox.SelectedItem != null)
                    {
                        if (listBox.SelectedIndex < listBox.Items.Count - 1)
                        {
                            Person person = listBox.SelectedItem as Person;
                            faction.Prince = person;
                        }
                        else
                        {
                            faction.Prince = null;
                        }
                        btPrince.SetBinding(Button.ContentProperty, new Binding("Prince") { Source = faction });
                        window.Close();
                    }
                }
                grid.Children.Add(listBox);
                window.ShowDialog();
            }
        }

        private void Btguanjuezi_Click(object sender, RoutedEventArgs e)
        {
            if (faction.Leader == null)
            {
                MessageBox.Show("请先选择势力君主");
            }
            else
            {
                Window window = new Window();
                window.Title = "请选择势力官爵--双击确认";
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Width = 800;
                window.Height = 600;
                Grid grid = new Grid();
                window.Content = grid;
                grid.Margin = new Thickness(50);
                ListBox listBox = new ListBox();
                foreach (guanjuezhongleilei guanjuezhongleilei in scen.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhongleiliebiao())
                {
                    listBox.Items.Add(guanjuezhongleilei);
                }
                listBox.MouseDoubleClick += listBox__MouseDoubleClick;
                void listBox__MouseDoubleClick(object sender2, MouseButtonEventArgs e2)
                {
                    if (listBox.SelectedItem != null)
                    {
                        guanjuezhongleilei guanjuezhongleilei = listBox.SelectedItem as guanjuezhongleilei;
                        faction.guanjue = scen.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhongleiliebiao().IndexOf(guanjuezhongleilei);
                        btguanjuezi.SetBinding(Button.ContentProperty, new Binding("guanjuezifuchuan") { Source = faction });
                        window.Close();
                    }
                }
                grid.Children.Add(listBox);
                window.ShowDialog();
            }
        }
        
        private void BtCapital_Click(object sender, RoutedEventArgs e)
        {
            if (faction.Leader == null)
            {
                MessageBox.Show("请先选择势力君主");
            }
            else if (faction.Architectures.Count == 0)
            {
                MessageBox.Show("势力城池数为0");
            }
            else
            { 
                Window window = new Window();
                window.Title = "请设置势力首都--双击确认";
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Width = 800;
                window.Height = 600;
                Grid grid = new Grid();
                window.Content = grid;
                grid.Margin = new Thickness(50);
                ListBox listBox = new ListBox();
                foreach (Architecture architecture in faction.Architectures)
                {
                    listBox.Items.Add(architecture);
                }
                listBox.MouseDoubleClick += listBox__MouseDoubleClick;
                void listBox__MouseDoubleClick(object sender2, MouseButtonEventArgs e2)
                {
                    if (listBox.SelectedItem != null)
                    {
                        Architecture architecture = listBox.SelectedItem as Architecture;
                        faction.Capital = architecture;
                        btCapital.SetBinding(Button.ContentProperty, new Binding("Capital") { Source = faction });
                        window.Close();
                    }
                }
                grid.Children.Add(listBox);
                window.ShowDialog();
            }
        }

        private void CmUpingTec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmUpingTec.SelectedItem !=null)
            {
                faction.UpgradingTechnique = cmUpingTec.SelectedIndex;
            }
        }

        private void CbNoPSelectable_Click(object sender, RoutedEventArgs e)
        {
            if (cbNoPSelectable.IsChecked == true)
            {
                faction.NotPlayerSelectable = true;
            }
            else
            {
                faction.NotPlayerSelectable = false;
            }
        }

        private void BtAddMiliKind_Click(object sender, RoutedEventArgs e)
        {
            if (faction.Leader == null)
            {
                MessageBox.Show("请先选择势力君主");
            }
            else
            {
                Window window = new Window();
                window.Title = "请添加想要的基础兵种";
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Width = 800;
                window.Height = 600;
                Grid grid = new Grid();
                window.Content = grid;
                grid.Margin = new Thickness(50);
                ListBox listBox = new ListBox();
                foreach (GameObjects.TroopDetail.MilitaryKind militaryKind in scen.GameCommonData.AllMilitaryKinds.MilitaryKinds.Values)
                {
                    if (!faction.BaseMilitaryKinds.MilitaryKinds.Values.Contains(militaryKind))
                    {
                        CheckBox checkBox = new CheckBox();
                        checkBox.IsChecked = false;
                        checkBox.Content = militaryKind;
                        checkBox.Click += checkBox_check;
                        void checkBox_check(object sender2, RoutedEventArgs e2)
                        {
                            if (checkBox.IsChecked == true)
                            {
                                faction.BaseMilitaryKinds.AddMilitaryKind(militaryKind);
                                faction.BaseMilitaryKindsString = faction.BaseMilitaryKinds.SaveToString();
                            }
                            else
                            {
                                bool temp = faction.BaseMilitaryKinds.RemoveMilitaryKind(militaryKind.ID);
                                faction.BaseMilitaryKindsString = faction.BaseMilitaryKinds.SaveToString();
                            }
                        }
                        listBox.Items.Add(checkBox);
                    }

                }
                grid.Children.Add(listBox);
                window.Closed += window_Closed;
                void window_Closed(object sender3, EventArgs e3)
                {
                    lbMiliKind.Items.Clear();
                    InitMilkinds();
                }
                window.ShowDialog();
            }
        }

        private void BtDelMiliKind_Click(object sender, RoutedEventArgs e)
        {
            for (int i = lbMiliKind.Items.Count - 1; i >= 0; i--)
            {
                CheckBox checkBox = lbMiliKind.Items[i] as CheckBox;
                if (checkBox.IsChecked == true)
                {
                    GameObjects.TroopDetail.MilitaryKind militaryKind = checkBox.Content as GameObjects.TroopDetail.MilitaryKind;
                    faction.BaseMilitaryKinds.RemoveMilitaryKind(militaryKind.ID);
                    faction.BaseMilitaryKindsString = faction.BaseMilitaryKinds.SaveToString();
                    lbMiliKind.Items.Remove(lbMiliKind.Items[i]);
                }
            }
        }

        private void DgDipRelation_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DiplomaticRelation relation = list[e.Row.GetIndex()] as DiplomaticRelation;
            if(e.Column.DisplayIndex ==1)
            {
               bool b= int.TryParse( ( e.EditingElement as TextBox).Text,out int n);
                relation.Relation = n;
            }
            else if (e.Column.DisplayIndex == 2) 
            {
                bool b = int.TryParse((e.EditingElement as TextBox).Text, out int k);
                relation.Truce = k;
            }        
        }

        private void BtInitfacDipRe_Click(object sender, RoutedEventArgs e)
        {
            scen.DiplomaticRelations.RemoveDiplomaticRelationByFactionID(faction.ID);
            foreach (Faction faction2 in scen.Factions)
            {
                if (faction2 != faction)
                {
                    scen.DiplomaticRelations.AddDiplomaticRelation(faction.ID, faction2.ID, 0);
                }
            }
            InitDipRelations();
        }

        private void Bt2InitAllDipRe_Click(object sender, RoutedEventArgs e)
        {
            scen.DiplomaticRelations.Clear();
            for (int i = 0; i < scen.Factions.Count; i++)
            {
                Faction faction = scen.Factions[i] as Faction;
                for (int j = 0; j < scen.Factions.Count; j++)
                {
                    Faction faction2 = scen.Factions[j] as Faction;
                    if (faction != faction2)
                    {
                        scen.DiplomaticRelations.AddDiplomaticRelation( faction.ID, faction2.ID, 0);
                    }
                }
            }
            InitDipRelations();
        }

        private void BtDelchrhi_Click(object sender, RoutedEventArgs e)
        {
            for (int i = lbArchis.Items.Count - 1; i >= 0; i--)
            {
                CheckBox checkBox = lbArchis.Items[i] as CheckBox;
                if (checkBox.IsChecked == true)
                {
                    Architecture architecture = checkBox.Content as Architecture;
                    if (architecture.Persons.HasGameObject(faction.Leader))
                    {
                        MessageBox.Show("无法删除" + architecture.Name+"，" + faction.Name + "的君主" + faction.Leader.Name + "在此城池中");
                        checkBox.IsChecked = false;
                    }
                    else
                    {
                        faction.Architectures.Remove(architecture);
                        faction.ArchitecturesString = faction.Architectures.SaveToString();
                        if (faction.Capital == architecture)
                        {
                            faction.Capital = null;
                            faction.CapitalID = -1;
                            btCapital.SetBinding(Button.ContentProperty, new Binding("Capital") { Source = faction });
                        }
                        architecture.BelongedFaction = null;
                        lbArchis.Items.Remove(lbArchis.Items[i]);
                    }
                }
            }
        }

        private void BtAddchrhi_Click(object sender, RoutedEventArgs e)
        {
            if (faction.Leader == null)
            {
                MessageBox.Show("请先选择势力君主");
            }
            else
            {
                Window window = new Window();
                window.Title = "请添加想要的城池";
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Width = 800;
                window.Height = 600;
                Grid grid = new Grid();
                window.Content = grid;
                grid.Margin = new Thickness(50);
                ListBox listBox = new ListBox();
                foreach (Architecture architecture in scen.Architectures)
                {
                    if (architecture.BelongedFaction != faction)
                    {
                        CheckBox checkBox = new CheckBox();
                        checkBox.IsChecked = false;
                        checkBox.Content = architecture;
                        Faction tempfaction = architecture.BelongedFaction;
                        checkBox.Click += checkBox_check;
                        void checkBox_check(object sender2, RoutedEventArgs e2)
                        {
                            if (checkBox.IsChecked == true)
                            {
                                if (tempfaction != null )
                                {
                                    if (architecture.Persons.HasGameObject(tempfaction.Leader))
                                    {
                                        MessageBox.Show("无法选择此城池，" + tempfaction.Name + "的君主" + tempfaction.Leader.Name + "在此城池中");
                                        checkBox.IsChecked = false;
                                    }
                                    else
                                    {
                                        if (tempfaction.Capital == architecture)
                                        {
                                            tempfaction.Capital = null;
                                            tempfaction.CapitalID = -1;
                                        }
                                        tempfaction.Architectures.Remove(architecture); 
                                        tempfaction.ArchitecturesString = tempfaction.Architectures.SaveToString();
                                        architecture.BelongedFaction = faction;
                                        faction.Architectures.Add(architecture);
                                        faction.ArchitecturesString = faction.Architectures.SaveToString();
                                    }
                                }
                                else
                                {
                                    architecture.BelongedFaction = faction;
                                    faction.Architectures.Add(architecture);
                                    faction.ArchitecturesString = faction.Architectures.SaveToString();
                                }
                            }
                            else
                            {
                                architecture.BelongedFaction = tempfaction;
                                faction.Architectures.Remove(architecture); 
                                faction.ArchitecturesString = faction.Architectures.SaveToString();
                                if(tempfaction !=null)
                                {
                                    if (tempfaction.CapitalID == -1)
                                    {
                                        tempfaction.Capital = architecture;
                                        tempfaction.CapitalID = architecture.ID;
                                    }
                                    tempfaction.Architectures.Add(architecture);
                                    tempfaction.ArchitecturesString = tempfaction.Architectures.SaveToString();
                                }
                            }
                        }
                        listBox.Items.Add(checkBox);
                    }
                }
                grid.Children.Add(listBox);
                window.Closed += window_Closed;
                void window_Closed(object sender3, EventArgs e3)
                {
                    lbArchis.Items.Clear();
                    InitArchis();
                }
                window.ShowDialog();
            }
        }

        private void BtEditchrhi_Click(object sender, RoutedEventArgs e)
        {
            List<CheckBox> list = new List<CheckBox>();
            for (int i = lbArchis.Items.Count - 1; i >= 0; i--)
            {
                CheckBox checkBox = lbArchis.Items[i] as CheckBox;
                if (checkBox.IsChecked == true)
                {
                    list.Add(checkBox);
                }
            }
            if (list.Count == 0)
            {
                MessageBox.Show("请选择一个想要编辑的城池");
            }
            else if (list.Count > 1)
            {
                MessageBox.Show("一次只能编辑一个城池");
            }
            else
            {
                Architecture architecture = list[0].Content as Architecture;
                Window window = new Window();
                window.Title = architecture.Name;
                window.ShowDialog();
            }
        }


        private void DgFaction_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            /* if (e.Column.Header.Equals("颜色编号"))
             {
                   int n =0;
                 if ((e.EditingElement as TextBox).Text != null && (e.EditingElement as TextBox).Text != null)
                 {
                     // n = int.Parse((e.EditingElement as TextBox).Text);
                     bool b = int.TryParse((e.EditingElement as TextBox).Text,out n);
                     if(n !=0)
                     {
                          Microsoft.Xna.Framework.Color color = scen.GameCommonData.AllColors[n];
                          e.Row.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
                     }
                 }
             }*/  // 这两个屏蔽是显示势力颜色，但因只能做到整行背景色改变，太花
        }

        private void DgFaction_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            /* if(e.Row.GetIndex()<dgFaction.Items.Count-1)
              {
                  Microsoft.Xna.Framework.Color color = scen.GameCommonData.AllColors[((Faction)scen.Factions[e.Row.GetIndex()]).ColorIndex];
                  e.Row.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
              }*/
        }
    }
}
