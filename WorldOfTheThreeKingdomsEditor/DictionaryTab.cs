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
    class DictionaryTab<K, V>
    {
        private Dictionary<K, V> dict;
        private String name;
        private DataGrid dg;
        private GameScenario scen;
        private static bool settingUp = false;
        private DataTable dt;
        // dict is write-thru
        public DictionaryTab(Dictionary<K, V> dict, String name, DataGrid dg,GameScenario scen)
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
            dt.Columns.Add("ID", typeof(K));
            dt.Columns["ID"].ReadOnly = true;
            dt.Columns.Add("武将名称" );
            dt.Columns["武将名称"].ReadOnly = true;
            dt.Columns.Add("对方武将ID", typeof(V));
            dt.Columns.Add("对方武将名称");
            dt.Columns["对方武将名称"].ReadOnly = true;
            initdt();

            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = dt.Columns["ID"];
            dt.PrimaryKey = PrimaryKeyColumns;
            dg.CanUserAddRows = false;
            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));

            dg.ItemsSource = dt.AsDataView();

            dt.TableNewRow += Dt_TableNewRow;
            dg.BeginningEdit += Dg_BeginningEdit;
            dt.RowChanged += Dt_RowChanged;//值变更后执行
            dt.RowDeleting += Dt_RowDeleting;
            dpd.AddValueChanged(dg, dg_ItemsSourceChanged);
        }

        private void initdt()
        {
            settingUp = true;
            dt.Clear();
            foreach (KeyValuePair<K, V> i in dict)
            {
                DataRow row = dt.NewRow();
                int.TryParse(i.Key.ToString(), out int n1);
                Person p1 = scen.Persons.GetGameObject(n1) as Person;
                int.TryParse(i.Value.ToString(), out int n2);
                Person p2 = scen.Persons.GetGameObject(n2) as Person;
                row["ID"] = i.Key;
                row["武将名称"] = p1 != null ? p1.Name : "";
                row["对方武将ID"] = i.Value;
                row["对方武将名称"] = p2 != null ? p2.Name : "";
                dt.Rows.Add(row);
            }
            settingUp = false;
        }
        private int oldID = -1;
        private void Dg_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (e.Column.Header.Equals("ID"))
            {
                MessageBox.Show("请不要轻易直接修改武将ID，如其他相关编号未修改完全很可能造成跳出");
                oldID = int.Parse((e.Column.GetCellContent(e.Row) as TextBlock).Text);
            }
        }
        private void Dt_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                dict.Remove((K)e.Row["ID"]);
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
                    K key = (K)e.Row["ID"];
                    V value = (V)e.Row["对方武将ID"];
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

        private void Dt_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            try
            {
                dict.Add((K)e.Row["ID"], (V)e.Row["对方武将ID"]);
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

                K key = (K)item.Row["ID"];
                V value = (V)item.Row["对方武将ID"];
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
        }

    }
}
