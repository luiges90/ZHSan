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

namespace WorldOfTheThreeKingdomsEditor
{
    public partial class MainWindow
    {

        private FieldInfo[] getFieldInfos()
        {
            Person person = new Person();
            return person.GetType().GetFields().Where(x => Attribute.IsDefined(x, typeof(DataMemberAttribute))).ToArray();
        }

        private PropertyInfo[] getPropertyInfos()
        {
            Person person = new Person();
            return person.GetType().GetProperties().Where(x => Attribute.IsDefined(x, typeof(DataMemberAttribute))).ToArray();
        }

        private void setupPersons()
        {
            DataTable dtPersons = new DataTable("Person");

            DataColumn dc = new DataColumn();
            dc.DataType = typeof(int);
            dc.ColumnName = "id";
            dc.ReadOnly = true;
            dtPersons.Columns.Add(dc);

            FieldInfo[] fields = getFieldInfos();
            PropertyInfo[] properties = getPropertyInfos();

            foreach (FieldInfo i in fields)
            {
                dtPersons.Columns.Add(i.Name, i.FieldType);
            }
            foreach (PropertyInfo i in properties)
            {
                if (i.PropertyType.Name == "Nullable`1")
                {
                    dtPersons.Columns.Add(i.Name, i.PropertyType.GenericTypeArguments[0]);
                }
                else
                {
                    dtPersons.Columns.Add(i.Name, i.PropertyType);
                }
            }
            
            foreach (Person p in scen.Persons)
            {
                DataRow row = dtPersons.NewRow();
                row["id"] = p.ID;

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

            dgPerson.ItemsSource = dtPersons.AsDataView();

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
