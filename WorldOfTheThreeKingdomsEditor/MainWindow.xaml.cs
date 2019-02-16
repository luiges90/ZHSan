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

namespace WorldOfTheThreeKingdomsEditor
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameScenario scen;
        private bool scenLoaded = false;

        private CityTab cityTab;
        private FactionTab factionTab;
        private CharacterTab characterTab;
		private BiographyTab biographyTab;
		private TreasureTab treasureTab;
		private TroopEventTab tpeventTab;
		private TitleKindTab ttlkindTab;
		private CaptiveTab captiveTab;

        public bool CopyIncludeTitle = true;
		
		/*
		Column display for person tab
		*/
		private void CharTabHeaderDisplay(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "ID":
					e.Column.Header = characterTab.CharID_CHS;
					break;

				case "Available":
					e.Column.Header = characterTab.CharAvailable_CHS;
					break;
				
				case "Alive":
					e.Column.Header = characterTab.CharAlive_CHS;
					break;
					
				case "LastName":
					e.Column.Header = characterTab.CharLastName_CHS;
					break;
					
				case "FirstName":
					e.Column.Header = characterTab.CharFirstName_CHS;
					break;
					
				case "CalledName":
					e.Column.Header = characterTab.CharCalledName_CHS;
					break;
					
				case "Sex":
					e.Column.Header = characterTab.CharSex_CHS;
					break;
					
				case "AvatarIndex":
					e.Column.Header = characterTab.CharAvatarIndex_CHS;
					break;
					
				case "Ideal":
					e.Column.Header = characterTab.CharIdeal_CHS;
					break;
					
				case "IdealTendencyIDString":
					e.Column.Header = characterTab.CharIdealTendencyIDString_CHS;
					break;
					
				case "LeaderPossibility":
					e.Column.Header = characterTab.CharLeaderPossibility_CHS;
					break;
					
				case "PCharacter":
					e.Column.Header = characterTab.CharPCharacter_CHS;
					break;
					
				case "YearAvailable":
					e.Column.Header = characterTab.CharYearAvailable_CHS;
					break;
					
				case "YearBorn":
					e.Column.Header = characterTab.CharYearBorn_CHS;
					break;
					
				case "YearDead":
					e.Column.Header = characterTab.CharYearDead_CHS;
					break;
					
				case "YearJoin":
					e.Column.Header = characterTab.CharYearJoin_CHS;
					break;
					
				case "DeathReason":
					e.Column.Header = characterTab.CharDeathReason_CHS;
					break;
					
				case "BaseStrength":
					e.Column.Header = characterTab.CharBaseStrength_CHS;
					break;
					
				case "BaseCommand":
					e.Column.Header = characterTab.CharBaseCommand_CHS;
					break;
					
				case "BaseIntelligence":
					e.Column.Header = characterTab.CharBaseIntelligence_CHS;
					break;
					
				case "BasePolitics":
					e.Column.Header = characterTab.CharBasePolitics_CHS;
					break;
					
				case "BaseGlamour":
					e.Column.Header = characterTab.CharBaseGlamour_CHS;
					break;
					
				case "Tiredness":
					e.Column.Header = characterTab.CharTiredness_CHS;
					break;
					
				case "Reputation":
					e.Column.Header = characterTab.CharReputation_CHS;
					break;
					
				case "Karma":
					e.Column.Header = characterTab.CharKarma_CHS;
					break;
					
				case "BaseBraveness":
					e.Column.Header = characterTab.CharBaseBraveness_CHS;
					break;
					
				case "BaseCalmness":
					e.Column.Header = characterTab.CharBaseCalmness_CHS;
					break;
					
				case "SkillsString":
					e.Column.Header = characterTab.CharSkillsString_CHS;
					break;
					
				case "RealTitlesString":
					e.Column.Header = characterTab.CharRealTitlesString_CHS;
					break;
					
				case "LearningTitleString":
					e.Column.Header = characterTab.CharLearningTitleString_CHS;
					break;
					
				case "StuntsString":
					e.Column.Header = characterTab.CharStuntsString_CHS;
					break;
					
				case "LearningStuntString":
					e.Column.Header = characterTab.CharLearningStuntString_CHS;
					break;
					
				case "UniqueTitlesString":
					e.Column.Header = characterTab.CharUniqueTitlesString_CHS;
					break;
					
				case "UniqueTroopTypesString":
					e.Column.Header = characterTab.CharUniqueTroopTypesString_CHS;
					break;
					
				case "Generation":
					e.Column.Header = characterTab.CharGeneration_CHS;
					break;
					
				case "Strain":
					e.Column.Header = characterTab.CharStrain_CHS;
					break;
					
				case "IsPregnant":
					e.Column.Header = characterTab.CharIsPregnant_CHS;
					break;
					
				case "PregnancyDiscovered":
					e.Column.Header = characterTab.CharPregnancyDiscovered_CHS;
					break;
					
				case "PregnancyDayCount":
					e.Column.Header = characterTab.CharPregnancyDayCount_CHS;
					break;
				case "DefinedPartner":
					e.Column.Header = characterTab.CharDefinedPartner_CHS;
					break;
				case "DefinedPartnersList":
					e.Column.Header = characterTab.CharDefinedPartnersList_CHS;
					break;
					
				case "MarriageGranter":
					e.Column.Header = characterTab.CharMarriageGranter_CHS;
					break;
					
				case "TempLoyaltyChange":
					e.Column.Header = characterTab.CharTempLoyaltyChange_CHS;
					break;
					
				case "BirthRegion":
					e.Column.Header = characterTab.CharBirthRegion_CHS;
					break;
					
				case "AvailableLocation":
					e.Column.Header = characterTab.CharAvailableLocation_CHS;
					break;
					
				case "PersonalLoyalty":
					e.Column.Header = characterTab.CharPersonalLoyalty_CHS;
					break;
					
				case "Ambition":
					e.Column.Header = characterTab.CharAmbition_CHS;
					break;
					
				case "Qualification":
					e.Column.Header = characterTab.CharQualification_CHS;
					break;
					
				case "ValuationOnGovernment":
					e.Column.Header = characterTab.CharValuationOnGovernment_CHS;
					break;
					
				case "StrategyTendency":
					e.Column.Header = characterTab.CharStrategyTendency_CHS;
					break;
				/* Possible not in use
 				case "OldFactionID":
 					e.Column.Header = characterTab.CharOldFactionID_CHS;
					break;
				*/
				case "ProhibitedFactionID":
					e.Column.Header = characterTab.CharProhibitedFactionID_CHS;
					break;
					
				case "IsGeneratedChild":
					e.Column.Header = characterTab.CharIsGeneratedChild_CHS;
					break;
					
				case "StrengthPotential":
					e.Column.Header = characterTab.CharStrengthPotential_CHS;
					break;
					
				case "CommandPotential":
					e.Column.Header = characterTab.CharCommandPotential_CHS;
					break;
					
				case "IntelligencePotential":
					e.Column.Header = characterTab.CharIntelligencePotential_CHS;
					break;
					
				case "PoliticsPotential":
					e.Column.Header = characterTab.CharPoliticsPotential_CHS;
					break;
					
				case "GlamourPotential":
					e.Column.Header = characterTab.CharGlamourPotential_CHS;
					break;
					
				case "EducationPolicy":
					e.Column.Header = characterTab.CharEducationPolicy_CHS;
					break;

				default:
					break;
			}
		}
		
		/*
		Column display for biography tab
		*/
		private void BioTabHeaderDisplay(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "ID":
					e.Column.Header = biographyTab.CharID_CHS;
					break;

				case "FactionColor":
					e.Column.Header = biographyTab.CharFactionColor_CHS;
					break;
				
				case "AllowedTroopTypesString":
					e.Column.Header = biographyTab.CharAllowedTroopTypesString_CHS;
					break;
					
				case "BriefIntro":
					e.Column.Header = biographyTab.CharBriefIntro_CHS;
					break;
					
				case "HistoricalIntro":
					e.Column.Header = biographyTab.CharHistoricalBio_CHS;
					break;
					
				case "RomancingIntro":
					e.Column.Header = biographyTab.CharMythicalBio_CHS;
					break;
					
				case "InGame":
					e.Column.Header = biographyTab.CharInGameRecords_CHS;
					break;

				default:
					break;
			}
		}
		
		/*
		Column display for treasure tab
		*/
		private void TreasureTabHeaderDisplay(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "ID":
					e.Column.Header = treasureTab.TreasureID_CHS;
					break;

				case "Name":
					e.Column.Header = treasureTab.TreasureName_CHS;
					break;
				
				case "TreasureImage":
					e.Column.Header = treasureTab.TreasureImage_CHS;
					break;
					
				case "Worth":
					e.Column.Header = treasureTab.TreasureValue_CHS;
					break;
					
				case "Available":
					e.Column.Header = treasureTab.TreasureIsAvailable_CHS;
					break;
					
				case "HiddenPlaceIDString":
					e.Column.Header = treasureTab.TreasureHiddenPlaceIDString_CHS;
					break;
					
				case "TreasureGroup":
					e.Column.Header = treasureTab.TreasureGroup_CHS;
					break;
					
				case "AppearYear":
					e.Column.Header = treasureTab.TreasureAppearYear_CHS;
					break;
					
				case "OwnershipIDString":
					e.Column.Header = treasureTab.TreasureOwnershipIDString_CHS;
					break;
					
				case "EffectsString":
					e.Column.Header = treasureTab.TreasureEffectsIDString_CHS;
					break;
					
				case "Description":
					e.Column.Header = treasureTab.TreasureDescription_CHS;
					break;

				default:
					break;
			}
		}
		
		/*
		Column display for title kind tab
		*/
		private void TitleKindTabHeaderDisplay(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "ID":
					e.Column.Header = ttlkindTab.TitleTypeID_CHS;
					break;

				case "Name":
					e.Column.Header = ttlkindTab.TitleTypeName_CHS;
					break;
				
				case "Combat":
					e.Column.Header = ttlkindTab.TitleTypeCombat_CHS;
					break;
					
				case "RandomTeachable":
					e.Column.Header = ttlkindTab.TitleTypeRandomTeachable_CHS;
					break;
					
				case "Recallable":
					e.Column.Header = ttlkindTab.TitleTypeRecallable_CHS;
					break;
					
				case "StudyDay":
					e.Column.Header = ttlkindTab.TitleTypeStudyDay_CHS;
					break;
					
				case "SuccessRate":
					e.Column.Header = ttlkindTab.TitleTypeSuccessRate_CHS;
					break;

				default:
					break;
			}
		}
		
		/*
		Column display for troop event tab
		*/
		private void TroopEventTabHeaderDisplay(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "ID":
					e.Column.Header = tpeventTab.TroopEventID_CHS;
					break;

				case "Name":
					e.Column.Header = tpeventTab.TroopEventName_CHS;
					break;
				
				case "Happened":
					e.Column.Header = tpeventTab.TroopEventHasHappened_CHS;
					break;
					
				case "Repeatable":
					e.Column.Header = tpeventTab.TroopEventIsRepeatable_CHS;
					break;
					
				case "PredecessorEventID":
					e.Column.Header = tpeventTab.TroopEventPredecessorEventID_CHS;
					break;
					
				case "LaunchPersonString":
					e.Column.Header = tpeventTab.TroopEventReqChar_CHS;
					break;
					
				case "EventDialogString":
					e.Column.Header = tpeventTab.TroopEventDialogString_CHS;
					break;
					
				case "ConditionsString":
					e.Column.Header = tpeventTab.TroopEventConditionString_CHS;
					break;
					
				case "HappenChance":
					e.Column.Header = tpeventTab.TroopEventTriggerChance_CHS;
					break;
					
				case "CharRelationsString":
					e.Column.Header = tpeventTab.TroopEventCharRelationString_CHS;
					break;
					
				case "SelfEffectsString":
					e.Column.Header = tpeventTab.TroopEventSelfEffectString_CHS;
					break;
					
				case "CharEffectsString":
					e.Column.Header = tpeventTab.TroopEventCharEffectString_CHS;
					break;
					
				case "EffectAreasString":
					e.Column.Header = tpeventTab.TroopEventEffectAreaString_CHS;
					break;
					
				case "Image":
					e.Column.Header = tpeventTab.TroopEventImage_CHS;
					break;
					
				case "Sound":
					e.Column.Header = tpeventTab.TroopEventSound_CHS;
					break;

				default:
					break;
			}
		}
		
		/*
		Column display for captive tab
		*/
		private void CaptiveTabHeaderDisplay(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			switch (e.PropertyName)
			{
				case "ID":
					e.Column.Header = captiveTab.CaptiveID_CHS;
					break;

				case "CaptiveCharacterID":
					e.Column.Header = captiveTab.CaptiveCharacterID_CHS;
					break;
				
				case "CaptiveCharFactionID":
					e.Column.Header = captiveTab.CaptiveCharFactionID_CHS;
					break;
					
				case "RansomReceivedCityID":
					e.Column.Header = captiveTab.RansomReceivedCityID_CHS;
					break;
					
				case "RansomArrivalDays":
					e.Column.Header = captiveTab.RansomArrivalDays_CHS;
					break;
					
				case "RansomAmount":
					e.Column.Header = captiveTab.RansomAmount_CHS;
					break;

				default:
					break;
			}
		}

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
                characterTab = new CharacterTab(scen, dgPerson, lblColumnHelp);
                characterTab.setup();
                new DictionaryTab<int, int>(scen.FatherIds, "FatherIds", dgFatherId).setup();
                new DictionaryTab<int, int>(scen.MotherIds, "MotherIds", dgMotherId).setup();
                new DictionaryTab<int, int>(scen.SpouseIds, "SpouseIds", dgSpouseId).setup();
				/* Possible function in future
				new DictionaryTab<int, int[]>(scen.BrotherIds, "BrotherIds", dgBrotherId).setup();
				*/
                cityTab = new CityTab(scen, dgArchitecture, lblColumnHelp);
                cityTab.setup();
                factionTab = new FactionTab(scen, dgFaction, lblColumnHelp);
                factionTab.setup();
                new TroopArrangementTab(scen, dgMilitary, lblColumnHelp).setup();
                new TroopTab(scen, dgTroop, lblColumnHelp).setup();
				captiveTab = new CaptiveTab(scen, dgCaptive, lblColumnHelp);
				captiveTab.setup();
				/* Modified
                new CaptiveTab(scen, dgCaptive, lblColumnHelp).setup();
				*/
                new EventTab(scen, dgEvent, lblColumnHelp).setup();
				tpeventTab = new TroopEventTab(scen, dgTroopEvent, lblColumnHelp);
				tpeventTab.setup();
				/* Modified
                new TroopEventTab(scen, dgTroopEvent, lblColumnHelp).setup();
				*/
				treasureTab = new TreasureTab(scen, dgTreasure, lblColumnHelp);
				treasureTab.setup();
				/* Modified
                new TreasureTab(scen, dgTreasure, lblColumnHelp).setup();
				*/
                new FacilityTab(scen, dgFacility, lblColumnHelp).setup();
				biographyTab = new BiographyTab(scen, dgBiography, lblColumnHelp);
				biographyTab.setup();
				/* Modified
                new BiographyTab(scen, dgBiography, lblColumnHelp).setup();
				*/
            }

            // Common
            new TitleTab(scen, dgTitle, lblColumnHelp).setup();
			/*
			Added TitleKindTab
			*/
			ttlkindTab = new TitleKindTab(scen, dgTitleKind, lblColumnHelp);
			ttlkindTab.setup();
            new SkillTab(scen, dgSkill, lblColumnHelp).setup();
            new StuntTab(scen, dgStunt, lblColumnHelp).setup();
            new CombatMethodTab(scen, dgCombatMethod, lblColumnHelp).setup();
            new InfleunceTab(scen, dgInfluence, lblColumnHelp).setup();
            new InfluenceTypeTab(scen, dgInflunceKind, lblColumnHelp).setup();
            new ConditionTab(scen, dgCondition, lblColumnHelp).setup();
            new ConditionTypeTab(scen, dgConditionKind, lblColumnHelp).setup();
            new EventEffectTab(scen, dgEventEffect, lblColumnHelp).setup();
            new EventEffectTypeTab(scen, dgEventEffectKind, lblColumnHelp).setup();
            new TroopEventEffectTab(scen, dgTroopEventEffect, lblColumnHelp).setup();
            new TroopEventEffectTypeTab(scen, dgTroopEventEffectKind, lblColumnHelp).setup();
            new FacilityTypeTab(scen, dgFacilityKind, lblColumnHelp).setup();
            new CityTypeTab(scen, dgArchitectureKind, lblColumnHelp).setup();
            new TroopTypeTab(scen, dgMilitaryKind, lblColumnHelp).setup();
            new TechniqueTab(scen, dgTechniques, lblColumnHelp).setup();
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
            return new GameManager.Scenario()
            {
                Create = DateTime.Now.ToSeasonDateTime(),
                Desc = scen.ScenarioDescription,
                First = StaticMethods.SaveToString(scen.ScenarioMap.JumpPosition),
                IDs = scen.Factions.GameObjects.Select(x => x.ID.ToString()).Aggregate((a, b) => a + "," + b),
                Info = "电脑",
                Name = (scenName.IndexOf(".json") >= 0) ? scenName.Substring(0, scenName.IndexOf(".json")) : scenName,
                Names = scen.Factions.GameObjects.Select(x => x.Name).Aggregate((a, b) => a + "," + b),
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

        private void btnNewFaction_Click(object sender, RoutedEventArgs e)
        {
            NewFactionWindow newFactionWindow = new NewFactionWindow(scen);
            newFactionWindow.Show();
            newFactionWindow.Closed += NewFactionWindow_Closed;
        }

        private void NewFactionWindow_Closed(object sender, EventArgs e)
        {
            cityTab.setup();
            factionTab.setup();
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
                characterTab.setup();
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
                characterTab.setup();
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
                characterTab.setup();
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
                characterTab.setup();
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
                }
                cityTab.setup();
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

        private void btnNewPerson_Click(object sender, RoutedEventArgs e)
        {
            NewPersonWindow newPersonWindow = new NewPersonWindow(scen);
            newPersonWindow.Show();
            newPersonWindow.Closed += NewPersonWindow_Closed;
        }

        private void NewPersonWindow_Closed(object sender, EventArgs e)
        {
            characterTab.setup();
        }

        private void MenuItem_IncludeTitle_Checked(object sender, EventArgs e)
        {
            CopyIncludeTitle = true;
        }

        private void MenuItem_IncludeTitle_Unchecked(object sender, EventArgs e)
        {
            CopyIncludeTitle = false;
        }
    }

}
