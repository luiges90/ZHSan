using GameObjects;
using GameObjects.PersonDetail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WorldOfTheThreeKingdomsEditor
{
    public partial class MainWindow
    {
        private void setupPersons()
        {
            DataTable dtPersons = new DataTable("Person");

            DataColumn dc = new DataColumn();
            dc.DataType = typeof(int);
            dc.ColumnName = "id";
            dc.ReadOnly = true;
            dtPersons.Columns.Add(dc);

            dtPersons.Columns.Add("Available", typeof(bool));
            dtPersons.Columns.Add("Alive", typeof(bool));
            dtPersons.Columns.Add("SurName", typeof(string));
            dtPersons.Columns.Add("GivenName", typeof(string));
            dtPersons.Columns.Add("CalledName", typeof(string));
            dtPersons.Columns.Add("Sex", typeof(bool));
            dtPersons.Columns.Add("PictureIndex", typeof(int));
            dtPersons.Columns.Add("Ideal", typeof(int));
            dtPersons.Columns.Add("IdealTendency", typeof(int));
            dtPersons.Columns.Add("LeaderPossibility", typeof(bool));
            dtPersons.Columns.Add("PCharacter", typeof(int));
            dtPersons.Columns.Add("YearAvailable", typeof(int));
            dtPersons.Columns.Add("YearBorn", typeof(int));
            dtPersons.Columns.Add("YearDead", typeof(int));
            dtPersons.Columns.Add("DeadReason", typeof(PersonDeadReason));

            foreach (Person p in scen.Persons)
            {
                DataRow row = dtPersons.NewRow();
                row["id"] = p.ID;
                row["Available"] = p.Available;
                row["Alive"] = p.Alive;
                row["SurName"] = p.SurName;
                row["GivenName"] = p.GivenName;
                row["CalledName"] = p.CalledName;
                row["Sex"] = p.Sex;
                row["PictureIndex"] = p.PictureIndex;
                row["Ideal"] = p.Ideal;
                row["IdealTendency"] = p.IdealTendency.ID;
                row["LeaderPossibility"] = p.LeaderPossibility;
                row["PCharacter"] = p.PCharacter;
                row["YearAvailable"] = p.YearAvailable;
                row["YearBorn"] = p.YearBorn;
                row["YearDead"] = p.YearDead;
                // TODO value not set correctly
                row["DeadReason"] = p.DeadReason;
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
                // TODO cannot save Available / Alive
                p.Available = (bool) e.Row["Available"];
                p.Alive = (bool) e.Row["Alive"];
                p.SurName = e.Row["SurName"].ToString();
                p.GivenName = e.Row["GivenName"].ToString();
                p.CalledName = e.Row["CalledName"].ToString();
                p.Sex = (bool)e.Row["Sex"];
                p.PictureIndex = int.Parse(e.Row["PictureIndex"].ToString());
                p.Ideal = int.Parse(e.Row["Ideal"].ToString());
                p.IdealTendency = (IdealTendencyKind) scen.GameCommonData.AllIdealTendencyKinds.GetGameObject(int.Parse(e.Row["IdealTendency"].ToString()));
                p.LeaderPossibility = (bool)e.Row["LeaderPossibility"];
                p.PCharacter = int.Parse(e.Row["PCharacter"].ToString());
                p.YearAvailable = int.Parse(e.Row["YearAvailable"].ToString());
                p.YearBorn = int.Parse(e.Row["YearBorn"].ToString());
                p.YearDead = int.Parse(e.Row["YearDead"].ToString());
                p.DeadReason = (PersonDeadReason) e.Row["DeadReason"];
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
