using GameObjects;
using GameObjects.Conditions;
using GameObjects.Influences;
using GameObjects.PersonDetail;
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

        private Type[] supportedTypes = new Type[]
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
            typeof(TitleKind),
            typeof(List<int>),
            typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind),
            typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind)
        };

        private readonly T sampleInstance = Activator.CreateInstance<T>();

        private static bool settingUp = false;

        private FieldInfo[] getFieldInfos()
        {
            return sampleInstance.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x, typeof(DataMemberAttribute)))
                .Where(x => supportedTypes.Contains(x.FieldType) || x.FieldType.IsEnum)
                .ToArray();
        }

        private PropertyInfo[] getPropertyInfos()
        {
            return sampleInstance.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x, typeof(DataMemberAttribute)))
                .Where(x => supportedTypes.Contains(x.PropertyType) || x.PropertyType.IsEnum)
                .ToArray();
        }

        private GameScenario scen;
        private DataGrid dg;
        private TextBlock helpTextBlock;

        protected void init(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            this.scen = scen;
            this.dg = dg;
            this.helpTextBlock = helpTextBlock;
        }

        protected interface IItemList
        {
            GameObjectList GetList();
            T GetGameObject(int id);
            void Remove(T item);
            void Add(T item);
            int GetFreeGameObjectID();
        }

        protected class GameObjectItemList : IItemList
        {
            private GameObjectList list;

            public GameObjectItemList(GameObjectList l)
            {
                this.list = l;
            }

            public void Add(T item)
            {
                list.Add(item);
            }

            public int GetFreeGameObjectID()
            {
                return list.GetFreeGameObjectID();
            }

            public T GetGameObject(int id)
            {
                if (list.HasGameObject(id))
                {
                    return (T)list.GetGameObject(id);
                }
                else
                {
                    return null;
                }
            }

            public GameObjectList GetList()
            {
                return list;
            }

            public void Remove(T item)
            {
                list.Remove(item);
            }
        }

        protected class GameObjectDictionaryItemList : IItemList
        {
            private Dictionary<int, T> dict;

            public GameObjectDictionaryItemList(Dictionary<int, T> d)
            {
                this.dict = d;
            }

            public void Add(T item)
            {
                dict.Add(item.ID, item);
            }

            public int GetFreeGameObjectID()
            {
                return dict.Keys.Max() + 1;
            }

            public T GetGameObject(int id)
            {
                if (dict.ContainsKey(id))
                {
                    return dict[id];
                }
                else
                {
                    return null;
                }
            }

            public GameObjectList GetList()
            {
                GameObjectList list = new GameObjectList();
                foreach (T t in dict.Values)
                {
                    list.Add(t);
                }
                return list;
            }

            public void Remove(T item)
            {
                dict.Remove(item.ID);
            }
        }

        protected abstract String[] GetRawItemOrder();

        protected abstract Dictionary<String, String> GetDefaultValues();

        protected virtual Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
            };
        }

        protected abstract IItemList GetDataList(GameScenario scen);

        public string getColumnName(string name)
        {
            string helpText;
            if (GetHelpText().TryGetValue(name, out helpText))
            {
                helpText = helpText.Split('。')[0];
                if (helpText.Length <= 0)
                {
                    helpText = name;
                }
            }
            else
            {
                helpText = name;
            }
            return helpText;
        }

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

                string helpText = getColumnName(name);
                if (type.IsEnum)
                {
                    DataColumn col = new DataColumn(name, 1.GetType());
                    col.DefaultValue = 0;
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(InfluenceKind))
                {
                    DataColumn col = new DataColumn(name, 1.GetType());
                    col.DefaultValue = 0;
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(ConditionKind))
                {
                    DataColumn col = new DataColumn(name, 1.GetType());
                    col.DefaultValue = 0;
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(TitleKind))
                {
                    DataColumn col = new DataColumn(name, 1.GetType());
                    col.DefaultValue = 1;
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind))
                {
                    DataColumn col = new DataColumn(name, 1.GetType());
                    col.DefaultValue = 0;
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind))
                {
                    DataColumn col = new DataColumn(name, 1.GetType());
                    col.DefaultValue = 0;
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                } 
                else if (type == typeof(List<int>))
                {
                    DataColumn col = new DataColumn(name, "".GetType());
                    col.DefaultValue = "";
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                }
                else
                {
                    DataColumn col = new DataColumn(name, type);
                    col.DefaultValue = defaultValue;
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                }
            }

            foreach (T p in GetDataList(scen).GetList())
            {
                DataRow row = dt.NewRow();

                foreach (FieldInfo i in fields)
                {
                    string helpText = getColumnName(i.Name);

                    if (i.FieldType.IsEnum)
                    {
                        row[helpText] = (int) i.GetValue(p);
                    }
                    else if (i.FieldType == typeof(InfluenceKind))
                    {
                        row[helpText] = ((InfluenceKind)i.GetValue(p)).ID;
                    }
                    else if (i.FieldType == typeof(ConditionKind))
                    {
                        row[helpText] = ((ConditionKind)i.GetValue(p)).ID;
                    }
                    else if (i.FieldType == typeof(TitleKind))
                    {
                        row[helpText] = ((TitleKind)i.GetValue(p)).ID;
                    }
                    else if (i.FieldType == typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind))
                    {
                        row[helpText] = ((GameObjects.ArchitectureDetail.EventEffect.EventEffectKind)i.GetValue(p)).ID;
                    }
                    else if (i.FieldType == typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind))
                    {
                        row[helpText] = ((GameObjects.TroopDetail.EventEffect.EventEffectKind)i.GetValue(p)).ID;
                    }
                    else if (i.FieldType == typeof(List<int>))
                    {
                        row[helpText] = ((List<int>)i.GetValue(p)).Aggregate<int, string>("", (s, x) => s += x.ToString() + " ");
                    }
                    else
                    {
                        row[helpText] = i.GetValue(p);
                    }
                }
                foreach (PropertyInfo i in properties)
                {
                    string helpText = getColumnName(i.Name);

                    if (i.PropertyType.IsEnum)
                    {
                        row[helpText] = (int)i.GetValue(p);
                    }
                    else if (i.PropertyType == typeof(InfluenceKind))
                    {
                        row[helpText] = ((InfluenceKind)i.GetValue(p)).ID;
                    }
                    else if (i.PropertyType == typeof(ConditionKind))
                    {
                        row[helpText] = ((ConditionKind)i.GetValue(p)).ID;
                    }
                    else if (i.PropertyType == typeof(TitleKind))
                    {
                        row[helpText] = ((TitleKind)i.GetValue(p)).ID;
                    }
                    else if (i.PropertyType == typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind))
                    {
                        row[helpText] = ((GameObjects.ArchitectureDetail.EventEffect.EventEffectKind)i.GetValue(p)).ID;
                    }
                    else if (i.PropertyType == typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind))
                    {
                        row[helpText] = ((GameObjects.TroopDetail.EventEffect.EventEffectKind)i.GetValue(p)).ID;
                    }
                    else if (i.PropertyType == typeof(List<int>))
                    {
                        row[helpText] = ((List<int>)i.GetValue(p)).Aggregate<int, string>("", (s, x) => s += x.ToString() + " ");
                    }
                    else
                    {
                        row[helpText] = i.GetValue(p) ?? DBNull.Value;
                    }

                }

                dt.Rows.Add(row);
            }

            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));

            dg.ItemsSource = dt.AsDataView();

            dt.TableNewRow += Dt_TableNewRow;
            dt.ColumnChanging += Dt_ColumnChanging;
            dt.RowChanged += Dt_RowChanged;
            dt.RowDeleting += Dt_RowDeleting;

            dg.CurrentCellChanged += Dg_CurrentCellChanged;

            dpd.AddValueChanged(dg, dg_ItemsSourceChanged);
            settingUp = false;
        }

        private void Dg_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dg.CurrentCell.Column != null)
            {
                string helpText = "";
                GetHelpText().TryGetValue(dg.CurrentCell.Column.Header.ToString(), out helpText);
                helpTextBlock.Text = dg.CurrentCell.Column.Header.ToString() + ": " + helpText;
            }
            else
            {
                helpTextBlock.Text = "";
            }
        }

        private void Dt_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            IItemList list = GetDataList(scen);
            T p = (T)list.GetGameObject((int)e.Row["id"]);
            list.Remove(p);
        }

        private int oldID = -1;
        private void Dt_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {
            oldID = (int)e.Row["id"];
        }

        private void Dt_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                T p = (T)(GetDataList(scen).GetGameObject((int)e.Row["id"]));
                if (p == null)
                {
                    p = (T)(GetDataList(scen).GetGameObject(oldID));
                    GetDataList(scen).Remove(p);
                    p.ID = (int)e.Row["id"];
                    GetDataList(scen).Add(p);
                }
                oldID = -1;
 
                FieldInfo[] fields = getFieldInfos();
                PropertyInfo[] properties = getPropertyInfos();

                foreach (FieldInfo i in fields)
                {
                    string iName = getColumnName(i.Name);

                    if (i.FieldType.IsEnum)
                    {
                        i.SetValue(p, (Int32) Enum.ToObject(i.FieldType, e.Row[iName]));
                    }
                    else if (i.FieldType == typeof(InfluenceKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllInfluenceKinds.GetInfluenceKind((int) e.Row[iName]));
                    }
                    else if (i.FieldType == typeof(ConditionKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllConditionKinds.GetConditionKind((int)e.Row[iName]));
                    }
                    else if (i.FieldType == typeof(TitleKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllTitleKinds.GetTitleKind((int)e.Row[iName]));
                    }
                    else if (i.FieldType == typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllEventEffectKinds.GetEventEffectKind((int)e.Row[iName]));
                    }
                    else if (i.FieldType == typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllTroopEventEffectKinds.GetEventEffectKind((int)e.Row[iName]));
                    }
                    else if (i.FieldType == typeof(List<int>))
                    {
                        i.SetValue(p, e.Row[iName].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select((x) => int.Parse(x)).ToList());
                    }
                    else
                    {
                        if (e.Row[iName] != DBNull.Value)
                        {
                            i.SetValue(p, e.Row[iName]);
                        }
                    }
                }
                foreach (PropertyInfo i in properties)
                {
                    string iName = getColumnName(i.Name);

                    if (i.PropertyType.IsEnum)
                    {
                        i.SetValue(p, (Int32) Enum.ToObject(i.PropertyType, e.Row[iName]));
                    }
                    else if (i.PropertyType == typeof(InfluenceKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllInfluenceKinds.GetInfluenceKind((int)e.Row[iName]));
                    }
                    else if (i.PropertyType == typeof(ConditionKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllConditionKinds.GetConditionKind((int)e.Row[iName]));
                    }
                    else if (i.PropertyType == typeof(TitleKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllTitleKinds.GetTitleKind((int)e.Row[iName]));
                    }
                    else if (i.PropertyType == typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllEventEffectKinds.GetEventEffectKind((int)e.Row[iName]));
                    }
                    else if (i.PropertyType == typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllTroopEventEffectKinds.GetEventEffectKind((int)e.Row[iName]));
                    }
                    else if (i.PropertyType == typeof(List<int>))
                    {
                        i.SetValue(p, e.Row[iName].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select((x) => int.Parse(x)).ToList());
                    }
                    else
                    {
                        if (e.Row[iName] != DBNull.Value)
                        {
                            i.SetValue(p, e.Row[iName]);
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

            IItemList list = GetDataList(scen);
            int id = list.GetFreeGameObjectID();
            e.Row["id"] = id;
            p.ID = id;
            list.Add(p);
        }

        private void dg_ItemsSourceChanged(object sender, EventArgs e)
        {
            if (settingUp) return;

            DataGrid dataGrid = (DataGrid)sender;
            IItemList list = GetDataList(scen);

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
                    string iName = getColumnName(i.Name);

                    if (i.FieldType.IsEnum)
                    {
                        i.SetValue(p, (Int32) Enum.ToObject(i.FieldType, item[iName]));
                    }
                    else if (i.FieldType == typeof(InfluenceKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllInfluenceKinds.GetInfluenceKind((int)item[iName]));
                    }
                    else if (i.FieldType == typeof(ConditionKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllConditionKinds.GetConditionKind((int)item[iName]));
                    }
                    else if (i.FieldType == typeof(TitleKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllTitleKinds.GetTitleKind((int)item[iName]));
                    }
                    else if (i.FieldType == typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllEventEffectKinds.GetEventEffectKind((int)item[iName]));
                    }
                    else if (i.FieldType == typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllTroopEventEffectKinds.GetEventEffectKind((int)item[iName]));
                    }
                    else if (i.FieldType == typeof(List<int>))
                    {
                        i.SetValue(p, item[iName].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select((x) => int.Parse(x)).ToList());
                    }
                    else
                    {
                        if (item[iName] != DBNull.Value)
                        {
                            i.SetValue(p, item[iName]);
                        }
                    }
                }
                foreach (PropertyInfo i in properties)
                {
                    string iName = getColumnName(i.Name);

                    if (i.PropertyType.IsEnum)
                    {
                        i.SetValue(p, (Int32) Enum.ToObject(i.PropertyType, item[iName]));
                    }
                    else if (i.PropertyType == typeof(InfluenceKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllInfluenceKinds.GetInfluenceKind((int)item[iName]));
                    }
                    else if (i.PropertyType == typeof(ConditionKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllConditionKinds.GetConditionKind((int)item[iName]));
                    }
                    else if (i.PropertyType == typeof(TitleKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllTitleKinds.GetTitleKind((int)item[iName]));
                    }
                    else if (i.PropertyType == typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllEventEffectKinds.GetEventEffectKind((int)item[iName]));
                    }
                    else if (i.PropertyType == typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllTroopEventEffectKinds.GetEventEffectKind((int)item[iName]));
                    }
                    else if (i.PropertyType == typeof(List<int>))
                    {
                        i.SetValue(p, item[iName].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select((x) => int.Parse(x)).ToList());
                    }
                    else
                    {
                        if (item[iName] != DBNull.Value)
                        {
                            i.SetValue(p, item[iName]);
                        }
                    }
                }
            }
        }

    }
}