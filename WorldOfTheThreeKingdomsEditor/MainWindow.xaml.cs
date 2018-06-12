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

namespace WorldOfTheThreeKingdomsEditor
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameScenario scen;

        public MainWindow()
        {
            InitializeComponent();

            CommonData.Current = Tools.SimpleSerializer.DeserializeJsonFile<CommonData>(@"Content\Data\Common\CommonData.json", false, false);
        }

        private void populateTables()
        {
            new PersonTab(scen, dgPerson).setup();
            new DictionaryTab<int, int>(scen.FatherIds, "FatherIds", dgFatherId).setup();
            new DictionaryTab<int, int>(scen.MotherIds, "MotherIds", dgMotherId).setup();
            new DictionaryTab<int, int>(scen.SpouseIds, "SpouseIds", dgSpouseId).setup();
            // new DictionaryTab<int, int[]>(scen.BrotherIds, "BrotherIds", dgBrotherId).setup();
            new ArchitectureTab(scen, dgArchitecture).setup();
            new FactionTab(scen, dgFaction).setup();
            new MilitaryTab(scen, dgMilitary).setup();
            new TroopTab(scen, dgTroop).setup();
            new CaptiveTab(scen, dgCaptive).setup();
            new EventTab(scen, dgEvent).setup();
            new TroopEventTab(scen, dgTroopEvent).setup();
            new TreasureTab(scen, dgTreasure).setup();
            new FacilityTab(scen, dgFacility).setup();

            // Common
            /*
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
            */
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

                populateTables();
                Title = "中華三國志劇本編輯器 - " + filename;
            }
        }

        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "劇本檔 (*.json)|*.json";
            saveFileDialog.InitialDirectory = @"Content\Data\Scenario";
            if (saveFileDialog.ShowDialog() == true)
            {
                String filename = saveFileDialog.FileName;

                scen.SaveGameScenario(filename, true, false, false, false, true, true);

                MessageBox.Show("劇本已儲存為" + filename);
            }
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
                // invalid input text, ignore.
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
    }
}
