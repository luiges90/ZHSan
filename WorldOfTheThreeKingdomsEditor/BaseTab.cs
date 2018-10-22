using GameObjects;
using GameObjects.Conditions;
using GameObjects.Influences;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    public abstract class BaseTab<T> where T : GameObject, new()
    {
        class ItemOrderComparer : IComparer<String>
        {
            private String[] rawItemOrder;
            public ItemOrderComparer(String[] rawItemOrder)
            {
                this.rawItemOrder = rawItemOrder;
            }

            Dictionary<String, int> order;
            public int Compare(string x, string y)
            {
                if (order == null)
                {
                    order = new Dictionary<string, int>();
                    int i = 0;
                    foreach (String s in rawItemOrder)
                    {
                        order.Add(s, i);
                        i++;
                    }
                }
                int xi, yi;
                if (!order.TryGetValue(x, out xi))
                {
                    xi = int.MaxValue;
                }
                if (!order.TryGetValue(y, out yi))
                {
                    yi = int.MaxValue;
                }
                return xi - yi;
            }
        }

        protected Type[] supportedTypes = new Type[]
        {
            typeof(bool),
            typeof(byte),
            typeof(short),
            typeof(int),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(char),
            typeof(string),
            typeof(ConditionKind),
            typeof(InfluenceKind),
            typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind),
            typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind)
        };

        private readonly T sampleInstance = Activator.CreateInstance<T>();

        private static bool settingUp = false;

        protected FieldInfo[] getFieldInfos()
        {
            return sampleInstance.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x, typeof(DataMemberAttribute)))
                .Where(x => supportedTypes.Contains(x.FieldType) || x.FieldType.IsEnum)
                .ToArray();
        }

        protected PropertyInfo[] getPropertyInfos()
        {
            return sampleInstance.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x, typeof(DataMemberAttribute)))
                .Where(x => supportedTypes.Contains(x.PropertyType) || x.PropertyType.IsEnum)
                .ToArray();
        }

        private GameScenario scen;
        private DataGrid dg;

        protected void init(GameScenario scen, DataGrid dg)
        {
            this.scen = scen;
            this.dg = dg;
        }

        protected abstract String[] GetRawItemOrder();

        protected abstract Dictionary<String, String> GetDefaultValues();

        protected abstract GameObjectList GetDataList(GameScenario scen);

        public void setup()
        {
            settingUp = true;

            DataTable dt = new DataTable(sampleInstance.GetType().Name);

            FieldInfo[] fields = getFieldInfos();
            PropertyInfo[] properties = getPropertyInfos();

            MemberInfo[] items = new MemberInfo[fields.Length + properties.Length];
            items = items.Union(fields).Union(properties).OrderBy(x => x == null ? "" : x.Name, new ItemOrderComparer(GetRawItemOrder())).ToArray();

            foreach (MemberInfo i in items)
            {
                if (i == null) continue;

                String name = i.Name;
                Type type;
                if (i is FieldInfo)
                {
                    type = ((FieldInfo)i).FieldType;
                }
                else
                {
                    type = ((PropertyInfo)i).PropertyType;
                }

                /*
                if (type.Name == "Nullable`1")
                {
                    type = type.GenericTypeArguments[0];
                }*/
                String defaultValue;
                if (!GetDefaultValues().TryGetValue(name, out defaultValue))
                {
                    if (type == typeof(string))
                    {
                        defaultValue = "";
                    }
                    else
                    {
                        defaultValue = "0";
                    }
                }

                if (type.IsEnum)
                {
                    DataColumn col = new DataColumn(name, 1.GetType());
                    col.DefaultValue = 0;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(InfluenceKind))
                {
                    DataColumn col = new DataColumn(name, 1.GetType());
                    col.DefaultValue = 0;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(ConditionKind))
                {
                    DataColumn col = new DataColumn(name, 1.GetType());
                    col.DefaultValue = 0;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind))
                {
                    DataColumn col = new DataColumn(name, 1.GetType());
                    col.DefaultValue = 0;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind))
                {
                    DataColumn col = new DataColumn(name, 1.GetType());
                    col.DefaultValue = 0;
                    dt.Columns.Add(col);
                }
                else
                {
                    DataColumn col = new DataColumn(name, type);
                    col.DefaultValue = defaultValue;
                    dt.Columns.Add(col);
                }
            }

            foreach (T p in GetDataList(scen))
            {
                DataRow row = dt.NewRow();

                foreach (FieldInfo i in fields)
                {
                    if (i.FieldType.IsEnum)
                    {
                        row[i.Name] = (int) i.GetValue(p);
                    }
                    else if (i.FieldType == typeof(InfluenceKind))
                    {
                        row[i.Name] = ((InfluenceKind)i.GetValue(p)).ID;
                    }
                    else if (i.FieldType == typeof(ConditionKind))
                    {
                        row[i.Name] = ((ConditionKind)i.GetValue(p)).ID;
                    }
                    else if (i.FieldType == typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind))
                    {
                        row[i.Name] = ((GameObjects.ArchitectureDetail.EventEffect.EventEffectKind)i.GetValue(p)).ID;
                    }
                    else if (i.FieldType == typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind))
                    {
                        row[i.Name] = ((GameObjects.TroopDetail.EventEffect.EventEffectKind)i.GetValue(p)).ID;
                    }
                    else
                    {
                        row[i.Name] = i.GetValue(p);
                    }
                }
                foreach (PropertyInfo i in properties)
                {
                    try
                    {
                        if (i.PropertyType.IsEnum)
                        {
                            row[i.Name] = (int)i.GetValue(p);
                        }
                        else if (i.PropertyType == typeof(InfluenceKind))
                        {
                            row[i.Name] = ((InfluenceKind)i.GetValue(p)).ID;
                        }
                        else if (i.PropertyType == typeof(ConditionKind))
                        {
                            row[i.Name] = ((ConditionKind)i.GetValue(p)).ID;
                        }
                        else if (i.PropertyType == typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind))
                        {
                            row[i.Name] = ((GameObjects.ArchitectureDetail.EventEffect.EventEffectKind)i.GetValue(p)).ID;
                        }
                        else if (i.PropertyType == typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind))
                        {
                            row[i.Name] = ((GameObjects.TroopDetail.EventEffect.EventEffectKind)i.GetValue(p)).ID;
                        }
                        else
                        {
                            row[i.Name] = i.GetValue(p) ?? DBNull.Value;
                        }
                    }
                    catch
                    {
                        row[i.Name] = DBNull.Value;
                    }
                }

                dt.Rows.Add(row);
            }

            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));

            dg.ItemsSource = dt.AsDataView();

            dt.TableNewRow += Dt_TableNewRow;
            dt.RowChanged += Dt_RowChanged;
            dt.RowDeleting += Dt_RowDeleting;

            dpd.AddValueChanged(dg, dg_ItemsSourceChanged);
            settingUp = false;
        }

        private void Dt_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            GameObjectList list = (GameObjectList)GetDataList(scen);
            T p = (T)list.GetGameObject((int)e.Row["id"]);
            list.Remove(p);
        }

        private void Dt_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                T p = (T)((GameObjectList)GetDataList(scen)).GetGameObject((int)e.Row["id"]);

                FieldInfo[] fields = getFieldInfos();
                PropertyInfo[] properties = getPropertyInfos();

                foreach (FieldInfo i in fields)
                {
                    if (i.FieldType.IsEnum)
                    {
                        i.SetValue(p, (Int32) Enum.ToObject(i.FieldType, e.Row[i.Name]));
                    }
                    else if (i.FieldType == typeof(InfluenceKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllInfluenceKinds.GetInfluenceKind((int) e.Row[i.Name]));
                    }
                    else if (i.FieldType == typeof(ConditionKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllConditionKinds.GetConditionKind((int)e.Row[i.Name]));
                    }
                    else if (i.FieldType == typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllEventEffectKinds.GetEventEffectKind((int)e.Row[i.Name]));
                    }
                    else if (i.FieldType == typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllTroopEventEffectKinds.GetEventEffectKind((int)e.Row[i.Name]));
                    }
                    else
                    {
                        if (e.Row[i.Name] != DBNull.Value)
                        {
                            i.SetValue(p, e.Row[i.Name]);
                        }
                    }
                }
                foreach (PropertyInfo i in properties)
                {
                    if (i.PropertyType.IsEnum)
                    {
                        i.SetValue(p, (Int32) Enum.ToObject(i.PropertyType, e.Row[i.Name]));
                    }
                    else if (i.PropertyType == typeof(InfluenceKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllInfluenceKinds.GetInfluenceKind((int)e.Row[i.Name]));
                    }
                    else if (i.PropertyType == typeof(ConditionKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllConditionKinds.GetConditionKind((int)e.Row[i.Name]));
                    }
                    else if (i.PropertyType == typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllEventEffectKinds.GetEventEffectKind((int)e.Row[i.Name]));
                    }
                    else if (i.PropertyType == typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllTroopEventEffectKinds.GetEventEffectKind((int)e.Row[i.Name]));
                    }
                    else
                    {
                        if (e.Row[i.Name] != DBNull.Value)
                        {
                            i.SetValue(p, e.Row[i.Name]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("資料輸入錯誤。" + ex.Message);
            }
        }

        private void Dt_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            T p = Activator.CreateInstance<T>();

            GameObjectList list = (GameObjectList)GetDataList(scen);
            int id = list.GetFreeGameObjectID();
            e.Row["id"] = id;
            p.ID = id;
            list.Add(p);
        }

        private void dg_ItemsSourceChanged(object sender, EventArgs e)
        {
            if (settingUp) return;

            DataGrid dataGrid = (DataGrid)sender;
            GameObjectList list = (GameObjectList)GetDataList(scen);

            for (int r = 0; r < dataGrid.Items.Count; r++)
            {
                DataRowView item = dataGrid.Items[r] as DataRowView;
                if (item == null)
                {
                    continue;
                }

                int id = (int) item["id"];
                T p = (T) list.GetGameObject(id);
                if (p == null)
                {
                    p = Activator.CreateInstance<T>();
                    list.Add(p);
                }
                FieldInfo[] fields = getFieldInfos();
                PropertyInfo[] properties = getPropertyInfos();

                foreach (FieldInfo i in fields)
                {
                    if (i.FieldType.IsEnum)
                    {
                        i.SetValue(p, (Int32) Enum.ToObject(i.FieldType, item[i.Name]));
                    }
                    else if (i.FieldType == typeof(InfluenceKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllInfluenceKinds.GetInfluenceKind((int)item[i.Name]));
                    }
                    else if (i.FieldType == typeof(ConditionKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllConditionKinds.GetConditionKind((int)item[i.Name]));
                    }
                    else if (i.FieldType == typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllEventEffectKinds.GetEventEffectKind((int)item[i.Name]));
                    }
                    else if (i.FieldType == typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllTroopEventEffectKinds.GetEventEffectKind((int)item[i.Name]));
                    }
                    else
                    {
                        if (item[i.Name] != DBNull.Value)
                        {
                            i.SetValue(p, item[i.Name]);
                        }
                    }
                }
                foreach (PropertyInfo i in properties)
                {
                    if (i.PropertyType.IsEnum)
                    {
                        i.SetValue(p, (Int32) Enum.ToObject(i.PropertyType, item[i.Name]));
                    }
                    else if (i.PropertyType == typeof(InfluenceKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllInfluenceKinds.GetInfluenceKind((int)item[i.Name]));
                    }
                    else if (i.PropertyType == typeof(ConditionKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllConditionKinds.GetConditionKind((int)item[i.Name]));
                    }
                    else if (i.PropertyType == typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllEventEffectKinds.GetEventEffectKind((int)item[i.Name]));
                    }
                    else if (i.PropertyType == typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllTroopEventEffectKinds.GetEventEffectKind((int)item[i.Name]));
                    }
                    else
                    {
                        if (item[i.Name] != DBNull.Value)
                        {
                            i.SetValue(p, item[i.Name]);
                        }
                    }
                }
            }
        }

    }
}