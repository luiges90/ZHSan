using GameObjects;
using GameObjects.Conditions;
using GameObjects.Influences;
using GameObjects.PersonDetail;
using GameObjects.TroopDetail;
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
            typeof(Microsoft.Xna.Framework.Point),
            typeof(Microsoft.Xna.Framework.Point?),
            typeof(ConditionKind),
            typeof(InfluenceKind),
            typeof(TitleKind),
            typeof(List<int>),
            //typeof(List<KeyValuePair<int, int>>),
            typeof(int[]),
            typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind),
            typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind)
        };

        private readonly T sampleInstance = Activator.CreateInstance<T>();

        protected static bool settingUp = false;

        private FieldInfo[] getFieldInfos()
        {
            return sampleInstance.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x, typeof(DataMemberAttribute)))
                //.Where(x => supportedTypes.Contains(x.FieldType) || x.FieldType.IsEnum)
                .ToArray();
        }

        private PropertyInfo[] getPropertyInfos()
        {
            return sampleInstance.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x, typeof(DataMemberAttribute)))
                //.Where(x => supportedTypes.Contains(x.PropertyType) || x.PropertyType.IsEnum)
                .ToArray();
        }

        private GameScenario scen;
        private DataGrid dg;
        private TextBlock helpTextBlock;
        private DataTable dt;
        protected void init(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            this.scen = scen;
            this.dg = dg;
            this.helpTextBlock = helpTextBlock;
        }

        protected GameScenario getScen()
        {
            return scen;
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
            this.dg.ItemsSource = null;
            dt = new DataTable(sampleInstance.GetType().Name);

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
                else if (type == typeof(Microsoft.Xna.Framework.Point) || type == typeof(Microsoft.Xna.Framework.Point?))
                {
                    DataColumn col = new DataColumn(name, "".GetType());
                    col.DefaultValue = "";
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(InfluenceKind))
                {
                    DataColumn col = new DataColumn(name, 1.GetType());
                    col.DefaultValue = 0;
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                    //if (helpText)
                    //{

                    //}
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
                else if (type == typeof(AttackDefaultKind))
                {
                    DataColumn col = new DataColumn(name, 1.GetType());
                    col.DefaultValue = 0;
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(AttackTargetKind))
                {
                    DataColumn col = new DataColumn(name, 1.GetType());
                    col.DefaultValue = 0;
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(CastDefaultKind))
                {
                    DataColumn col = new DataColumn(name, 1.GetType());
                    col.DefaultValue = 0;
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(CastTargetKind))
                {
                    DataColumn col = new DataColumn(name, 1.GetType());
                    col.DefaultValue = 0;
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(zainanlei))
                {
                    DataColumn col = new DataColumn(name, "".GetType());
                    col.DefaultValue = "0 0 ";
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(List<KeyValuePair<int, int>>))
                {
                    DataColumn col = new DataColumn(name, "".GetType());
                    col.DefaultValue = "";
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
                else if (type == typeof(List<Microsoft.Xna.Framework.Point>))
                {
                    DataColumn col = new DataColumn(name, "".GetType());
                    col.DefaultValue = "";
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(int[]))
                {
                    DataColumn col = new DataColumn(name, "".GetType());
                    col.DefaultValue = defaultValue;
                    col.ColumnName = helpText;
                    dt.Columns.Add(col);
                }
                else if (type == typeof(Dictionary<int, int>))
                {
                    DataColumn col = new DataColumn(name, "".GetType());
                    col.DefaultValue = defaultValue;
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
            if (new T() is Influence)
            {
                dt.Columns.Add("对应种类名称");
                dt.Columns.Add("对应种类类型");
                dt.Columns["对应种类名称"].SetOrdinal(dt.Columns["对应种类"].Ordinal + 1);
                dt.Columns["对应种类类型"].SetOrdinal(dt.Columns["对应种类"].Ordinal + 2);
                dt.Columns["对应种类名称"].ReadOnly = true;
                dt.Columns["对应种类类型"].ReadOnly = true;
            }
            else if (new T() is InfluenceKind)
            {
                dt.Columns.Add("种类名称");
                dt.Columns["种类名称"].SetOrdinal(dt.Columns["种类"].Ordinal + 1);
                dt.Columns["ID"].ReadOnly = true;
                dt.Columns["种类"].ReadOnly = true;
                dt.Columns["种类名称"].ReadOnly = true;
            }
            else if (new T() is GameObjects.PersonDetail.Biography)
            {
                dt.Columns["姓名"].ReadOnly = true;
            }
            initdt();

            //if (dt.Columns.Contains("ID") && !dg.Name.Equals("dgDiplomaticRelation"))
            //{
                DataColumn[] PrimaryKeyColumns = new DataColumn[1];
                PrimaryKeyColumns[0] = dt.Columns["ID"];
                dt.PrimaryKey = PrimaryKeyColumns;
            //}
            //if (dg.Name.Equals("dgDiplomaticRelation"))
            //{
            //    dt.Columns.Remove("ID");
            //    dt.Columns.Remove("Name");
            //}
                DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));

            dg.ItemsSource = dt.AsDataView();
            dt.TableNewRow += Dt_TableNewRow;
            dt.RowChanged += Dt_RowChanged;
            dt.RowDeleting += Dt_RowDeleting;
            //dt.RowDeleted += Dt_RowDeleted;
            dg.BeginningEdit += Dg_BeginningEdit;
            dg.CurrentCellChanged += Dg_CurrentCellChanged;
            //dg.Loaded += Dg_Loaded;
            dpd.AddValueChanged(dg, dg_ItemsSourceChanged);
        }

        //private void Dg_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (dg.Name.Equals("dgDiplomaticRelation"))
        //    {
        //        dg.Columns[dt.Columns["ID"].Ordinal].Visibility = Visibility.Hidden;
        //        dg.Columns[dt.Columns["Name"].Ordinal].Visibility = Visibility.Hidden;
        //    }
        //}

        protected void initdt()
        {
            settingUp = true;
            dt.Clear();
            FieldInfo[] fields = getFieldInfos();
            PropertyInfo[] properties = getPropertyInfos();
            foreach (T p in GetDataList(scen).GetList())
            {
                DataRow row = dt.NewRow();

                foreach (FieldInfo i in fields)
                {
                    string helpText = getColumnName(i.Name);

                    if (i.FieldType.IsEnum)
                    {
                        row[helpText] = (int)i.GetValue(p);
                    }
                    else if (i.FieldType == typeof(Microsoft.Xna.Framework.Point) || i.FieldType == typeof(Microsoft.Xna.Framework.Point?))
                    {
                        row[helpText] = GameGlobal.StaticMethods.SaveToString((Microsoft.Xna.Framework.Point?)i.GetValue(p));
                    }
                    else if (i.FieldType == typeof(CastDefaultKind))
                    {
                        row[helpText] = ((CastDefaultKind)i.GetValue(p)).ID;
                    }
                    else if (i.FieldType == typeof(CastTargetKind))
                    {
                        row[helpText] = ((CastTargetKind)i.GetValue(p)).ID;
                    }
                    else if (i.FieldType == typeof(AttackDefaultKind))
                    {
                        row[helpText] = ((AttackDefaultKind)i.GetValue(p)).ID;
                    }
                    else if (i.FieldType == typeof(AttackTargetKind))
                    {
                        row[helpText] = ((AttackTargetKind)i.GetValue(p)).ID;
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
                    else if (i.FieldType == typeof(zainanlei))
                    {
                        row[helpText] = ((zainanlei)i.GetValue(p)).SavezainantoString();
                    }
                    else if (i.FieldType == typeof(Dictionary<int, int>))
                    {
                        row[helpText] = GameGlobal.StaticMethods.SaveToString((Dictionary<int, int>)i.GetValue(p));
                    }
                    else if (i.FieldType == typeof(List<KeyValuePair<int, int>>))
                    {
                        row[helpText] = GameGlobal.StaticMethods.SaveToString((List<KeyValuePair<int, int>>)i.GetValue(p));
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
                    else if (i.FieldType == typeof(List<Microsoft.Xna.Framework.Point>))
                    {
                        row[helpText] = GameGlobal.StaticMethods.SaveToString((List<Microsoft.Xna.Framework.Point>)i.GetValue(p));
                    }
                    else if (i.FieldType == typeof(int[]))
                    {
                        row[helpText] = GameGlobal.StaticMethods.SaveToString((int[])i.GetValue(p));
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
                    else if (i.PropertyType == typeof(Microsoft.Xna.Framework.Point) || i.PropertyType == typeof(Microsoft.Xna.Framework.Point?))
                    {
                        row[helpText] = GameGlobal.StaticMethods.SaveToString((Microsoft.Xna.Framework.Point?)i.GetValue(p));
                    }
                    else if (i.PropertyType == typeof(CastDefaultKind))
                    {
                        row[helpText] = ((CastDefaultKind)i.GetValue(p)).ID;
                    }
                    else if (i.PropertyType == typeof(CastTargetKind))
                    {
                        row[helpText] = ((CastTargetKind)i.GetValue(p)).ID;
                    }
                    else if (i.PropertyType == typeof(AttackDefaultKind))
                    {
                        row[helpText] = ((AttackDefaultKind)i.GetValue(p)).ID;
                    }
                    else if (i.PropertyType == typeof(AttackTargetKind))
                    {
                        row[helpText] = ((AttackTargetKind)i.GetValue(p)).ID;
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
                    else if (i.PropertyType == typeof(zainanlei))
                    {
                        row[helpText] = ((zainanlei)i.GetValue(p)).SavezainantoString();
                    }
                    else if (i.PropertyType == typeof(Dictionary<int, int>))
                    {
                        row[helpText] = GameGlobal.StaticMethods.SaveToString((Dictionary<int, int>)i.GetValue(p));
                    }
                    else if (i.PropertyType == typeof(List<KeyValuePair<int, int>>))
                    {
                        row[helpText] = GameGlobal.StaticMethods.SaveToString((List<KeyValuePair<int, int>>)i.GetValue(p));
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
                    else if (i.PropertyType == typeof(List<Microsoft.Xna.Framework.Point>))
                    {
                        row[helpText] = GameGlobal.StaticMethods.SaveToString((List<Microsoft.Xna.Framework.Point>)i.GetValue(p));
                    }
                    else if (i.PropertyType == typeof(int[]))
                    {
                        row[helpText] = GameGlobal.StaticMethods.SaveToString((int[])i.GetValue(p));
                    }
                    else
                    {
                        row[helpText] = i.GetValue(p) ?? DBNull.Value;
                    }

                }
                if (p is Influence)
                {
                    row["对应种类名称"] = (p as Influence).Kind != null ? (p as Influence).Kind.Name : "";
                    row["对应种类类型"] = (InfluenceType)(p as Influence).Kind.Type;
                }
                else if (p is InfluenceKind)
                {
                    row["种类名称"] = (InfluenceType)(p as InfluenceKind).Type;
                }
                else if (p is Biography)
                {
                    row["姓名"] = "";
                    if (scen.Persons.Count>0 && scen.Persons.GetGameObject((int)row["ID"]) as Person !=null)
                    {
                        row["姓名"] = (scen.Persons.GetGameObject((int)row["ID"]) as Person).Name;
                    }
                }
                dt.Rows.Add(row);
            }

            settingUp = false;
        }

        private void Dg_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (e.Column.Header.Equals("ID"))
            {
                MessageBox.Show("请不要轻易修改ID，如其他相关编号未修改完全很可能造成跳出");
                oldID = int.Parse((e.Column.GetCellContent(e.Row) as TextBlock).Text);
            }
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
            //if (!dg.Name.Equals("dgDiplomaticRelation"))
            //{
                IItemList list = GetDataList(scen);
                T p = (T)list.GetGameObject((int)e.Row["id"]);
                list.Remove(p);
            //}
            //else if (dg.Name.Equals("dgDiplomaticRelation"))
            //{
            //    foreach (KeyValuePair<int, GameObjects.FactionDetail.DiplomaticRelation> a in scen.DiplomaticRelations.DiplomaticRelations)
            //    {
            //        if (a.Value.RelationFaction1ID == (int)e.Row["势力1ID"] && a.Value.RelationFaction2ID == (int)e.Row["势力2ID"])
            //        {
            //            scen.DiplomaticRelations.DiplomaticRelations.Remove(a.Key);
            //            break;
            //        }
            //    }
            //}
        }

        private int oldID = -1;

        private void Dt_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                if (!settingUp && !MainWindow.pasting)
                {
                    if (oldID == -1)
                    {
                        oldID = (int)e.Row["id"];
                    }
                    T p = (T)(GetDataList(scen).GetGameObject(oldID));
                    if (p != null && GetDataList(scen) is GameObjectDictionaryItemList)
                    {
                        GetDataList(scen).Remove(p);
                    }
                    p.ID = (int)e.Row["ID"];
                    if (GetDataList(scen) is GameObjectDictionaryItemList)
                    {
                        GetDataList(scen).Add(p);
                    }
                    oldID = -1;
                    //T p = (T)(GetDataList(scen).GetGameObject((int)e.Row["id"]));
                    //if (p == null)
                    //{
                    //    p = (T)(GetDataList(scen).GetGameObject(oldID));
                    //    GetDataList(scen).Remove(p);
                    //    p.ID = (int)e.Row["id"];
                    //    GetDataList(scen).Add(p);
                    //}
                    FieldInfo[] fields = getFieldInfos();
                    PropertyInfo[] properties = getPropertyInfos();

                    foreach (FieldInfo i in fields)
                    {
                        string iName = getColumnName(i.Name);

                        if (i.FieldType.IsEnum)
                        {
                            i.SetValue(p, (Int32)Enum.ToObject(i.FieldType, e.Row[iName]));
                        }
                        else if (i.FieldType == typeof(Microsoft.Xna.Framework.Point) || i.FieldType == typeof(Microsoft.Xna.Framework.Point?))
                        {
                            Microsoft.Xna.Framework.Point? list2;
                            list2 = GameGlobal.StaticMethods.LoadFromString(e.Row[iName].ToString());
                            i.SetValue(p, list2);
                        }
                        else if (i.FieldType == typeof(InfluenceKind))
                        {
                            i.SetValue(p, scen.GameCommonData.AllInfluenceKinds.GetInfluenceKind((int)e.Row[iName]));
                        }
                        else if (i.FieldType == typeof(ConditionKind))
                        {
                            i.SetValue(p, scen.GameCommonData.AllConditionKinds.GetConditionKind((int)e.Row[iName]));
                        }
                        else if (i.FieldType == typeof(TitleKind))
                        {
                            i.SetValue(p, scen.GameCommonData.AllTitleKinds.GetTitleKind((int)e.Row[iName]));
                        }
                        else if (i.FieldType == typeof(zainanlei))
                        {
                            i.SetValue(p, GameGlobal.StaticMethods.LoadzainanfromString(e.Row[iName].ToString()));
                        }
                        else if (i.FieldType == typeof(Dictionary<int, int>))
                        {
                            Dictionary<int, int> list2 = new Dictionary<int, int>();
                            GameGlobal.StaticMethods.LoadFromString(list2, e.Row[iName].ToString());
                            i.SetValue(p, list2);
                        }
                        else if (i.FieldType == typeof(List<KeyValuePair<int, int>>))
                        {
                            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
                            GameGlobal.StaticMethods.LoadFromString(list, e.Row[iName].ToString());
                            i.SetValue(p, list);
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
                        else if (i.FieldType == typeof(List<Microsoft.Xna.Framework.Point>))
                        {
                            List<Microsoft.Xna.Framework.Point> list = new List<Microsoft.Xna.Framework.Point>();
                            GameGlobal.StaticMethods.LoadFromString(list, e.Row[iName].ToString());
                            i.SetValue(p, list);
                        }
                        else if (i.FieldType == typeof(int[]))
                        {
                            GameGlobal.StaticMethods.LoadFromString(out int[] value, e.Row[iName].ToString());
                            i.SetValue(p, value);
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
                            i.SetValue(p, (Int32)Enum.ToObject(i.PropertyType, e.Row[iName]));
                        }
                        else if (i.PropertyType == typeof(Microsoft.Xna.Framework.Point) || i.PropertyType == typeof(Microsoft.Xna.Framework.Point?))
                        {
                            Microsoft.Xna.Framework.Point? list2;
                            list2 = GameGlobal.StaticMethods.LoadFromString(e.Row[iName].ToString());
                            i.SetValue(p, list2);
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
                        else if (i.PropertyType == typeof(List<KeyValuePair<int, int>>))
                        {
                            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
                            GameGlobal.StaticMethods.LoadFromString(list, e.Row[iName].ToString());
                            i.SetValue(p, list);
                        }
                        else if (i.PropertyType == typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind))
                        {
                            i.SetValue(p, scen.GameCommonData.AllEventEffectKinds.GetEventEffectKind((int)e.Row[iName]));
                        }
                        else if (i.PropertyType == typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind))
                        {
                            i.SetValue(p, scen.GameCommonData.AllTroopEventEffectKinds.GetEventEffectKind((int)e.Row[iName]));
                        }
                        else if (i.PropertyType == typeof(zainanlei))
                        {
                            i.SetValue(p, GameGlobal.StaticMethods.LoadzainanfromString(e.Row[iName].ToString()));
                        }
                        else if (i.PropertyType == typeof(Dictionary<int, int>))
                        {
                            Dictionary<int, int> list2 = new Dictionary<int, int>();
                            GameGlobal.StaticMethods.LoadFromString(list2, e.Row[iName].ToString());
                            i.SetValue(p, list2);
                        }
                        else if (i.PropertyType == typeof(List<int>))
                        {
                            i.SetValue(p, e.Row[iName].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select((x) => int.Parse(x)).ToList());
                        }
                        else if (i.PropertyType == typeof(List<Microsoft.Xna.Framework.Point>))
                        {
                            List<Microsoft.Xna.Framework.Point> list2 = new List<Microsoft.Xna.Framework.Point>();
                            GameGlobal.StaticMethods.LoadFromString(list2, e.Row[iName].ToString());
                            i.SetValue(p, list2);
                        }
                        else if (i.PropertyType == typeof(int[]))
                        {
                            GameGlobal.StaticMethods.LoadFromString(out int[] value, e.Row[iName].ToString());
                            i.SetValue(p, value);
                        }
                        else
                        {
                            if (e.Row[iName] != DBNull.Value)
                            {
                                i.SetValue(p, e.Row[iName]);
                            }
                        }
                    }
                    //initdt();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("資料輸入錯誤。" + ex.Message);
            }
        }

        private void Dt_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            if(!settingUp && !MainWindow.pasting)
            {
                T p = Activator.CreateInstance<T>();

                IItemList list = GetDataList(scen);
                int id = list.GetFreeGameObjectID();
                e.Row["id"] = id;
                p.ID = id;
                list.Add(p);
                FieldInfo[] fields = getFieldInfos();
                PropertyInfo[] properties = getPropertyInfos();

                foreach (FieldInfo i in fields)
                {
                    string iName = getColumnName(i.Name);

                    if (i.FieldType.IsEnum)
                    {
                        i.SetValue(p, (Int32)Enum.ToObject(i.FieldType, e.Row[iName]));
                    }
                    else if (i.FieldType == typeof(Microsoft.Xna.Framework.Point) || i.FieldType == typeof(Microsoft.Xna.Framework.Point?))
                    {
                        Microsoft.Xna.Framework.Point? list2;
                        list2 = GameGlobal.StaticMethods.LoadFromString(e.Row[iName].ToString());
                        i.SetValue(p, list2);
                    }
                    else if (i.FieldType == typeof(InfluenceKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllInfluenceKinds.GetInfluenceKind((int)e.Row[iName]));
                    }
                    else if (i.FieldType == typeof(ConditionKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllConditionKinds.GetConditionKind((int)e.Row[iName]));
                    }
                    else if (i.FieldType == typeof(TitleKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllTitleKinds.GetTitleKind((int)e.Row[iName]));
                    }
                    else if (i.FieldType == typeof(zainanlei))
                    {
                        i.SetValue(p, GameGlobal.StaticMethods.LoadzainanfromString(e.Row[iName].ToString()));
                    }
                    else if (i.FieldType == typeof(Dictionary<int, int>))
                    {
                        Dictionary<int, int> list2 = new Dictionary<int, int>();
                        GameGlobal.StaticMethods.LoadFromString(list2, e.Row[iName].ToString());
                        i.SetValue(p, list2);
                    }
                    else if (i.FieldType == typeof(List<KeyValuePair<int, int>>))
                    {
                        List<KeyValuePair<int, int>> list2 = new List<KeyValuePair<int, int>>();
                        GameGlobal.StaticMethods.LoadFromString(list2, e.Row[iName].ToString());
                        i.SetValue(p, list2);
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
                    else if (i.FieldType == typeof(List<Microsoft.Xna.Framework.Point>))
                    {
                        List<Microsoft.Xna.Framework.Point> list2 = new List<Microsoft.Xna.Framework.Point>();
                        GameGlobal.StaticMethods.LoadFromString(list2, e.Row[iName].ToString());
                        i.SetValue(p, list2);
                    }
                    else if (i.FieldType == typeof(int[]))
                    {
                        GameGlobal.StaticMethods.LoadFromString(out int[] value, e.Row[iName].ToString());
                        i.SetValue(p, value);
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
                        i.SetValue(p, (Int32)Enum.ToObject(i.PropertyType, e.Row[iName]));
                    }
                    else if (i.PropertyType == typeof(Microsoft.Xna.Framework.Point) || i.PropertyType == typeof(Microsoft.Xna.Framework.Point?))
                    {
                        Microsoft.Xna.Framework.Point? list2;
                        list2 = GameGlobal.StaticMethods.LoadFromString(e.Row[iName].ToString());
                        i.SetValue(p, list2);
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
                    else if (i.PropertyType == typeof(zainanlei))
                    {
                        i.SetValue(p, GameGlobal.StaticMethods.LoadzainanfromString(e.Row[iName].ToString()));
                    }
                    else if (i.PropertyType == typeof(Dictionary<int, int>))
                    {
                        Dictionary<int, int> list2 = new Dictionary<int, int>();
                        GameGlobal.StaticMethods.LoadFromString(list2, e.Row[iName].ToString());
                        i.SetValue(p, list2);
                    }
                    else if (i.PropertyType == typeof(List<KeyValuePair<int, int>>))
                    {
                        List<KeyValuePair<int, int>> list2 = new List<KeyValuePair<int, int>>();
                        GameGlobal.StaticMethods.LoadFromString(list2, e.Row[iName].ToString());
                        i.SetValue(p, list2);
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
                    else if (i.PropertyType == typeof(List<Microsoft.Xna.Framework.Point>))
                    {
                        List<Microsoft.Xna.Framework.Point> list2 = new List<Microsoft.Xna.Framework.Point>();
                        GameGlobal.StaticMethods.LoadFromString(list2, e.Row[iName].ToString());
                        i.SetValue(p, list2);
                    }
                    else if (i.PropertyType == typeof(int[]))
                    {
                        GameGlobal.StaticMethods.LoadFromString(out int[] value, e.Row[iName].ToString());
                        i.SetValue(p, value);
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

                int id = (int)item["id"];
                T p = (T)list.GetGameObject(id);
                if (p == null)
                {
                    p = Activator.CreateInstance<T>();
                    if(list is GameObjectDictionaryItemList)
                    {
                        p.ID = id;
                    }
                    list.Add(p);
                }
                FieldInfo[] fields = getFieldInfos();
                PropertyInfo[] properties = getPropertyInfos();

                foreach (FieldInfo i in fields)
                {
                    string iName = getColumnName(i.Name);

                    if (i.FieldType.IsEnum)
                    {
                        i.SetValue(p, (Int32)Enum.ToObject(i.FieldType, item[iName]));
                    }
                    else if (i.FieldType == typeof(Microsoft.Xna.Framework.Point) || i.FieldType == typeof(Microsoft.Xna.Framework.Point?))
                    {
                        Microsoft.Xna.Framework.Point? list2;
                        list2 = GameGlobal.StaticMethods.LoadFromString(item[iName].ToString());
                        i.SetValue(p, list2);
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
                    else if (i.FieldType == typeof(zainanlei))
                    {
                        i.SetValue(p, GameGlobal.StaticMethods.LoadzainanfromString(item[iName].ToString()));
                    }
                    else if (i.FieldType == typeof(Dictionary<int, int>))
                    {
                        Dictionary<int, int> list2 = new Dictionary<int, int>();
                        GameGlobal.StaticMethods.LoadFromString(list2, item[iName].ToString());
                        i.SetValue(p, list2);
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
                    else if (i.FieldType == typeof(List<Microsoft.Xna.Framework.Point>))
                    {
                        List<Microsoft.Xna.Framework.Point> list2 = new List<Microsoft.Xna.Framework.Point>();
                        GameGlobal.StaticMethods.LoadFromString(list2, item[iName].ToString());
                        i.SetValue(p, list2);
                    }
                    else if (i.FieldType == typeof(List<KeyValuePair<int, int>>))
                    {
                        List<KeyValuePair<int, int>> list2 = new List<KeyValuePair<int, int>>();
                        GameGlobal.StaticMethods.LoadFromString(list2, item[iName].ToString());
                        i.SetValue(p, list2);
                    }
                    else if (i.FieldType == typeof(int[]))
                    {
                        GameGlobal.StaticMethods.LoadFromString(out int[] value, item[iName].ToString());
                        i.SetValue(p, value);
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
                        i.SetValue(p, (Int32)Enum.ToObject(i.PropertyType, item[iName]));
                    }
                    else if (i.PropertyType == typeof(Microsoft.Xna.Framework.Point) || i.PropertyType == typeof(Microsoft.Xna.Framework.Point?))
                    {
                        Microsoft.Xna.Framework.Point? list2;
                        list2 = GameGlobal.StaticMethods.LoadFromString(item[iName].ToString());
                        i.SetValue(p, list2);
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
                    else if (i.PropertyType == typeof(Dictionary<int, int>))
                    {
                        Dictionary<int, int> list2 = new Dictionary<int, int>();
                        GameGlobal.StaticMethods.LoadFromString(list2, item[iName].ToString());
                        i.SetValue(p, list2);
                    }
                    else if (i.PropertyType == typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllEventEffectKinds.GetEventEffectKind((int)item[iName]));
                    }
                    else if (i.PropertyType == typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind))
                    {
                        i.SetValue(p, scen.GameCommonData.AllTroopEventEffectKinds.GetEventEffectKind((int)item[iName]));
                    }
                    else if (i.PropertyType == typeof(zainanlei))
                    {
                        i.SetValue(p, GameGlobal.StaticMethods.LoadzainanfromString(item[iName].ToString()));
                    }
                    else if (i.PropertyType == typeof(List<int>))
                    {
                        i.SetValue(p, item[iName].ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select((x) => int.Parse(x)).ToList());
                    }
                    else if (i.PropertyType == typeof(List<Microsoft.Xna.Framework.Point>))
                    {
                        List<Microsoft.Xna.Framework.Point> list2 = new List<Microsoft.Xna.Framework.Point>();
                        GameGlobal.StaticMethods.LoadFromString(list2, item[iName].ToString());
                        i.SetValue(p, list2);
                    }
                    else if (i.PropertyType == typeof(List<KeyValuePair<int, int>>))
                    {
                        List<KeyValuePair<int, int>> list2 = new List<KeyValuePair<int, int>>();
                        GameGlobal.StaticMethods.LoadFromString(list2, item[iName].ToString());
                        i.SetValue(p, list2);
                    }
                    else if (i.PropertyType == typeof(int[]))
                    {
                        GameGlobal.StaticMethods.LoadFromString(out int[] value, item[iName].ToString());
                        i.SetValue(p, value);
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
            MainWindow.pasting = false;
        }

        public virtual void createWindow(bool edit, DataGrid dataGrid,MainWindow mainWindow)
        {
           
        }
    }
}