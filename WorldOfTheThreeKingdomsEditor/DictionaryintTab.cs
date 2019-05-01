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
            //dpd.AddValueChanged(dg, dg_ItemsSourceChanged);//针对的是复制粘贴后的情况
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
                if (!settingUp)
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
            this.setup();
        }

    }
}
