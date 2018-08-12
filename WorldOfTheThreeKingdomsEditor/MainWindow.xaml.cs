using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using GameObjects;
using System.Data;
using System.IO;
using Tools;
using GameGlobal;

namespace WorldOfTheThreeKingdomsEditor
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameScenario scen;
        private bool scenLoaded = false;

        private ArchitectureTab architectureTab;
        private FactionTab factionTab;

        public MainWindow()
        {
            InitializeComponent();

            CommonData.Current = Tools.SimpleSerializer.DeserializeJsonFile<CommonData>(@"Content\Data\Common\CommonData.json", false, false);

            scen = new GameScenario();
            scen.GameCommonData = CommonData.Current;
            populateTables(false);
        }

        private void populateTables(bool hasScen)
        {
            if (hasScen)
            {
                new PersonTab(scen, dgPerson).setup();
                new DictionaryTab<int, int>(scen.FatherIds, "FatherIds", dgFatherId).setup();
                new DictionaryTab<int, int>(scen.MotherIds, "MotherIds", dgMotherId).setup();
                new DictionaryTab<int, int>(scen.SpouseIds, "SpouseIds", dgSpouseId).setup();
                // new DictionaryTab<int, int[]>(scen.BrotherIds, "BrotherIds", dgBrotherId).setup();
                architectureTab = new ArchitectureTab(scen, dgArchitecture);
                architectureTab.setup();
                factionTab = new FactionTab(scen, dgFaction);
                factionTab.setup();
                new MilitaryTab(scen, dgMilitary).setup();
                new TroopTab(scen, dgTroop).setup();
                new CaptiveTab(scen, dgCaptive).setup();
                new EventTab(scen, dgEvent).setup();
                new TroopEventTab(scen, dgTroopEvent).setup();
                new TreasureTab(scen, dgTreasure).setup();
                new FacilityTab(scen, dgFacility).setup();
            }

            // Common
            new TitleTab(scen, dgTitle).setup();
            new SkillTab(scen, dgSkill).setup();
            new StuntTab(scen, dgStunt).setup();
            new CombatMethodTab(scen, dgCombatMethod).setup();
            new InfleunceTab(scen, dgInfluence).setup();
            new InfleunceKindTab(scen, dgInflunceKind).setup();
            new ConditionTab(scen, dgCondition).setup();
            new ConditionKindTab(scen, dgConditionKind).setup();
            new EventEffectTab(scen, dgEventEffect).setup();
            new EventEffectKindTab(scen, dgEventEffectKind).setup();
            new TroopEventEffectTab(scen, dgTroopEventEffect).setup();
            new TroopEventEffectKindTab(scen, dgTroopEventEffectKind).setup();
            new FacilityKindTab(scen, dgFacilityKind).setup();
            new ArchitectureKindTab(scen, dgArchitectureKind).setup();
            new MilitaryKindTab(scen, dgMilitaryKind).setup();
        }

        public static DataTable DataViewAsDataTable(DataView dv)
        {
            DataTable dt = dv.Table.Clone();
            foreach (DataRowView drv in dv)
            {
                dt.ImportRow(drv.Row);
            }
            return dt;
        }

        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "劇本檔 (*.json)|*.json";
            openFileDialog.InitialDirectory = @"Content\Data\Scenario";
            if (openFileDialog.ShowDialog() == true)
            {
                String filename = openFileDialog.FileName;

                scen = WorldOfTheThreeKingdoms.GameScreens.MainGameScreen.LoadScenarioData(filename, true, null, true);
                scen.GameCommonData = CommonData.Current;

                populateTables(true);
                scenLoaded = true;
                Title = "中華三國志劇本編輯器 - " + filename;
            }
        }

        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (scenLoaded)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "劇本檔 (*.json)|*.json";
                saveFileDialog.InitialDirectory = @"Content\Data\Scenario";
                if (saveFileDialog.ShowDialog() == true)
                {
                    String filename = saveFileDialog.FileName;
                    String scenName = filename.Substring(filename.LastIndexOf(@"\") + 1, filename.LastIndexOf(".") - filename.LastIndexOf(@"\") - 1);
                    String scenPath = filename.Substring(0, filename.LastIndexOf(@"\"));

                    scen.SaveGameScenario(filename, true, false, false, false, true, true);

                    // GameCommonData.json
                    String commonPath = @"Content\Data\Common\CommonData.json";
                    saveGameCommonData(commonPath);

                    // Scenarios.json
                    String scenariosPath = scenPath + @"\Scenarios.json";
                    List<GameManager.Scenario> scesList = null;
                    if (File.Exists(scenariosPath))
                    {
                        scesList = SimpleSerializer.DeserializeJsonFile<List<GameManager.Scenario>>(scenariosPath, false).NullToEmptyList();
                    }
                    if (scesList == null)
                    {
                        scesList = new List<GameManager.Scenario>();
                    }

                    string time = scen.Date.Year + "-" + scen.Date.Month + "-" + scen.Date.Day;
                    GameManager.Scenario s1 = new GameManager.Scenario()
                    {
                        Create = DateTime.Now.ToSeasonDateTime(),
                        Desc = scen.ScenarioDescription,
                        First = StaticMethods.SaveToString(scen.ScenarioMap.JumpPosition),
                        IDs = scen.Factions.GameObjects.Select(x => x.ID.ToString()).Aggregate((a, b) => a + "," + b),
                        Info = "电脑",
                        Name = scenName,
                        Names = scen.Factions.GameObjects.Select(x => x.Name).Aggregate((a, b) => a + "," + b),
                        //  Path = "",
                        // PlayTime = scenario.GameTime.ToString(),
                        // Player = "",
                        //  Players = String.Join(",", scenario.PlayerList.NullToEmptyList()),
                        Time = time.ToSeasonDate(),
                        Title = scen.ScenarioTitle
                    };

                    int index = scesList.FindIndex(x => x.Name == scenName);
                    if (index >= 0)
                    {
                        scesList[index] = s1;
                    }
                    else
                    {
                        scesList.Add(s1);
                    }

                    string s2 = Newtonsoft.Json.JsonConvert.SerializeObject(scesList, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(scenariosPath, s2);

                    MessageBox.Show("劇本已儲存為" + filename);
                }
            }
            else
            {
                // GameCommonData.json
                String commonPath = @"Content\Data\Common\CommonData.json";
                saveGameCommonData(commonPath);

                MessageBox.Show("CommonData已儲存為" + commonPath);
            }
        }

        private void saveGameCommonData(String commonPath)
        {
            GameScenario.SaveGameCommonData(scen);
            string ss1 = "";
            System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(CommonData));
            using (MemoryStream stream = new MemoryStream())
            {
                //lock (Platform.SerializerLock)
                {
                    serializer.WriteObject(stream, scen.GameCommonData);
                }
                var array = stream.ToArray();
                ss1 = Encoding.UTF8.GetString(array, 0, array.Length);
            }
            ss1 = ss1.Replace("{\"", "{\r\n\"");
            ss1 = ss1.Replace("[{", "[\r\n{");
            ss1 = ss1.Replace(",\"", ",\r\n\"");
            ss1 = ss1.Replace("}", "\r\n}");
            ss1 = ss1.Replace("},{", "},\r\n{");
            ss1 = ss1.Replace("}]", "}\r\n]");
            File.WriteAllText(commonPath, ss1);
        }

        private void CopyCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Grid grid = (Grid)tabControl.SelectedContent;
            DataGrid dataGrid = (DataGrid)grid.Children[0];

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataGrid.SelectedCells.Count; i += dataGrid.Columns.Count)
            {
                object[] values = (dataGrid.SelectedCells[i].Item as DataRowView)?.Row?.ItemArray;
                if (values != null)
                {
                    foreach (object o in values)
                    {
                        sb.Append(o.ToString()).Append("\t");
                    }
                    sb.Append("\n");
                }
            }

            Clipboard.SetText(sb.ToString());
        }

        private void PasteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Grid grid = (Grid)tabControl.SelectedContent;
            DataGrid dataGrid = (DataGrid)grid.Children[0];

            try
            {
                String text = Clipboard.GetText();
                String[] textRows = text.Split(new char[] { '\n' });

                DataTable dt = ((DataView)dataGrid.ItemsSource).ToTable();
                for (int i = 0; i < textRows.Count(); i++)
                {
                    if (textRows[i].Length == 0) continue;

                    String[] data = textRows[i].Split(new char[] { '\t' });

                    DataColumnCollection columns = dt.Columns;
                    DataRow row = dt.NewRow();

                    for (int j = 0; j < Math.Min(columns.Count, data.Count()); ++j)
                    {
                        row[columns[j].ColumnName] = data[j];
                    }

                    dt.Rows.Add(row);
                }

                dataGrid.ItemsSource = dt.AsDataView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("導入資料錯誤:" + ex);
            }
        }

        private void DeleteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Grid grid = (Grid)tabControl.SelectedContent;
            DataGrid dataGrid = (DataGrid)grid.Children[0];

            StringBuilder sb = new StringBuilder();
            for (int i = dataGrid.SelectedCells.Count - 1; i > 0; i -= dataGrid.Columns.Count)
            {
                ((DataRowView)dataGrid.SelectedCells[i].Item).Row.Delete();
            }
                
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnNewFaction_Click(object sender, RoutedEventArgs e)
        {
            NewFactionWindow newFactionWindow = new NewFactionWindow(scen);
            newFactionWindow.Show();
            newFactionWindow.Closed += NewFactionWindow_Closed;
        }

        private void btnSyncScenario_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("更新");
        }

        private void NewFactionWindow_Closed(object sender, EventArgs e)
        {
            architectureTab.setup();
            factionTab.setup();
        }
    }
}
