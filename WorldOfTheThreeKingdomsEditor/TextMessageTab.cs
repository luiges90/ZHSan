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
using GameObjects.PersonDetail;

namespace WorldOfTheThreeKingdomsEditor
{
    class TextMessageTab
    {
        private DataGrid dg;
        private DataGrid dgkind;
        private GameScenario scen;
        private bool settingUp = false;
        private DataTable dt;
        private MainWindow MainWindow;
        private bool hasscen;
        // dict is write-thru
        public TextMessageTab(GameScenario scen, DataGrid dg,DataGrid dgkind,bool hasscen, MainWindow mainWindow)
        {
            this.MainWindow = mainWindow;
            this.scen = scen;
            this.dg = dg;
            this.dgkind = dgkind;
            this.hasscen = hasscen;
        }

        //private void initstates

        public void setup()
        {
            dgkind.ItemsSource = null;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("语言类型ID",typeof(int));
            dataTable.Columns.Add("语言类型名称");
            for (int i = 0; i < textkinds.Length-1; i++)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["语言类型ID"] = i;
                dataRow["语言类型名称"] = textkinds[i];
                dataTable.Rows.Add(dataRow);
            }
            dgkind.ItemsSource = dataTable.AsDataView();


            dg.ItemsSource = null;
            dt = new DataTable();
            dt.Columns.Add("武将ID", typeof(int));
            dt.Columns.Add("武将姓名");
            dt.Columns["武将姓名"].ReadOnly = true;
            dt.Columns.Add("语言类型ID", typeof(int));
            dt.Columns.Add("个性语言--语言数量大于1条则随机选中--语言之间用空格格开");
            dt.Columns["武将ID"].DefaultValue=-1;
            dt.Columns["语言类型ID"].DefaultValue = -1;
            dt.Columns["个性语言--语言数量大于1条则随机选中--语言之间用空格格开"].DefaultValue = "说些什么呢";
            initdt();

            //dg.CanUserSortColumns = false;
            dg.ItemsSource = dt.AsDataView();
            dt.TableNewRow += Dt_TableNewRow;
            dt.RowChanged += Dt_RowChanged;//值变更后执行
            dt.RowDeleting += Dt_RowDeleting;

            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            dpd.AddValueChanged(dg, dg_ItemsSourceChanged);
        }

        private void Dt_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            if (!MainWindow.pasting && !settingUp)
            {
                TextMessageKind kind = (TextMessageKind)(int)e.Row["语言类型ID"];
                List<string> list = new List<string>();
                GameGlobal.StaticMethods.LoadFromString(list, e.Row["个性语言--语言数量大于1条则随机选中--语言之间用空格格开"].ToString());
                scen.GameCommonData.AllTextMessages.AddTextMessages((int)e.Row["武将ID"], kind, list);
            }
        }

        private void initdt()
        {
            settingUp = true;
            if (dt != null)
            {
                dt.Clear();
            }
            foreach (KeyValuePair<KeyValuePair<int, GameObjects.PersonDetail.TextMessageKind>, List<string>> a in scen.GameCommonData.AllTextMessages.GetAllMessages())
            {
                DataRow row = dt.NewRow();
                row["武将ID"] = a.Key.Key;
                if(hasscen)
                {
                    row["武将姓名"] = (scen.Persons.GetGameObject(a.Key.Key) as Person) != null ? (scen.Persons.GetGameObject(a.Key.Key) as Person).Name : "";
                }
                else
                {
                    row["武将姓名"] ="未载入剧本";
                }
                row["语言类型ID"] = (int)a.Key.Value;
                row["个性语言--语言数量大于1条则随机选中--语言之间用空格格开"] = GameGlobal.StaticMethods.SaveToString(a.Value);
                dt.Rows.Add(row);
            }
            settingUp = false;
        }

        private void Dt_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                foreach (KeyValuePair<KeyValuePair<int, TextMessageKind>, List<string>> a in scen.GameCommonData.AllTextMessages.textMessages)
                {
                    if (a.Key.Key == (int)e.Row["武将ID"] && a.Key.Value==(TextMessageKind) (int)e.Row["语言类型ID"])
                    {
                        scen.GameCommonData.AllTextMessages.textMessages.Remove(a.Key);
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
                    scen.GameCommonData.AllTextMessages.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        TextMessageKind kind = (TextMessageKind)(int)dr["语言类型ID"];
                        List<string> list = new List<string>();
                        GameGlobal.StaticMethods.LoadFromString(list, dr["个性语言--语言数量大于1条则随机选中--语言之间用空格格开"].ToString());
                        scen.GameCommonData.AllTextMessages.AddTextMessages((int)dr["武将ID"], kind, list);
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
                scen.GameCommonData.AllTextMessages.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    TextMessageKind kind = (TextMessageKind)(int)dr["语言类型ID"];
                    List<string> list = new List<string>();
                    GameGlobal.StaticMethods.LoadFromString(list, dr["个性语言--语言数量大于1条则随机选中--语言之间用空格格开"].ToString());
                    scen.GameCommonData.AllTextMessages.AddTextMessages((int)dr["武将ID"], kind, list);
                }
                initdt();
                MainWindow.pasting = false;
            }
        }

        private string[] textkinds = new string[]
        {
            "暴击",
            "暴击建筑",
            "被暴击",
            "包围",
            "击破",
            "单挑主动胜利",
            "单挑被动胜利",
            "论战主动胜利",
            "论战被动胜利",
            "进入混乱",
            "深度混乱",
            "施展计略致乱",
            "从混乱中恢复",
            "中计",
            "被计略帮助",
            "抵抗敌意计略",
            "抵抗友好计略",
            "抵抗攻击",
            "攻破城墙",
            "愤怒",
            "沉静",
            "开始工作",
            "修习技能成功",
            "修习技能失败",
            "修习特技成功",
            "修习特技失败",
            "修习称号成功",
            "修习称号失败",
            "被录用",
            "被褒奖",
            "被获得宝物",
            "被没收宝物",
            "发现宝物",
            "情报成功",
            "情报失败",
            "搜索资金",
            "搜索军粮",
            "搜索技巧点",
            "搜索间谍",
            "搜索未发现武将",
            "下野",
            "逃狱",
            "出征",
            "部队移动",
            "运输队返回",
            "被扩散火伤",
            "使用战法",
            "准备使用战法",
            "使用特技",
            "(无势力)使用友好计略",
            "(无势力)使用敌对计略",
            "攻心",
            "扰乱",
            "侦查",
            "埋伏",
            "火攻",
            "镇静",
            "灭火",
            "鼓舞",
            "点火",
            "医治",
            "伪报",
            "挑衅",
            "准备使用计略",
            "开始埋伏",
            "中止埋伏",
            "发动埋伏",
            "被发动埋伏",
            "发现埋伏",
            "被发现埋伏",
            "部队捕获武将",
            "开始截断粮道",
            "中止截断粮道",
            "成功截断粮道",
            "失败截断粮道",
            "死亡",
            "在单挑中死亡",
            "因上任死亡成为君主",
            "建立义兄弟",
            "建立义姊妹",
            "建立配偶",
            "被纳妃",
            "被宠幸",
            "君主自己发现怀孕",
            "武将发现怀孕",
            "妃子发现怀孕",
            "父亲子女出生",
            "子女出生",
            "被夺妻",
            "子女加入",
            "子女自行加入",
            "女性配偶加入",
            "男性配偶加入",
            "亲善",
            "外交征讨",
            "断交指令",
            "断交",
            "同盟",
            "同盟失败",
            "停战",
            "停战失败",
            "势力灭亡被俘",
            "释放俘虏",
            "处斩俘虏",
            "流放武将",
            "得到控制权",
            "设施完成",
            "(君主)占领建筑",
            "发生天灾",
            "势力技巧完成",
            "建筑遭受攻击",
            "君主受封升官",
            "禅位",
            "称帝",
            "称帝后果",
            "自立",
            "转君主保留名称",
            "转君主更改名称",
            "统一天下",
            "君主自立升官",
            "被成功伪报",
            "被成功挑衅",
            "武将加入",
            "取得基本兵种",
            "赐婚",
            "立储",
            "招贤",
            "任命县令",
            "劝降",
            "劝降失败",
        };

    }
}
