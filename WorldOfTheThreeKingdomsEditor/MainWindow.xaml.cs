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

            CommonData.Current = Tools.SimpleSerializer.DeserializeJsonFile<CommonData>(@"Content\Data\Common\CommonData.json", false, true);
        }

        private void populateTables()
        {
            DataTable dtPersons = new DataTable("Person");

            DataColumn dc = new DataColumn();
            dc.DataType = typeof(int);
            dc.ColumnName = "id";
            dc.ReadOnly = true;
            dtPersons.Columns.Add(dc);

            dtPersons.Columns.Add("surname", typeof(string));
            dtPersons.Columns.Add("givenName", typeof(string));
            dtPersons.Columns.Add("calledName", typeof(string));

            foreach (Person p in scen.Persons)
            {
                DataRow row = dtPersons.NewRow();
                row["id"] = p.ID;
                row["surname"] = p.SurName;
                row["givenName"] = p.GivenName;
                row["calledName"] = p.CalledName;
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
            catch (InvalidCastException)
            {
                MessageBox.Show("資料輸入錯誤。");
            }
        }

        private void DtPersons_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                Person p = (Person)scen.Persons.GetGameObject((int)e.Row["id"]);
                p.SurName = e.Row["surname"].ToString();
                p.GivenName = e.Row["givenName"].ToString();
                p.CalledName = e.Row["calledName"].ToString();
            } catch (InvalidCastException)
            {
                MessageBox.Show("資料輸入錯誤。");
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

        public static DataTable DataViewAsDataTable(DataView dv)
        {
            DataTable dt = dv.Table.Clone();
            foreach (DataRowView drv in dv)
            {
                dt.ImportRow(drv.Row);
            }
            return dt;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "劇本檔 (*.json)|*.json";
            openFileDialog.InitialDirectory = @"Content\Data\Scenario";
            if (openFileDialog.ShowDialog() == true)
            {
                String filename = openFileDialog.FileName;

                scen = WorldOfTheThreeKingdoms.GameScreens.MainGameScreen.LoadScenarioData(filename, true, null);

                populateTables();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "劇本檔 (*.json)|*.json";
            saveFileDialog.InitialDirectory = @"Content\Data\Scenario";
            if (saveFileDialog.ShowDialog() == true)
            {
                String filename = saveFileDialog.FileName;
                
                scen.SaveGameScenario(filename, true, true, false, true, true);

                MessageBox.Show("劇本已儲存為" + filename);
            }
        }
    }
}
