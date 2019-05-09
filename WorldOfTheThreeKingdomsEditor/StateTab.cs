using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GameObjects.ArchitectureDetail;
using System.Data;
using System.Windows;
using System.ComponentModel;
using System.Windows.Media;

namespace WorldOfTheThreeKingdomsEditor
{
    class StateTab
    {
        private DataGrid dg;
        private GameScenario scen;
        private bool settingUp = false;
        private DataTable dt;
        private MainWindow mainWindow;
        // dict is write-thru
        public StateTab(DataGrid dg, GameScenario scen)
        {
            this.scen = scen;
            this.dg = dg;
        }

        //private void initstates

        public void setup()
        {
            dg.ItemsSource = null;
            dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("名称");
            dt.Columns.Add("所属地区");
            dt.Columns.Add("州治所");
            dt.Columns.Add("连接州域");
            dt.Columns["所属地区"].ReadOnly = true;
            dt.Columns["州治所"].ReadOnly = true;
            dt.Columns["连接州域"].ReadOnly = true;
            initdt();

            DataColumn[] PrimaryKeyColumns = new DataColumn[1];//声明了一个datacolumn的数组，该数组只有一个元素 
            PrimaryKeyColumns[0] = dt.Columns["ID"];//把主键列赋给数组元素 
            dt.PrimaryKey = PrimaryKeyColumns;//指定表的主键为PrimaryKeyColumns主键数组 
            dg.CanUserAddRows = false;
            //dg.CanUserSortColumns = false;
            dg.ItemsSource = dt.AsDataView();

            dg.BeginningEdit += Dg_BeginningEdit;
            dt.RowChanged += Dt_RowChanged;//值变更后执行
            dt.RowDeleting += Dt_RowDeleting;
        }

        private void Dg_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (e.Column.Header.Equals("ID"))
            {
                oldID = -1;
                MessageBox.Show("请不要轻易修改ID，如其他相关编号未修改完全很可能造成跳出");
                oldID = int.Parse((e.Column.GetCellContent(e.Row) as TextBlock).Text);
            }
        }

        private void initdt()
        {
            settingUp = true;
            if (dt != null)
            {
                dt.Clear();
            }
            foreach (State state in scen.States)
            {
                DataRow row = dt.NewRow();
                row["ID"] = state.ID;
                row["名称"] = state.Name;
                row["所属地区"] = state.LinkedRegionString;
                row["州治所"] = state.StateAdminString;
                row["连接州域"] = state.ContactStatesDisplayString;
                dt.Rows.Add(row);
            }
            settingUp = false;
        }

        private void Dt_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                State state = scen.States.GetGameObject((int)e.Row["ID"]) as State;
                //foreach (State state in region.States)
                //{
                //    state.LinkedRegion = null;
                //    region.States.Remove(state);
                //}
                scen.Regions.Remove(state);
            }
            catch (Exception ex)
            {
                MessageBox.Show("資料輸入錯誤。" + ex.Message);
            }
        }
        private int oldID = -1;

        private void Dt_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                if (!settingUp)
                {
                    if (oldID == -1)
                    {
                        oldID = (int)e.Row["id"];
                    }
                    State state = scen.States.GetGameObject(oldID) as State;
                    state.ID = (int)e.Row["ID"];
                    oldID = -1;
                    state.Name = e.Row["名称"].ToString();
                    initdt();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("資料輸入錯誤。" + ex.Message);
            }
        }

        public void creatWindow(bool edit, DataGrid dgregion, MainWindow mainWindow)
        {
            if (!settingUp)
            {
                this.mainWindow = mainWindow;
                Window win = new Window();
                win.Title = "修改州域信息";
                win.Width = 300;
                win.Height = 400;
                win.ResizeMode = ResizeMode.NoResize;
                win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                Grid grid = new Grid() { Background = Brushes.WhiteSmoke };
                win.Content = grid;
                State state = new State();
                state.ID = scen.States.GetFreeGameObjectID();
                state.LinkedRegion = null;
                state.StateAdmin = null;
                if (edit)
                {
                    state = scen.States.GetGameObject(int.Parse(((DataRowView)dg.SelectedItem).Row["ID"].ToString())) as State;
                }
                UserControl1_labeltext textname = new UserControl1_labeltext() { Margin = new Thickness(20, 10, 0, 0), Title ="州名",Text=state.Name ,Height=25,Width=180, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
                grid.Children.Add(textname);
                Label lbPresentationLinkedRegion = new Label() { Margin = new Thickness(20, 50, 0, 0), Content = "所属地区", FontSize = 13 };
                ComboBox cbLinkedRegion = new ComboBox() { Margin = new Thickness(0, 50, 0, 0), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top, Width = 75, Height = 25 };
                foreach (Region re in scen.Regions)
                {
                    cbLinkedRegion.Items.Add(re);
                }
                cbLinkedRegion.SelectedItem = state.LinkedRegion;
                cbLinkedRegion.SelectionChanged += CbLinkedRegion_SelectionChanged;
                Region regiontemp = state.LinkedRegion;
                void CbLinkedRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                    regiontemp = cbLinkedRegion.SelectedItem as Region;
                }
                Label lbStateAdmin = new Label() { Margin = new Thickness(110, 80, 0, 0), Content = state.StateAdmin != null ? state.StateAdmin.Name : "" };

                Button btSetStateAdmin = new Button() { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(20, 80, 0, 0), Width = 75, Height = 25, Content = "州治所" };
                btSetStateAdmin.ToolTip = "提示：当州域的州治所其技术值超过其上限50%时，在这个州域内所有的城池，\n友（我）方城池钱粮收入上升20%，敌方势力部队攻防城池钱粮收入下降20%";
                btSetStateAdmin.Click += BtSetStateAdmin_Click;
                Architecture architecture = new Architecture();
                void BtSetStateAdmin_Click(object sender, RoutedEventArgs e)
                {
                    Window winChoicenStateAdmin = new Window();
                    winChoicenStateAdmin.Title = "选择建筑==双击";
                    winChoicenStateAdmin.Width = 600;
                    winChoicenStateAdmin.Height = 600;
                    winChoicenStateAdmin.ResizeMode = ResizeMode.NoResize;
                    winChoicenStateAdmin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    DataGrid dgArchis = new DataGrid() { Margin = new Thickness(5) };
                    DataTable dtArchis = new DataTable();
                    dtArchis.Columns.Add("ID", typeof(int));
                    dtArchis.Columns.Add("名称");
                    dtArchis.Columns.Add("种类");
                    dtArchis.Columns.Add("规模");
                    dtArchis.Columns.Add("所属州");
                    dtArchis.Columns.Add("所属地域");
                    dtArchis.Columns.Add("所属势力");
                    foreach (Architecture a in scen.Architectures)
                    {
                        DataRow dr = dtArchis.NewRow();
                        dr["ID"] = a.ID;
                        dr["名称"] = a.Name;
                        dr["种类"] = a.Kind;
                        dr["规模"] = a.JianzhuGuimo;
                        dr["所属州"] = a.LocationState;
                        dr["所属地域"] = a.LocationState.LinkedRegionString;
                        dr["所属势力"] = a.BelongedFaction;
                        dtArchis.Rows.Add(dr);
                    }
                    dgArchis.ItemsSource = dtArchis.AsDataView();
                    dgArchis.IsReadOnly = true;
                    dgArchis.MouseDoubleClick += Dg_MouseDoubleClick;
                    void Dg_MouseDoubleClick(object sender2, System.Windows.Input.MouseButtonEventArgs e2)
                    {
                        if (dgArchis.SelectedItem != null)
                        {
                            architecture = scen.Architectures.GetGameObject(int.Parse(((DataRowView)dgArchis.SelectedItem).Row["ID"].ToString())) as Architecture;
                            lbStateAdmin.Content = architecture.Name;
                            winChoicenStateAdmin.Close();
                        }
                    }
                    dgArchis.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
                    winChoicenStateAdmin.Content = dgArchis;
                    winChoicenStateAdmin.ShowDialog();
                }
                Button btReSetStateAdmin = new Button() { HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top, Margin = new Thickness(0, 80, 20, 0), Width = 75, Height = 25, Content = "留空" };
                btReSetStateAdmin.Click += BtReSetStateAdmin_Click;
                void BtReSetStateAdmin_Click(object sender, RoutedEventArgs e)
                {
                    architecture = null;
                    lbStateAdmin.Content = "";
                }

                Label lbPresentationChoice = new Label() { Margin = new Thickness(20, 110, 0, 0), Content = "连接州", FontSize = 13 };
                ListBox lbStatesList = new ListBox() { Margin = new Thickness(20, 140, 20, 60) };
                foreach (State state2 in scen.States)
                {
                    if (state2 != state)
                    {
                        CheckBox checkBox = new CheckBox();
                        checkBox.Content = state2;
                        if (state.ContactStates.HasGameObject(state2))
                        {
                            checkBox.IsChecked = true;
                        }
                        lbStatesList.Items.Add(checkBox);
                    }
                }

                Button btSave = new Button() { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness(20, 0, 0, 15), Width = 75, Height = 25, Content = "保存退出" };
                btSave.Click += BtSave_Click;
                void BtSave_Click(object sender, RoutedEventArgs e)
                {
                    if (regiontemp == null)
                    {
                        MessageBox.Show("未选择所属地区，新增失败");
                    }
                    else
                    {
                        state.Name = textname.Text;
                        state.StateAdmin = architecture;
                        state.StateAdminID = state.StateAdmin != null ? state.StateAdmin.ID : -1;
                        if (state.LinkedRegion != null)
                        {
                            state.LinkedRegion.States.Remove(state);
                            state.LinkedRegion.StatesListString = state.LinkedRegion.States.SaveToString();
                        }
                        state.LinkedRegion = regiontemp;
                        state.LinkedRegion.States.Add(state);
                        state.LinkedRegion.StatesListString = state.LinkedRegion.States.SaveToString();
                        state.ContactStates.Clear();
                        foreach (var item in lbStatesList.Items)
                        {
                            CheckBox checkBox = item as CheckBox;
                            if (checkBox.IsChecked == true)
                            {
                                State state2 = checkBox.Content as State;
                                state.ContactStates.Add(state2);
                            }
                        }
                        state.ContactStatesString = state.ContactStates.SaveToString();
                        if (!edit && state != null)
                        {
                            scen.States.Add(state);
                        }
                        if (!settingUp)
                        {
                            mainWindow.initTables(new string[] { "dgRegion" });
                            initdt();
                        }

                        win.Close();
                    }
                }
                Button btExit = new Button() { HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Bottom, Margin = new Thickness(0, 0, 20, 15), Width = 75, Height = 25, Content = "直接退出" };
                btExit.Click += BtExit_Click;
                void BtExit_Click(object sender, RoutedEventArgs e)
                {
                    win.Close();
                }
                grid.Children.Add(lbPresentationLinkedRegion);
                grid.Children.Add(cbLinkedRegion);
                grid.Children.Add(btSetStateAdmin);
                grid.Children.Add(lbStateAdmin);
                grid.Children.Add(btReSetStateAdmin);
                grid.Children.Add(lbPresentationChoice);
                grid.Children.Add(lbStatesList);
                grid.Children.Add(btSave);
                grid.Children.Add(btExit);
                win.ShowDialog();
            }
        }

    }
}
