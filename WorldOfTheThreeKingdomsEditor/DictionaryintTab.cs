using GameObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace WorldOfTheThreeKingdomsEditor
{
    class DictionaryintTab
    {
        private Dictionary<int, int[]> dict;
        private String name;
        private DataGrid dg;
        private DataTable dt = new DataTable();
        private GameScenario scen;
        private static bool settingUp = false;

        // dict is write-thru
        public DictionaryintTab(Dictionary<int, int[]> dict, String name, DataGrid dg,GameScenario scen)
        {
            this.scen = scen;
            this.dict = dict;
            this.name = name;
            this.dg = dg;
        }

        public void setup()
        {
            settingUp = true;
            dg.ItemsSource = null;
            dt = new DataTable(name);
            dt.Columns.Add("武将ID",typeof(int));
            dt.Columns["武将ID"].ReadOnly = true;
            dt.Columns.Add("武将名称");
            dt.Columns["武将名称"].ReadOnly = true;
            dt.Columns.Add("武将IDs，不同武将间用空格格开");
            dt.Columns.Add("对方武将名称");
            dt.Columns["对方武将名称"].ReadOnly = true;
            initdt();
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = dt.Columns["武将ID"];
            dt.PrimaryKey = PrimaryKeyColumns;
            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            dg.CanUserAddRows = false;
            dg.ItemsSource = dt.AsDataView();

            dg.BeginningEdit += Dg_BeginningEdit;
            dt.RowChanged += Dt_RowChanged;
            dt.RowDeleting += Dt_RowDeleting;
            dpd.AddValueChanged(dg, dg_ItemsSourceChanged);//针对的是复制粘贴后的情况
        }

        private void initdt()
        {
            settingUp = true;
            dt.Clear();
            foreach (KeyValuePair<int, int[]> i in dict)
            {
                DataRow row = dt.NewRow();
                int.TryParse(i.Key.ToString(), out int n1);
                Person p1 = scen.Persons.GetGameObject(n1) as Person;
                string sname = "";
                for (int ii = 0; ii < i.Value.Length; ii++)
                {
                    Person p2 = scen.Persons.GetGameObject(i.Value[ii]) as Person;
                    string ssss = p2 != null ? p2.Name + " " : "";
                    sname = sname + ssss;
                }
                row["武将ID"] = i.Key;
                row["武将名称"] = p1 != null ? p1.Name : "";
                row["武将IDs，不同武将间用空格格开"] = GameGlobal.StaticMethods.SaveToString(i.Value);
                row["对方武将名称"] = sname;
                dt.Rows.Add(row);
            }
            settingUp = false;
        }
        private int oldID = -1;
        private void Dg_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (e.Column.Header.Equals("武将ID"))
            {
                MessageBox.Show("请不要轻易直接修改武将ID，如其他相关编号未修改完全很可能造成跳出");
                oldID = int.Parse((e.Column.GetCellContent(e.Row) as TextBlock).Text);
            }
        }
        private void Dt_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                dict.Remove((int)e.Row["武将ID"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("資料輸入錯誤。" + ex.Message);
            }
        }
        
        private void Dt_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                if (!settingUp && !MainWindow.pasting)
                {
                    int key = int.Parse(e.Row["武将ID"].ToString());
                    GameGlobal.StaticMethods.LoadFromString(out int[] value, e.Row["武将IDs，不同武将间用空格格开"].ToString());
                    if (dict.ContainsKey(key))
                    {
                        dict[key] = value;
                    }
                    else
                    {
                        dict.Remove(key);
                        dict.Add(key, value);
                    }
                    initdt();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("資料輸入錯誤。" + ex.Message);
            }
        }


        private void dg_ItemsSourceChanged(object sender, EventArgs e)
        {
            if (settingUp) return;
            DataGrid dataGrid = (DataGrid)sender;
            for (int r = 0; r < dataGrid.Items.Count; r++)
            {
                DataRowView item = dataGrid.Items[r] as DataRowView;
                if (item == null)
                {
                    continue;
                }
                int key = (int)(item["武将ID"]);
                GameGlobal.StaticMethods.LoadFromString(out int[] value, item["武将IDs，不同武将间用空格格开"].ToString());
                if (dict.ContainsKey(key))
                {
                    dict[key] = value;
                }
                else
                {
                    dict.Remove(key);
                    dict.Add(key, value);
                }
            }
            MainWindow.pasting = false;
        }

        public void creatWindow(bool edit, DataGrid datagrid0)
        {
            if (!settingUp)
            {
                int p1ID = -1;
                string p2IDs = "";
                if (edit)
                {
                    p1ID = int.Parse(((DataRowView)datagrid0.SelectedItem).Row["武将ID"].ToString());
                    p2IDs = ((DataRowView)datagrid0.SelectedItem).Row["武将IDs，不同武将间用空格格开"].ToString();
                }

                int temp1 = p1ID;
                string tempP2IDs = p2IDs;
                //int temp2 = p2ID;
                Window win = new Window();
                win.Title = "编辑武将关系";
                if (!edit)
                {
                    win.Title = "新增武将关系";
                }
                win.Width = 500;
                win.Height = 250;
                win.ResizeMode = ResizeMode.NoResize;
                Grid grid = new Grid();
                win.Content = grid;
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                Person p1 = scen.Persons.GetGameObject(p1ID) as Person;
                string p1name = p1 == null ? "" : p1.Name;
                string p2names = "";
                GameObjectList list = new GameObjectList();

                char[] separator = new char[] { ' ', '\n', '\r', '\t' };
                string[] strArray = p2IDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strArray.Length; i++)
                {
                    Person p2 = scen.Persons.GetGameObject(int.Parse(strArray[i])) as Person;
                    if (p2 != null)
                    {
                        list.Add(p2);
                        p2names = p2names + p2.Name + " ";
                    }
                }
                string tempP1name = p1name;
                string tempP2names = p2names;

                Label labelP1ID = new Label { Content = "武将ID:   " + p1ID, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
                Label labelP1 = new Label { Content = "武将名称:   " + p1name, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
                Button buttonperson = new Button() { Width = 75, Height = 30, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Content = p1name, IsEnabled = !edit };
                Label labelP2ID = new Label { Content = "对方武将ID:   " + p2IDs, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
                Label labelP2 = new Label { Content = "对方武将名称:   " + p2names, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
                Button button2person = new Button() { Width = 75, Height = 30, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Content = p2names };
                buttonperson.Click += Buttonperson_Click;
                button2person.Click += Button2person_Click;
                void Buttonperson_Click(object sender, RoutedEventArgs e)
                {
                    Window window = new Window();
                    window.Title = "请选择武将--双击确认";
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    window.Width = 800;
                    window.Height = 600;
                    Grid grid2 = new Grid();
                    window.Content = grid2;
                    grid2.Margin = new Thickness(50);
                    ListBox listBox = new ListBox();
                    DataGrid dataGrid1 = new DataGrid();
                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("ID", typeof(int));
                    dt2.Columns.Add("姓名");
                    dt2.Columns.Add("性别");
                    dt2.Columns.Add("所在");
                    dt2.Columns.Add("所属势力");
                    dt2.Columns.Add("武学");
                    dt2.Columns.Add("将略");
                    dt2.Columns.Add("谋略");
                    dt2.Columns.Add("政理");
                    dt2.Columns.Add("风度");
                    DataRow dr1 = dt2.NewRow();
                    dr1["ID"] = -1;
                    dr1["姓名"] = "无";
                    dt2.Rows.Add(dr1);
                    foreach (Person person in scen.Persons)
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
                    dataGrid1.ItemsSource = dt2.DefaultView;
                    dataGrid1.MouseDoubleClick += dataGrid1_MouseDoubleClick;
                    void dataGrid1_MouseDoubleClick(object sender2, MouseButtonEventArgs e2)
                    {
                        if (dataGrid1.SelectedItem != null)
                        {
                            DataTable dt5 = ((DataView)dataGrid1.ItemsSource).ToTable();
                            p1ID = int.Parse(dt5.Rows[dataGrid1.SelectedIndex]["ID"].ToString());
                            window.Close();
                            p1 = scen.Persons.GetGameObject(p1ID) as Person;
                            p1name = p1 == null ? "" : p1.Name;
                            labelP1ID.Content = "武将ID:   " + p1ID;
                            labelP1.Content = "武将名称:   " + p1name;
                            buttonperson.Content = p1name;
                        }
                    }
                    dataGrid1.IsReadOnly = true;
                    grid2.Children.Add(dataGrid1);
                    dataGrid1.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
                    window.ShowDialog();
                }
                void Button2person_Click(object sender, RoutedEventArgs e)
                {
                    list = new GameObjectList();
                    strArray = p2IDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        Person p2 = scen.Persons.GetGameObject(int.Parse(strArray[i])) as Person;
                        if (p2 != null)
                        {
                            list.Add(p2);
                        }
                    }
                    //string idssss = "";
                    //string namessss = "";
                    Window window = new Window();
                    string titleplugin = "   注意:可以选择多个";
                    window.Title = "请选择武将-勾选后点击确认" + titleplugin; ;
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    window.Width = 800;
                    window.Height = 600;
                    Grid grid2 = new Grid();
                    window.Content = grid2;
                    grid2.Margin = new Thickness(50);
                    ListBox listBox = new ListBox();
                    DataGrid dataGrid1 = new DataGrid();
                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("选择", typeof(bool));
                    dt2.Columns.Add("ID", typeof(int));
                    dt2.Columns.Add("姓名");
                    dt2.Columns.Add("性别");
                    dt2.Columns.Add("所在");
                    dt2.Columns.Add("所属势力");
                    dt2.Columns.Add("武学");
                    dt2.Columns.Add("将略");
                    dt2.Columns.Add("谋略");
                    dt2.Columns.Add("政理");
                    dt2.Columns.Add("风度");
                    foreach (Person person in scen.Persons)
                    {
                        DataRow dr = dt2.NewRow();
                        dr["选择"] = false;
                        if (list.HasGameObject(person))
                        {
                            dr["选择"] = true;
                        }
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
                    dataGrid1.CanUserAddRows = false;
                    foreach (DataColumn column in dt2.Columns)
                    {
                        if (!column.ColumnName.Equals("选择"))
                        {
                            column.ReadOnly = true;
                        }
                    }
                    dataGrid1.ItemsSource = dt2.DefaultView;
                    dataGrid1.MouseLeftButtonUp += DataGrid1_MouseLeftButtonUp;
                    void DataGrid1_MouseLeftButtonUp(object sender2, MouseButtonEventArgs e2)
                    {
                        if (!bool.Parse(((DataRowView)dataGrid1.SelectedItem).Row["选择"].ToString()))
                        {
                            ((DataRowView)dataGrid1.SelectedItem).Row["选择"] = true;
                        }
                        else if (bool.Parse(((DataRowView)dataGrid1.SelectedItem).Row["选择"].ToString()))
                        {
                            ((DataRowView)dataGrid1.SelectedItem).Row["选择"] = false;
                        }
                    }
                    Button buttonsave2 = new Button() { Width = 50, Height = 25, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Bottom, Content = "确定" };
                    buttonsave2.Click += Buttonsave2_Click;
                    void Buttonsave2_Click(object sender2, RoutedEventArgs e2)
                    {
                        DataTable dataTable = ((DataView)dataGrid1.ItemsSource).ToTable();
                        int n = 0;
                        string idssss = "";
                        string namessss = "";
                        foreach (DataRow row in dataTable.Rows)
                        {
                            if (bool.Parse(row["选择"].ToString()))
                            {
                                idssss = idssss + row["ID"].ToString() + " ";
                                namessss = namessss + row["姓名"].ToString() + " ";
                                n++;
                            }
                        }
                        p2IDs = idssss;
                        p2names = namessss;
                        window.Close();
                        labelP2ID.Content = "武将ID:   " + p2IDs;
                        labelP2.Content = "武将名称:   " + p2names;
                        button2person.Content = p2names;
                    }

                    Button buttonexit2 = new Button() { Width = 50, Height = 25, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Bottom, Content = "取消" };
                    buttonexit2.Click += Buttonexit2_Click;
                    void Buttonexit2_Click(object sender2, RoutedEventArgs e2)
                    {
                        window.Close();
                    }

                    dataGrid1.Margin = new Thickness(0, 0, 0, 30);
                    grid2.Children.Add(dataGrid1);
                    grid2.Children.Add(buttonsave2);
                    grid2.Children.Add(buttonexit2);
                    dataGrid1.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
                    window.ShowDialog();
                }

                Button buttonBack = new Button() { Width = 75, Height = 30, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Content = "还原初始设置" };
                buttonBack.Click += ButtonBack_Click;
                void ButtonBack_Click(object sender, RoutedEventArgs e)
                {
                    p1ID = temp1;
                    p1name = tempP1name;
                    p2IDs = tempP2IDs;
                    p2names = tempP2names;

                    labelP1ID.Content = "武将ID:   " + p1ID;
                    labelP1.Content = "武将名称:   " + p1name;
                    buttonperson.Content = p1name;
                    labelP2ID.Content = "武将ID:   " + p2IDs;
                    labelP2.Content = "武将名称:   " + p2names;
                    button2person.Content = p2names;
                }
                Button buttonSave = new Button() { Width = 75, Height = 30, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Content = "保存退出" };
                void dicclose1(int P1ID, int P2ID, Dictionary<int, int> dict, Window window)
                {
                    if (P1ID != -1)
                    {
                        if (dict.ContainsKey(P1ID))
                        {
                            dict[P1ID] = P2ID;
                        }
                        else
                        {
                            dict.Remove(P1ID);
                            dict.Add(P1ID, P2ID);
                        }
                    }
                    window.Close();
                }
                void dicclose2(int P1ID, int[] P2IDs, Dictionary<int, int[]> dict, Window window)
                {
                    if (P1ID != -1)
                    {
                        if (dict.ContainsKey(P1ID))
                        {
                            dict[P1ID] = P2IDs;
                        }
                        else
                        {
                            dict.Remove(P1ID);
                            dict.Add(P1ID, P2IDs);
                        }
                    }
                    window.Close();
                }
                buttonSave.Click += ButtonSave_Click;
                void ButtonSave_Click(object sender, RoutedEventArgs e)
                {
                    Dictionary<int, int[]> dict = new Dictionary<int, int[]>();
                    GameGlobal.StaticMethods.LoadFromString(out int[] value, p2IDs);
                    if (datagrid0.Name == "dgBrotherIds")
                    {
                        dict = scen.BrotherIds;
                        dicclose2(p1ID, value, dict, win);
                    }
                    else if (datagrid0.Name == "dgSuoshuIds")
                    {
                        dict = scen.SuoshuIds;
                        dicclose2(p1ID, value, dict, win);
                    }
                    else if (datagrid0.Name == "dgCloseIds")
                    {
                        dict = scen.CloseIds;
                        dicclose2(p1ID, value, dict, win);
                    }
                    else if (datagrid0.Name == "dgHatedIds")
                    {
                        dict = scen.HatedIds;
                        dicclose2(p1ID, value, dict, win);
                    }
                    setup();
                }
                Button buttonExit = new Button() { Width = 75, Height = 30, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Content = "直接退出" };
                buttonExit.Click += ButtonExit_Click;
                void ButtonExit_Click(object sender, RoutedEventArgs e)
                {
                    win.Close();
                }

                grid.Children.Add(labelP1ID);
                grid.Children.Add(labelP1);
                grid.Children.Add(buttonperson);
                grid.Children.Add(labelP2ID);
                grid.Children.Add(labelP2);
                grid.Children.Add(button2person);
                grid.Children.Add(buttonBack);
                grid.Children.Add(buttonSave);
                grid.Children.Add(buttonExit);
                Grid.SetColumn(labelP1ID, 0);
                Grid.SetRow(labelP1ID, 0);
                Grid.SetColumn(labelP1, 0);
                Grid.SetRow(labelP1, 1);
                Grid.SetColumn(buttonperson, 0);
                Grid.SetRow(buttonperson, 2);
                Grid.SetColumn(buttonBack, 1);
                Grid.SetRow(buttonBack, 0);
                Grid.SetColumn(buttonSave, 1);
                Grid.SetRow(buttonSave, 1);
                Grid.SetColumn(buttonExit, 1);
                Grid.SetRow(buttonExit, 2);
                Grid.SetColumn(labelP2ID, 2);
                Grid.SetRow(labelP2ID, 0);
                Grid.SetColumn(labelP2, 2);
                Grid.SetRow(labelP2, 1);
                Grid.SetColumn(button2person, 2);
                Grid.SetRow(button2person, 2);
                win.ShowDialog();
            }
        }

    }
}
