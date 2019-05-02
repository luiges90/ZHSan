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
using GameObjects.FactionDetail;
using System.Drawing;
using OfficeOpenXml;

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
        private PersonTab personTab;
        private DictionaryTab<int, int> fatherTab;
        private DictionaryTab<int, int> motherTab;
        private DictionaryTab<int, int> spouseTab;
        private string scename;

        public bool CopyIncludeTitle = true;

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
                personTab = new PersonTab(scen, dgPerson, lblColumnHelp);
                personTab.setup();
                fatherTab = new DictionaryTab<int, int>(scen.FatherIds, "FatherIds", dgFatherId);
                fatherTab.setup();
                motherTab = new DictionaryTab<int, int>(scen.MotherIds, "MotherIds", dgMotherId);
                motherTab.setup();
                spouseTab = new DictionaryTab<int, int>(scen.SpouseIds, "SpouseIds", dgSpouseId);
                spouseTab.setup();
                // new DictionaryTab<int, int[]>(scen.BrotherIds, "BrotherIds", dgBrotherId).setup();
                architectureTab = new ArchitectureTab(scen, dgArchitecture, lblColumnHelp);
                architectureTab.setup();
                factionTab = new FactionTab(scen, dgFaction, lblColumnHelp);
                factionTab.setup();
                new MilitaryTab(scen, dgMilitary, lblColumnHelp).setup();
                new TroopTab(scen, dgTroop, lblColumnHelp).setup();
                new CaptiveTab(scen, dgCaptive, lblColumnHelp).setup();
                new EventTab(scen, dgEvent, lblColumnHelp).setup();
                new TroopEventTab(scen, dgTroopEvent, lblColumnHelp).setup();
                new TreasureTab(scen, dgTreasure, lblColumnHelp).setup();
                new FacilityTab(scen, dgFacility, lblColumnHelp).setup();
                new BiographyTab(scen, dgBiography, lblColumnHelp).setup();
            }

            // Common
            new TitleTab(scen, dgTitle, lblColumnHelp).setup();
            new SkillTab(scen, dgSkill, lblColumnHelp).setup();
            new StuntTab(scen, dgStunt, lblColumnHelp).setup();
            new CombatMethodTab(scen, dgCombatMethod, lblColumnHelp).setup();
            new InfleunceTab(scen, dgInfluence, lblColumnHelp).setup();
            new InfleunceKindTab(scen, dgInflunceKind, lblColumnHelp).setup();
            new ConditionTab(scen, dgCondition, lblColumnHelp).setup();
            new ConditionKindTab(scen, dgConditionKind, lblColumnHelp).setup();
            new EventEffectTab(scen, dgEventEffect, lblColumnHelp).setup();
            new EventEffectKindTab(scen, dgEventEffectKind, lblColumnHelp).setup();
            new TroopEventEffectTab(scen, dgTroopEventEffect, lblColumnHelp).setup();
            new TroopEventEffectKindTab(scen, dgTroopEventEffectKind, lblColumnHelp).setup();
            new FacilityKindTab(scen, dgFacilityKind, lblColumnHelp).setup();
            new ArchitectureKindTab(scen, dgArchitectureKind, lblColumnHelp).setup();
            new MilitaryKindTab(scen, dgMilitaryKind, lblColumnHelp).setup();
            new TechniqueTab(scen, dgTechniques, lblColumnHelp).setup();
            new GuanjueTab(scen, dgGuanjue, lblColumnHelp).setup();
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
                scename = filename;
                scen = WorldOfTheThreeKingdoms.GameScreens.MainGameScreen.LoadScenarioData(filename, true, null, true);
                scen.GameCommonData = CommonData.Current;

                populateTables(true);
                scenLoaded = true;
                Title = "中華三國志劇本編輯器 - " + openFileDialog.SafeFileName;
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

                    GameManager.Scenario s1 = createScenarioObject(scen, scenName);
                    if (s1 != null)
                    {
                        int index = scesList.FindIndex(x => x.Name == scenName);
                        if (index >= 0)
                        {
                            scesList[index] = s1;
                        }
                        else
                        {
                            scesList.Add(s1);
                        }
                    }

                    string s2 = Newtonsoft.Json.JsonConvert.SerializeObject(scesList, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(scenariosPath, s2);

                    MessageBox.Show("劇本已儲存為" + filename + "。CommonData已儲存為" + commonPath);
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

        private GameManager.Scenario createScenarioObject(GameScenario scen, String scenName)
        {
            if (scen.Factions.Count == 0) return null;

            string time = scen.Date.Year.ToString().PadLeft(4, '0') + "-" + scen.Date.Month.ToString().PadLeft(2, '0') + "-" + scen.Date.Day.ToString().PadLeft(2, '0');
            GameObjectList gameObjectList = new GameObjectList();//恢复原先剧本设定，某些势力玩家不可选
            foreach (Faction faction in scen.Factions)
            {
                if (!faction.NotPlayerSelectable)
                {
                    gameObjectList.Add(faction);
                }
            }
            return new GameManager.Scenario()
            {
                Create = DateTime.Now.ToSeasonDateTime(),
                Desc = scen.ScenarioDescription,
                First = StaticMethods.SaveToString(scen.ScenarioMap.JumpPosition),
                IDs = gameObjectList.GameObjects.Select(x => x.ID.ToString()).Aggregate((a, b) => a + "," + b),
                Info = "电脑",
                Name = (scenName.IndexOf(".json") >= 0) ? scenName.Substring(0, scenName.IndexOf(".json")) : scenName,
                Names = gameObjectList.GameObjects.Select(x => x.Name).Aggregate((a, b) => a + "," + b),
                //  Path = "",
                // PlayTime = scenario.GameTime.ToString(),
                // Player = "",
                //  Players = String.Join(",", scenario.PlayerList.NullToEmptyList()),
                Time = time,
                Title = scen.ScenarioTitle
            };
        }

        private void CopyCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Grid grid = (Grid)tabControl.SelectedContent;
            DataGrid dataGrid = (DataGrid)grid.Children[0];

            StringBuilder sb = new StringBuilder();

            if (CopyIncludeTitle)
            {
                for (int i = 0; i < dataGrid.Columns.Count; ++i)
                {
                    sb.Append(dataGrid.Columns[i].Header).Append("\t");
                }
                sb.Append("\n");
            }

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

        private void btnSyncScenario_Click(object sender, RoutedEventArgs e)
        {
            String scenariosPath = @"Content\Data\Scenario\Scenarios.json";
            MessageBoxResult result = MessageBox.Show("更新" + scenariosPath + "檔案，使遊戲能辨認劇本資料夾裡的劇本。是否繼續？", "更新Scenarios.json", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                List<GameManager.Scenario> scesList = new List<GameManager.Scenario>();

                FileInfo[] files = new DirectoryInfo(@"Content\Data\Scenario").GetFiles("*.json", SearchOption.TopDirectoryOnly);
                foreach (FileInfo file in files)
                {
                    if (file.Name.Equals("Scenarios.json")) continue;
                    GameScenario s = WorldOfTheThreeKingdoms.GameScreens.MainGameScreen.LoadScenarioData(file.FullName, true, null, true);
                    GameManager.Scenario s1 = createScenarioObject(s, file.Name);
                    if (s1 != null)
                    {
                        scesList.Add(s1);
                    }
                }

                string s2 = Newtonsoft.Json.JsonConvert.SerializeObject(scesList, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(scenariosPath, s2);

                MessageBox.Show("已更新" + scenariosPath);
            }
        }

        private void btnPNGAlpha_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "待處理圖片 (*.png)|*.png";
            openFileDialog.InitialDirectory = @"Content\Textures";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                var filenames = openFileDialog.FileNames;

                foreach (var filename in filenames)
                {
                    using (var img = System.Drawing.Image.FromFile(filename))
                    {
                        var bitmap = new Bitmap(img, img.Width, img.Height);

                        var target = new Bitmap(img.Width, img.Height);

                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            g.DrawImage(target, 0, 0);
                            for (int h = 0; h < bitmap.Height; h++)
                            {
                                for (int w = 0; w < bitmap.Width; w++)
                                {
                                    var color = bitmap.GetPixel(w, h);
                                    color = PremultiplyAlpha(color);
                                    target.SetPixel(w, h, color);
                                }
                            }
                        }

                        var dir = filename.Substring(0, filename.LastIndexOf('\\') + 1);

                        var file = filename.Substring(filename.LastIndexOf('\\') + 1);

                        var newDir = dir + "Alpha\\";

                        var newFile = newDir + file;

                        if (!Directory.Exists(newDir))
                        {
                            Directory.CreateDirectory(newDir);
                        }

                        target.Save(newFile, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }

                MessageBox.Show("PNG圖片PreMultiplied!");
            }
        }

        public static System.Drawing.Color PremultiplyAlpha(System.Drawing.Color pixel)
        {
            return System.Drawing.Color.FromArgb(
                pixel.A,
                PremultiplyAlpha_Component(pixel.R, pixel.A),
                PremultiplyAlpha_Component(pixel.G, pixel.A),
                PremultiplyAlpha_Component(pixel.B, pixel.A));
        }

        private static byte PremultiplyAlpha_Component(byte source, byte alpha)
        {
            return (byte)(Convert.ToSingle(source) * Convert.ToSingle(alpha) / Convert.ToSingle(byte.MaxValue) + 0.5f);
        }

        private void btnRandomizeIdeal_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("把所有武將的相性及相性考慮隨機化，是否確認？", "隨機化武將相性", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                foreach (Person p in scen.Persons)
                {
                    p.Ideal = GameObject.Random(0, 149);
                    p.IdealTendencyIDString = scen.GameCommonData.AllIdealTendencyKinds.GetRandomObject().ID;
                }
                personTab.setup();
            }
        }

        private void btnRandomizePersonality_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("把所有武將的各項性格隨機化，是否確認？", "隨機化武將性格", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                foreach (Person p in scen.Persons)
                {
                    List<GameObjects.PersonDetail.CharacterKind> ck = scen.GameCommonData.AllCharacterKinds;
                    p.Character = ck[GameObject.Random(ck.Count)];

                    if (p.BelongedFaction != null && p.BelongedFaction.IsAlien)
                    {
                        p.PersonalLoyalty = GameObject.Random(2);
                    }
                    else
                    {
                        p.PersonalLoyalty = GameObject.Random(5);
                    }
                    p.Ambition = GameObject.Random(5);
                    p.Qualification = (PersonQualification)GameObject.Random(Enum.GetNames(typeof(PersonQualification)).Length);
                    p.Braveness = GameObject.Random(11);
                    p.Calmness = GameObject.Random(11);
                    p.ValuationOnGovernment = (PersonValuationOnGovernment)GameObject.Random(Enum.GetNames(typeof(PersonValuationOnGovernment)).Length);
                    p.StrategyTendency = (PersonStrategyTendency)GameObject.Random(Enum.GetNames(typeof(PersonStrategyTendency)).Length);
                }
                personTab.setup();
            }
        }

        private void btnRandomizeDeadYear_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("把所有武將的壽命隨機化，是否確認？", "隨機化武將壽命", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                foreach (Person p in scen.Persons)
                {
                    if (p.Alive)
                    {
                        if (p.Available)
                        {
                            p.YearDead = Math.Max(p.YearBorn + GameObject.RandomGaussianRange(30, 90), p.YearAvailable + GameObject.RandomGaussianRange(1, 10));
                        }
                        else
                        {
                            p.YearDead = p.YearBorn + GameObject.RandomGaussianRange(30, 90);
                        }
                    }
                }
                personTab.setup();
            }
        }

        private void btnRandomizeAvailableLocation_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("把所有武將的登場地點隨機化，是否確認？", "隨機化武將登場地點", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                foreach (Person p in scen.Persons)
                {
                    if (!p.Available)
                    {
                        p.AvailableLocation = scen.Architectures.GetRandomObject().ID;
                    }
                }
                personTab.setup();
            }
        }

        private void btnRedoArchitectureLinks_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("重新計算並更新所有建築的連接。可能要花上數分鐘的時間，是否確認？", "重新設置城池連接", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                scen.InitializeMapData();
                scen.InitializeArchitectureMapTile();
                foreach (Architecture architecture2 in scen.Architectures)
                {
                    architecture2.AILandLinks.Clear();
                    architecture2.AIWaterLinks.Clear();
                }
                foreach (Architecture architecture2 in scen.Architectures)
                {
                    architecture2.FindLinks(scen.Architectures);
                    architecture2.AILandLinksString = architecture2.AILandLinks.SaveToString();
                    architecture2.AIWaterLinksString = architecture2.AIWaterLinks.SaveToString();
                }
                architectureTab.setup();
            }
        }

        private void btnDeleteAllDiplomacy_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("刪除所有勢力外交關係，是否確認？", "刪除所有勢力外交關係", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                foreach (DiplomaticRelation relation in scen.DiplomaticRelations.DiplomaticRelations.Values)
                {
                    relation.Relation = 0;
                }
            }
        }

        private void btnCleanupRelation_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("刪除多餘關係，是否確認？", "刪除所有未登場武將的特殊關係", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                Dictionary<int, int> copy = new Dictionary<int, int>(scen.FatherIds);
                foreach (KeyValuePair<int, int> kv in copy)
                {
                    Person k = scen.Persons.GetGameObject(kv.Key) as Person;
                    Person v = scen.Persons.GetGameObject(kv.Value) as Person;
                    if (k == null || v == null)
                    {
                        scen.FatherIds.Remove(kv.Key);
                    }
                }
                fatherTab.setup();

                copy = new Dictionary<int, int>(scen.MotherIds);
                foreach (KeyValuePair<int, int> kv in copy)
                {
                    Person k = scen.Persons.GetGameObject(kv.Key) as Person;
                    Person v = scen.Persons.GetGameObject(kv.Value) as Person;
                    if (k == null || v == null)
                    {
                        scen.MotherIds.Remove(kv.Key);
                    }
                }
                motherTab.setup();

                copy = new Dictionary<int, int>(scen.SpouseIds);
                foreach (KeyValuePair<int, int> kv in copy)
                {
                    Person k = scen.Persons.GetGameObject(kv.Key) as Person;
                    Person v = scen.Persons.GetGameObject(kv.Value) as Person;
                    if (k == null || v == null)
                    {
                        scen.SpouseIds.Remove(kv.Key);
                    }
                }
                spouseTab.setup();
            }
        }

        private void btnDeleteUnavailableRelation_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("刪除所有未登場武將的配偶關係，是否確認？", "刪除所有未登場武將的特殊關係", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                Dictionary<int, int> copy = new Dictionary<int, int>(scen.SpouseIds);
                foreach (KeyValuePair<int, int> kv in copy)
                {
                    Person k = scen.Persons.GetGameObject(kv.Key) as Person;
                    Person v = scen.Persons.GetGameObject(kv.Value) as Person;
                    if (k != null && v != null && !k.Available && !v.Available)
                    {
                        scen.SpouseIds.Remove(kv.Key);
                    }
                }
                spouseTab.setup();
            }
        }

        private void btnNewPerson_Click(object sender, RoutedEventArgs e)
        {
            NewPersonWindow newPersonWindow = new NewPersonWindow(scen);
            newPersonWindow.Show();
            newPersonWindow.Closed += NewPersonWindow_Closed;
        }

        private void NewPersonWindow_Closed(object sender, EventArgs e)
        {
            personTab.setup();
        }

        private void MenuItem_IncludeTitle_Checked(object sender, EventArgs e)
        {
            CopyIncludeTitle = true;
        }

        private void MenuItem_IncludeTitle_Unchecked(object sender, EventArgs e)
        {
            CopyIncludeTitle = false;
        }

        private void btnScenariotoexcel_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(@Environment.CurrentDirectory + @"\转换生成文件"))//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(@Environment.CurrentDirectory + @"\转换生成文件");
            }
            if (scename != null && scename != "")
            {
                String scenName = scename.Substring(scename.LastIndexOf(@"\") + 1, scename.LastIndexOf(".") - scename.LastIndexOf(@"\") - 1);
                if (File.Exists(Environment.CurrentDirectory + "\\转换生成文件\\" + scenName + "." + "xlsx"))
                {
                    File.Delete(Environment.CurrentDirectory + "\\转换生成文件\\" + scenName + "." + "xlsx");
                }
                if (File.Exists(Environment.CurrentDirectory + "\\转换生成文件\\" + scenName + "地形信息.txt"))
                {
                    File.Delete(Environment.CurrentDirectory + "\\转换生成文件\\" + scenName + "地形信息.txt");
                }
                using (ExcelPackage package = new ExcelPackage(new FileInfo(Environment.CurrentDirectory + "\\转换生成文件\\" + scenName + "." + "xlsx")))
                {
                    for(int i=0;i< tabControl.Items.Count;i++)
                    {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(((TabItem)tabControl.Items[i]).Header.ToString());
                            tabControl.SelectedIndex = i;
                             Grid grid = (Grid)tabControl.SelectedContent;
                             DataGrid dataGrid = (DataGrid)grid.Children[0];
                             DataTable dt = ((DataView)dataGrid.ItemsSource).Table;
                             worksheet.Cells["a1"].LoadFromDataTable(dt, true); 
                        ///临时用来检查剧本问题的
                       /* if(((TabItem)tabControl.Items[i]).Header.ToString() == "武將")
                        {
                            int n = dt.Columns.Count;
                            worksheet.Cells[1, n+1].Value = "武将所属";
                            worksheet.Cells[1, n + 2].Value = "武将势力";
                            for (int ii=0;ii<scen.Persons.Count;ii++)
                            {
                                worksheet.Cells[ii + 2, n + 1].Value = ((Person)scen.Persons[ii]).Location ;
                                worksheet.Cells[ii + 2, n + 2].Value = ((Person)scen.Persons[ii]).BelongedFaction ;
                            }
                        }*/
                    }
                    package.Save();

                }
                StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "\\转换生成文件\\" + scenName + "地形信息.txt", false, Encoding.GetEncoding("gb2312"));
                sw.Write(scen.ScenarioMap.MapDataString);
                sw.Flush();
                sw.Close();
                sw.Dispose();
                MessageBox.Show("转换完毕,请查阅根目录下转换生成文件夹");
            }
            else
            {
                MessageBox.Show("请先用编辑器打开想要修改的剧本");
            }
        }

        private void btnsce_Click(object sender, RoutedEventArgs e)
        {
            SceWindow sceWindow = new SceWindow(scen);
            sceWindow.ShowDialog();
        }

        private void btnSets_Click(object sender, RoutedEventArgs e)
        {
            Sets sets = new Sets(scen);
            sets.Title = "Glo参数设置";
            sets.path1 = @Environment.CurrentDirectory + "\\Content\\Data\\GlobalVariables.xml";
            sets.typ = "Glo";
            if (this.scename != "" && this.scename != null)
            {
                sets.scename = this.scename.Substring(scename.LastIndexOf(@"\") + 1, scename.LastIndexOf(".") - scename.LastIndexOf(@"\") - 1);

            }
            sets.InitialSets();
            sets.ShowDialog();
            sets.Owner = this;
        }
        private void btnSetsPara_Click(object sender, RoutedEventArgs e)
        {
            Sets sets = new Sets(scen);
            sets.Title = "Para参数设置";
            sets.path1 = @Environment.CurrentDirectory + "\\Content\\Data\\GameParameters.xml";
            sets.typ = "Para";
            if (this.scename != "" && this.scename != null)
            {
                sets.scename = this.scename.Substring(scename.LastIndexOf(@"\") + 1, scename.LastIndexOf(".") - scename.LastIndexOf(@"\") - 1);

            }
            sets.InitialSets();
            sets.ShowDialog();
            sets.Owner = this;
        }

        private void DgFaction_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
           /* if (e.Column.Header.Equals("颜色编号"))
            {
                  int n =0;
                if ((e.EditingElement as TextBox).Text != null && (e.EditingElement as TextBox).Text != null)
                {
                    // n = int.Parse((e.EditingElement as TextBox).Text);
                    bool b = int.TryParse((e.EditingElement as TextBox).Text,out n);
                    if(n !=0)
                    {
                         Microsoft.Xna.Framework.Color color = scen.GameCommonData.AllColors[n];
                         e.Row.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
                    }
                }
            }*/  // 这两个屏蔽是显示势力颜色，但因只能做到整行背景色改变，太花
        }

        private void DgFaction_LoadingRow(object sender, DataGridRowEventArgs e)
        {
          /* if(e.Row.GetIndex()<dgFaction.Items.Count-1)
            {
                Microsoft.Xna.Framework.Color color = scen.GameCommonData.AllColors[((Faction)scen.Factions[e.Row.GetIndex()]).ColorIndex];
                e.Row.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
            }*/
        }

        private void DgFaction_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgFaction.SelectedItem != null)
            {
                Faction faction = scen.Factions.GetGameObject(int.Parse(((DataRowView)dgFaction.SelectedItem).Row["ID"].ToString())) as Faction;
                dgFaction.IsReadOnly = true;
                Editfaction(faction);
            }
        }
        private void Editfaction(Faction faction)
        {
            NewFactionWindow newFactionWindow = new NewFactionWindow(scen, this, faction);
            newFactionWindow.Closed += NewFactionWindow_Closed;
            newFactionWindow.ShowDialog();
        }

        private void NewFactionWindow_Closed(object sender, EventArgs e)
        {
            dgFaction.IsReadOnly = false;
            architectureTab.setup();
            factionTab.setup();
        }

        private void Menueditfac_Click(object sender, RoutedEventArgs e)
        {
            if (dgFaction.SelectedItem != null)
            {
                Faction faction = scen.Factions.GetGameObject(int.Parse(((DataRowView)dgFaction.SelectedItem).Row["ID"].ToString())) as Faction;
                Editfaction(faction);
            }
        }

        private void MenuDeltfac_Click(object sender, RoutedEventArgs e)
        {
            for (int i = dgFaction.SelectedCells.Count - 1; i > 0; i -= dgFaction.Columns.Count)
            {
                Faction faction = scen.Factions.GetGameObject(int.Parse(((DataRowView)dgFaction.SelectedCells[i].Item).Row["ID"].ToString())) as Faction;
                scen.Factions.RemoveFaction(faction);
            }
            factionTab.setup();
        }

        private void MenuAddfac_Click(object sender, RoutedEventArgs e)
        {
            Faction f = new Faction();
            f.ID = scen.Factions.GetFreeGameObjectID();
            f.ColorIndex = 52;
            f.BaseMilitaryKindsString = "0 1 3";
            f.UpgradingTechnique = -1;
            f.TransferingMilitaries = new MilitaryList();
            f.TransferingMilitariesString = "";
            f.TransferingMilitaryCount = 0;
            f.AvailableTechniquesString = "";
            f.PreferredTechniqueKinds = new List<int>() { 0 ,1, 2,3, 4, 5, 6, 7, 8, 9, 10 };
            f.PlanTechniqueString = -1;
            f.GetGeneratorPersonCountString = "0:0,1:0,2:0,3:0,4:0,5:0,6:0,7:0,8:0";
            f.InformationsString = "";
            f.LegionsString = "";
            f.MilitariesString = "";
            f.RoutewaysString = "";
            f.SectionsString = "";
            f.TroopListString = "";
            scen.Factions.Add(f);
            Editfaction(f);
        }
    }

}
