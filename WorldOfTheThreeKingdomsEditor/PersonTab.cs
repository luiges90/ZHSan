using GameObjects;
using GameObjects.PersonDetail;
using System;
using System.Collections.Generic;
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
    public class PersonTab
    {
        class ItemOrderComparer : IComparer<String>
        {
            String[] rawItemOrder =
            {
                "ID",
                "Available",
                "Alive",
                "SurName",
                "GivenName",
                "CalledName",
                "Sex",
                "PictureIndex",
                "Ideal",
                "IdealTendencyIDString",
                "LeaderPossibility",
                "PCharacter",
                "YearAvailable",
                "YearBorn",
                "YearDead",
                "YearJoin",
                "DeadReason",
                "BaseStrength",
                "BaseCommand",
                "BaseIntelligence",
                "BasePolitics",
                "BaseGlamour",
                "Reputation",
                "Braveness",
                "Calmness",
                "SkillsString",
                "RealTitlesString",
                "StudyingTitleString",
                "StuntsString",
                "StudyingStuntString",
                "UniqueTitlesString",
                "UniqueMilitaryKindsString",
                "Generation",
                "Strain",
                "huaiyun",
                "faxianhuaiyun",
                "huaiyuntianshu",
                "shoshurenwu",
                "suoshurenwuList",
                "MarriageGranter",
                "TempLoyaltyChange",
                "BornRegion",
                "AvailableLocation",
                "PersonalLoyalty",
                "Ambition",
                "Qualification",
                "ValuationOnGovernment",
                "StrategyTendency",
                "OldFactionID",
                "ProhibitedFactionID",
                "IsGeneratedChildren",
                "CommandPotential",
                "StrengthPotential",
                "IntelligencePotential",
                "GlamourPotential",
                "TrainPolicy"
            };

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
            typeof(string)
        };

        private Dictionary<String, String> defaultValues = new Dictionary<string, string>
        {
            { "StudyingTitleString", "-1" },
            { "StudyingStuntString", "-1" },
            { "huaiyuntianshu", "-1" },
            { "suoshurenwu", "-1" },
            { "ConvincingPersonID", "-1" },
            { "waitForFeiZiPeriod", "30" },
            { "waitForFeiziId", "-1" },
            { "InjureRate", "1" },
            { "Generation", "1" },
            { "InformationKindID", "-1" }
        };

        private FieldInfo[] getFieldInfos()
        {
            Person person = new Person();
            return person.GetType().GetFields()
                .Where(x => Attribute.IsDefined(x, typeof(DataMemberAttribute)))
                .Where(x => supportedTypes.Contains(x.FieldType))
                .ToArray();
        }

        private PropertyInfo[] getPropertyInfos()
        {
            Person person = new Person();
            return person.GetType().GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(DataMemberAttribute)))
                .Where(x => supportedTypes.Contains(x.PropertyType))
                .ToArray();
        }

        private GameScenario scen;
        private DataGrid dg;

        public PersonTab(GameScenario scen, DataGrid dg)
        {
            this.scen = scen;
            this.dg = dg;
        }

        public void setup()
        {
            DataTable dtPersons = new DataTable("Person");

            FieldInfo[] fields = getFieldInfos();
            PropertyInfo[] properties = getPropertyInfos();

            MemberInfo[] items = new MemberInfo[fields.Length + properties.Length];
            items = items.Union(fields).Union(properties).OrderBy(x => x == null ? "" : x.Name, new ItemOrderComparer()).ToArray();
 
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
                if (!defaultValues.TryGetValue(name, out defaultValue))
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

                DataColumn col = new DataColumn(name, type);
                col.DefaultValue = defaultValue;
                dtPersons.Columns.Add(col);
            }
            
            foreach (Person p in scen.Persons)
            {
                DataRow row = dtPersons.NewRow();

                foreach (FieldInfo i in fields)
                {
                    row[i.Name] = i.GetValue(p);
                }
                foreach (PropertyInfo i in properties)
                {
                    row[i.Name] = i.GetValue(p) ?? DBNull.Value;
                }
        
                dtPersons.Rows.Add(row);
            }

            dg.ItemsSource = dtPersons.AsDataView();

            dtPersons.TableNewRow += DtPersons_TableNewRow;
            dtPersons.RowChanged += DtPersons_RowChanged;
            dtPersons.RowDeleted += DtPersons_RowDeleted;
        }

        private void DtPersons_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                Person p = (Person)scen.Persons.GetGameObject((int)e.Row["id"]);
                scen.Persons.Remove(p);
            }
            catch (Exception ex)
            {
                MessageBox.Show("資料輸入錯誤。" + ex.Message);
            }
        }

        private void DtPersons_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                Person p = (Person)scen.Persons.GetGameObject((int)e.Row["id"]);

                FieldInfo[] fields = getFieldInfos();
                PropertyInfo[] properties = getPropertyInfos();

                foreach (FieldInfo i in fields)
                {
                    i.SetValue(p, e.Row[i.Name]);
                }
                foreach (PropertyInfo i in properties)
                {
                    i.SetValue(p, e.Row[i.Name]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("資料輸入錯誤。" + ex.Message);
            }
        }

        private void DtPersons_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            Person p = new Person();

            int id = scen.Persons.GetFreeGameObjectID();
            e.Row["id"] = id;
            p.ID = id;
            scen.Persons.Add(p);
        }

    }
}
