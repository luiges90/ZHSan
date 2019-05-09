using GameObjects;
using GameObjects.Conditions;
using GameObjects.Influences;
using GameObjects.PersonDetail;
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
    class PersonIDRelationsTab
    {
        private DataGrid dg;
        private GameScenario scen;
        private bool settingUp = false;
        private DataTable dt;
        public PersonIDRelationsTab(DataGrid dg, GameScenario scen)
        {
            this.scen = scen;
            this.dg = dg;
        }

        public void setup()
        {
            dg.ItemsSource = null;
            dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("武将名称");
            dt.Columns["武将名称"].ReadOnly = true;
            dt.Columns.Add("对方武将ID", typeof(int));
            dt.Columns.Add("对方武将名称");
            dt.Columns["对方武将名称"].ReadOnly = true;
            dt.Columns.Add("与对方武将的友好度", typeof(int));
            initdt();

            //dg.CanUserSortColumns = false;
            dg.ItemsSource = dt.AsDataView();
            dt.RowChanged += Dt_RowChanged;//值变更后执行
            dt.RowDeleting += Dt_RowDeleting;
            dt.TableNewRow += Dt_TableNewRow;
            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            dpd.AddValueChanged(dg, dg_ItemsSourceChanged);
        }

        private void Dt_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            if (!MainWindow.pasting && !settingUp)
            {
                e.Row["ID"] = -1;
                e.Row["对方武将ID"] = -1;
                e.Row["与对方武将的友好度"] = 0;
            }
        }

        private void initdt()
        {
            settingUp = true;
            if (dt != null)
            {

                dt.Clear();
            }
            foreach (var personIDRelation in scen.PersonRelationIds)
            {
                DataRow row = dt.NewRow();
                Person person1 = scen.Persons.GetGameObject(personIDRelation.PersonID1) as Person;
                Person person2 = scen.Persons.GetGameObject(personIDRelation.PersonID2) as Person;
                row["ID"] = personIDRelation.PersonID1;
                row["武将名称"] = person1 != null ? person1.Name : "";
                row["对方武将ID"] = personIDRelation.PersonID2;
                row["对方武将名称"] = person2 != null ? person2.Name : "";
                row["与对方武将的友好度"] = personIDRelation.Relation;
                dt.Rows.Add(row);
            }
            settingUp = false;
        }

        private void Dt_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                foreach (PersonIDRelation personIDRelation in scen.PersonRelationIds)
                {
                    if (personIDRelation.PersonID1 == (int)(e.Row["ID"]) && personIDRelation.PersonID2 == (int)(e.Row["对方武将ID"]))
                    {
                        scen.PersonRelationIds.Remove(personIDRelation);
                        break;
                    }
                }
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
                    scen.PersonRelationIds.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        PersonIDRelation personIDRelation = new PersonIDRelation();
                        personIDRelation.PersonID1 = (int)dr["ID"];
                        personIDRelation.PersonID2 = (int)dr["对方武将ID"];
                        personIDRelation.Relation = (int)dr["与对方武将的友好度"];
                        scen.PersonRelationIds.Add(personIDRelation);
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
            if (!settingUp)
            {
                scen.PersonRelationIds.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    PersonIDRelation personIDRelation = new PersonIDRelation();
                    personIDRelation.PersonID1 = (int)dr["ID"];
                    personIDRelation.PersonID2 = (int)dr["对方武将ID"];
                    personIDRelation.Relation = (int)dr["与对方武将的友好度"];
                    scen.PersonRelationIds.Add(personIDRelation);
                }
                initdt();
            }
            MainWindow.pasting = false;
        }
    }
}
