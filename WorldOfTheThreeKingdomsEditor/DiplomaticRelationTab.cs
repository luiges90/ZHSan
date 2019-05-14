using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GameObjects.ArchitectureDetail;
using System.Data;
using System.Windows;
using System.ComponentModel;
using System.Windows.Media;
using GameObjects.FactionDetail;

namespace WorldOfTheThreeKingdomsEditor
{
    class DiplomaticRelationTab
    {
        private DataGrid dg;
        private GameScenario scen;
        private bool settingUp = false;
        private DataTable dt;
        private MainWindow mainWindow;
        // dict is write-thru
        public DiplomaticRelationTab(GameScenario scen, DataGrid dg)
        {
            this.scen = scen;
            this.dg = dg;
        }

        //private void initstates

        public void setup()
        {
            dg.ItemsSource = null;
            dt = new DataTable();
            dt.Columns.Add("势力1ID", typeof(int));
            dt.Columns.Add("势力1名称");
            dt.Columns["势力1名称"].ReadOnly = true;
            dt.Columns.Add("势力2ID", typeof(int));
            dt.Columns.Add("势力2名称");
            dt.Columns["势力2名称"].ReadOnly = true;
            dt.Columns.Add("外交关系", typeof(int));
            dt.Columns.Add("停战天数", typeof(int));
            initdt();

            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            //dg.CanUserAddRows = false;
            //dg.CanUserSortColumns = false;
            dg.ItemsSource = dt.AsDataView();
            dt.RowChanged += Dt_RowChanged;//值变更后执行
            dt.RowDeleting += Dt_RowDeleting;
            dt.TableNewRow += Dt_TableNewRow;
            dpd.AddValueChanged(dg, dg_ItemsSourceChanged);

        }

        private void Dt_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            if(!MainWindow.pasting && !settingUp)
            {
                e.Row["势力1ID"] = -1;
                e.Row["势力2ID"] = -1;
                e.Row["外交关系"] = -1;
                e.Row["停战天数"] = -1;
            }
        }

        private void initdt()
        {
            settingUp = true;
            if (dt != null)
            {

                dt.Clear();
            }
            foreach (KeyValuePair<int, GameObjects.FactionDetail.DiplomaticRelation> a in scen.DiplomaticRelations.DiplomaticRelations)
            {
                DataRow row = dt.NewRow();
                row["势力1ID"] = a.Value.RelationFaction1ID;
                Faction faction1 = scen.Factions.GetGameObject(a.Value.RelationFaction1ID) as Faction;
                Faction faction2 = scen.Factions.GetGameObject(a.Value.RelationFaction2ID) as Faction;
                row["势力2ID"] = a.Value.RelationFaction2ID;
                row["势力1名称"] = faction1 != null ? faction1.Name : "";
                row["势力2名称"] = faction2 != null ? faction2.Name : "";
                row["外交关系"] = a.Value.Relation;
                row["停战天数"] = a.Value.Truce;
                dt.Rows.Add(row);
            }
            settingUp = false;
        }

        private void Dt_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                foreach (KeyValuePair<int, GameObjects.FactionDetail.DiplomaticRelation> a in scen.DiplomaticRelations.DiplomaticRelations)
                {
                    if (a.Value.RelationFaction1ID == (int)e.Row["势力1ID"] && a.Value.RelationFaction2ID == (int)e.Row["势力2ID"])
                    {
                        scen.DiplomaticRelations.DiplomaticRelations.Remove(a.Key);
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
                    scen.DiplomaticRelations.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        DiplomaticRelation relation = new DiplomaticRelation();
                        relation.RelationFaction1ID = (int)dr["势力1ID"];
                        relation.RelationFaction2ID = (int)dr["势力2ID"];
                        relation.Relation = (int)dr["外交关系"];
                        relation.Truce = (int)dr["停战天数"];
                        scen.DiplomaticRelations.AddDiplomaticRelation(relation);
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
            if(!settingUp)
            {
                scen.DiplomaticRelations.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    DiplomaticRelation relation = new DiplomaticRelation();
                    relation.RelationFaction1ID = (int)dr["势力1ID"];
                    relation.RelationFaction2ID = (int)dr["势力2ID"];
                    relation.Relation = (int)dr["外交关系"];
                    relation.Truce = (int)dr["停战天数"];
                    scen.DiplomaticRelations.AddDiplomaticRelation(relation);
                }
                initdt();
                MainWindow.pasting = false;
            }
        }
    }
}

