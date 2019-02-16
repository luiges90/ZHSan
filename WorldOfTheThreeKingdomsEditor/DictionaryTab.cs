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

        private static bool settingUp = false;

        // dict is write-thru
        public DictionaryTab(Dictionary<K, V> dict, String name, DataGrid dg)
        {
            this.dict = dict;
            this.name = name;
            this.dg = dg;
        }

        public void setup()
        {
            settingUp = true;

            DataTable dt = new DataTable(name);

            dt.Columns.Add("Key", typeof(K));
            dt.Columns.Add("Value", typeof(V));

            foreach (KeyValuePair<K, V> i in dict)
            {
                DataRow row = dt.NewRow();

                row["Key"] = i.Key;
                row["Value"] = i.Value;

                dt.Rows.Add(row);
            }

            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));

            dg.ItemsSource = dt.AsDataView();

            dt.TableNewRow += Dt_TableNewRow;
            dt.RowChanged += Dt_RowChanged;
            dt.RowDeleted += Dt_RowDeleted;

            dpd.AddValueChanged(dg, dg_ItemsSourceChanged);
            settingUp = false;
        }

        private void Dt_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            try
            { 
                dict.Remove((K) e.Row["Key"]);
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
                K key = (K)e.Row["Key"];
                V value = (V)e.Row["Value"];
            
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
            catch (Exception ex)
            {
                MessageBox.Show("資料輸入錯誤。" + ex.Message);
            }
        }

        private void Dt_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            try
            { 
                dict.Add((K)e.Row["Key"], (V)e.Row["Value"]);
            }
            catch (Exception)
            {
                // MessageBox.Show("資料輸入錯誤。" + ex.Message);
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

                K key = (K)item.Row["Key"];
                V value = (V)item.Row["Value"];

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
