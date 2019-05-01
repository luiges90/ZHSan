//using GameObjects;
//using GameObjects.Conditions;
//using GameObjects.Influences;
//using GameObjects.PersonDetail;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;

//namespace WorldOfTheThreeKingdomsEditor
//{
//    class PersonIDRelationsTab
//    {
//        private DataGrid dg;
//        private GameScenario scen;
//        private bool settingUp = false;
//        private DataTable dt;
//        // dict is write-thru
//        public PersonIDRelationsTab(DataGrid dg, GameScenario scen)
//        {
//            this.scen = scen;
//            this.dg = dg;
//        }

//        //private void initstates

//        public void setup()
//        {
//            dg.ItemsSource = null;
//            dt = new DataTable();
//            dt.Columns.Add("行号", typeof(int));
//            dt.Columns.Add("ID", typeof(int));
//            dt.Columns.Add("武将名称");
//            dt.Columns["武将名称"].ReadOnly = true;
//            dt.Columns.Add("对方武将ID", typeof(int));
//            dt.Columns.Add("对方武将名称");
//            dt.Columns["对方武将名称"].ReadOnly = true;
//            dt.Columns.Add("与对方武将的友好度", typeof(int));
//            initdt();

//            //dg.Columns[dt.Columns["行号"].Ordinal].Visibility = Visibility.Hidden;
//            //dg.CanUserSortColumns = false;
//            dg.ItemsSource = dt.AsDataView();
//            //dt.RowChanged += Dt_RowChanged;//值变更后执行
//            dt.RowDeleting += Dt_RowDeleting;
//            dg.Loaded += Dg_Loaded;
//        }

//        private void Dg_Loaded(object sender, RoutedEventArgs e)
//        {
//            dg.Columns[dt.Columns["行号"].Ordinal].Visibility = Visibility.Hidden;
//        }

//        private void initdt()
//        {
//            settingUp = true;
//            if (dt != null)
//            {

//                dt.Clear();
//            }
//            foreach (var personIDRelation in scen.PersonRelationIds)
//            {
//                DataRow row = dt.NewRow();
//                Person person1 = scen.Persons.GetGameObject(personIDRelation.PersonID1) as Person;
//                Person person2 = scen.Persons.GetGameObject(personIDRelation.PersonID2) as Person;
//                row["行号"] = scen.PersonRelationIds.IndexOf(personIDRelation);
//                row["ID"] = personIDRelation.PersonID1;
//                row["武将名称"] = person1 != null ? person1.Name : "";
//                row["对方武将ID"] = personIDRelation.PersonID2;
//                row["对方武将名称"] = person2 != null ? person2.Name : "";
//                row["与对方武将的友好度"] = personIDRelation.Relation;
//                dt.Rows.Add(row);
//            }
//            settingUp = false;
//        }

//        private void Dt_RowDeleting(object sender, DataRowChangeEventArgs e)
//        {
//            //try
//            {
//                //PersonIDRelation personIDRelation = scen.PersonRelationIds[(int)(e.Row["行号"])] as PersonIDRelation;
//                Person person1 = scen.Persons.GetGameObject((int)(e.Row["ID"])) as Person;
//                Person person2 = scen.Persons.GetGameObject((int)(e.Row["对方武将ID"])) as Person;
//                if(person1 !=null && person2!=null)
//                {
//                    person1.RemoveRelation(person2);
//                }
//                //Person person;
//                //person.GetRelation
//            }
//            //catch (Exception ex)
//            //{
//            //    MessageBox.Show("資料輸入錯誤。" + ex.Message);
//            //}
//        }
//        //private int oldID = -1;

//        //private void Dt_RowChanged(object sender, DataRowChangeEventArgs e)
//        //{
//        //    try
//        //    {
//        //        if (!settingUp)
//        //        {
//        //            Region regiontemp = scen.Regions.GetGameObject(oldID) as Region;
//        //            regiontemp.ID = (int)e.Row["ID"];
//        //            regiontemp.Name = e.Row["名称"].ToString();
//        //            oldID = -1;
//        //            initdt();
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        MessageBox.Show("資料輸入錯誤。" + ex.Message);
//        //    }
//        //}

//        //public void creatWindow(bool edit, DataGrid dgstate)
//        //{
//        //    Window win = new Window();
//        //    win.Title = "修改地区信息";
//        //    win.Width = 300;
//        //    win.Height = 350;
//        //    win.ResizeMode = ResizeMode.NoResize;
//        //    win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
//        //    Grid grid = new Grid() { Background = Brushes.WhiteSmoke };
//        //    win.Content = grid;
//        //    Region region = new Region();
//        //    region.ID = scen.Regions.GetFreeGameObjectID();
//        //    region.RegionCore = null;
//        //    if (edit)
//        //    {
//        //        region = scen.Regions.GetGameObject(int.Parse(((DataRowView)dg.SelectedItem).Row["ID"].ToString())) as Region;
//        //    }
//        //    Label lbPresentationRegionCore = new Label() { Margin = new Thickness(20, 20, 0, 0), Content = "提示：当地区的地区核心其技术值超过其上限80%时，在这个地区包含州域内所有城池出发的部队，\n友（我）方部队攻防上升20%，敌方势力部队攻防下降20%" };
//        //    Label lbRegionCore = new Label() { Margin = new Thickness(110, 20, 0, 0), FontSize = 13, Content = region.RegionCore != null ? region.RegionCore.Name : "" };

//        //    Button btSetRegionCore = new Button() { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(20, 20, 0, 0), Width = 75, Height = 25, Content = "地区核心" };
//        //    btSetRegionCore.ToolTip = "提示：当地区的地区核心其技术值超过其上限80%时，在这个地区包含州域内所有城池出发的部队，\n友（我）方部队攻防上升20%，敌方势力部队攻防下降20%";
//        //    btSetRegionCore.Click += BtSetRegionCore_Click;
//        //    Architecture architecture = new Architecture();
//        //    void BtSetRegionCore_Click(object sender, RoutedEventArgs e)
//        //    {
//        //        Window winChoicenCore = new Window();
//        //        winChoicenCore.Title = "选择建筑==双击";
//        //        winChoicenCore.Width = 600;
//        //        winChoicenCore.Height = 600;
//        //        winChoicenCore.ResizeMode = ResizeMode.NoResize;
//        //        winChoicenCore.WindowStartupLocation = WindowStartupLocation.CenterScreen;
//        //        DataGrid dgArchis = new DataGrid() { Margin = new Thickness(10), Background = Brushes.WhiteSmoke };
//        //        DataTable dtArchis = new DataTable();
//        //        dtArchis.Columns.Add("ID", typeof(int));
//        //        dtArchis.Columns.Add("名称");
//        //        dtArchis.Columns.Add("种类");
//        //        dtArchis.Columns.Add("规模");
//        //        dtArchis.Columns.Add("所属州");
//        //        dtArchis.Columns.Add("所属地域");
//        //        dtArchis.Columns.Add("所属势力");
//        //        foreach (Architecture a in scen.Architectures)
//        //        {
//        //            DataRow dr = dtArchis.NewRow();
//        //            dr["ID"] = a.ID;
//        //            dr["名称"] = a.Name;
//        //            dr["种类"] = a.Kind;
//        //            dr["规模"] = a.JianzhuGuimo;
//        //            dr["所属州"] = a.LocationState;
//        //            dr["所属地域"] = a.LocationState.LinkedRegionString;
//        //            dr["所属势力"] = a.BelongedFaction;
//        //            dtArchis.Rows.Add(dr);
//        //        }
//        //        dgArchis.ItemsSource = dtArchis.AsDataView();
//        //        dgArchis.IsReadOnly = true;
//        //        dgArchis.MouseDoubleClick += Dg_MouseDoubleClick;
//        //        void Dg_MouseDoubleClick(object sender2, System.Windows.Input.MouseButtonEventArgs e2)
//        //        {
//        //            if (dgArchis.SelectedItem != null)
//        //            {
//        //                architecture = scen.Architectures.GetGameObject(int.Parse(((DataRowView)dgArchis.SelectedItem).Row["ID"].ToString())) as Architecture;
//        //                lbRegionCore.Content = architecture.Name;
//        //                winChoicenCore.Close();
//        //            }
//        //        }
//        //        dgArchis.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
//        //        winChoicenCore.Content = dgArchis;
//        //        winChoicenCore.ShowDialog();
//        //    }
//        //    Button btReSetRegionCore = new Button() { HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(0, 20, 20, 0), Width = 75, Height = 25, Content = "留空" };
//        //    btReSetRegionCore.Click += BtReSetRegionCore_Click;
//        //    void BtReSetRegionCore_Click(object sender, RoutedEventArgs e)
//        //    {
//        //        architecture = null;
//        //        lbRegionCore.Content = "";
//        //    }
//        //    Label lbPresentationChoice = new Label() { Margin = new Thickness(20, 50, 0, 0), Content = "包含州域" };
//        //    lbPresentationChoice.ToolTip = "如将原有的州域取消后，请在保存前将此州域添加到其他地区中，否则很有可能导致出错跳出";
//        //    ListBox lbStatesList = new ListBox() { Margin = new Thickness(20, 80, 20, 60) };
//        //    foreach (State state in scen.States)
//        //    {
//        //        CheckBox checkBox = new CheckBox();
//        //        checkBox.Content = state;
//        //        if (state.LinkedRegion == region)
//        //        {
//        //            checkBox.IsChecked = true;
//        //        }
//        //        lbStatesList.Items.Add(checkBox);
//        //    }

//        //    Button btSave = new Button() { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness(20, 0, 0, 20), Width = 75, Height = 25, Content = "保存退出" };
//        //    btSave.Click += BtSave_Click;
//        //    void BtSave_Click(object sender, RoutedEventArgs e)
//        //    {
//        //        region.RegionCore = architecture;
//        //        region.RegionCoreID = region.RegionCore != null ? region.RegionCore.ID : -1;
//        //        region.States.Clear();
//        //        foreach (var item in lbStatesList.Items)
//        //        {
//        //            CheckBox checkBox = item as CheckBox;
//        //            State state = checkBox.Content as State;
//        //            if (checkBox.IsChecked != true && state.LinkedRegion == region)
//        //            {
//        //                state.LinkedRegion = null;
//        //                region.States.Remove(state);
//        //            }
//        //            else if (checkBox.IsChecked == true)
//        //            {
//        //                if (state.LinkedRegion != null)
//        //                {
//        //                    state.LinkedRegion.States.Remove(state);
//        //                    state.LinkedRegion.StatesListString = state.LinkedRegion.States.SaveToString();
//        //                }
//        //                state.LinkedRegion = region;
//        //                region.States.Add(state);
//        //            }
//        //        }
//        //        if (!edit && region != null)
//        //        {
//        //            scen.Regions.Add(region);
//        //        }
//        //        region.StatesListString = region.States.SaveToString();
//        //        StateTab state = new StateTab();
//        //        state.ind();
//        //        if (!settingUp)
//        //        {
//        //            new StateTab(dgstate, scen).setup();
//        //            initdt();
//        //        }

//        //        win.Close();
//        //    }
//        //    Button btExit = new Button() { HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness(0, 0, 20, 20), Width = 75, Height = 25, Content = "直接退出" };
//        //    btExit.Click += BtExit_Click;
//        //    void BtExit_Click(object sender, RoutedEventArgs e)
//        //    {
//        //        win.Close();
//        //    }
//        //    grid.Children.Add(btSetRegionCore);
//        //    grid.Children.Add(lbRegionCore);
//        //    grid.Children.Add(btReSetRegionCore);
//        //    grid.Children.Add(lbPresentationChoice);
//        //    grid.Children.Add(lbStatesList);
//        //    grid.Children.Add(btSave);
//        //    grid.Children.Add(btExit);
//        //    win.ShowDialog();
//        //}
//    }
        
//        //{
//    //    protected override IItemList GetDataList(GameScenario scen)
//    //    {
//    //        return new listItemList(scen.PersonRelationIds);
//    //    }

//    //    protected override Dictionary<string, string> GetDefaultValues()
//    //    {
//    //        return new Dictionary<string, string>()
//    //        {

//    //        };
//    //    }

//    //    protected override string[] GetRawItemOrder()
//    //    {
//    //        return new String[]
//    //        {
//    //            "ID",
//    //            "Kind",
//    //            "Name",
//    //            "Parameter",
//    //            "Parameter2",
//    //        };
//    //    }

//    //    protected override Dictionary<String, String> GetHelpText()
//    //    {
//    //        return new Dictionary<string, string>()
//    //        {
//    //            { "Kind", "对应种类" },
//    //            { "Name",  "名称" },
//    //            {"Parameter", "参数" },
//    //            {"Parameter2","参数2" }
//    //        };
//    //    }

//    //    public PersonRelationsTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
//    //    {
//    //        init(scen, dg, helpTextBlock);
//    //    }
//    //}
//}
