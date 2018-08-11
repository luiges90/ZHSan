using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WorldOfTheThreeKingdomsEditor
{
    /// <summary>
    /// NewFactionWindow.xaml 的互動邏輯
    /// </summary>
    public partial class NewFactionWindow : Window
    {
        private GameScenario scen;

        public NewFactionWindow(GameScenario scen)
        {
            this.scen = scen;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Person> persons;
            try
            {
                persons = tbPersons.Text.Split(' ').Select(x => scen.Persons.GetGameObject(int.Parse(x)) as Person).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("武將ID錯誤或不存在，應為空格分隔的武將ID");
                return;
            }
            Person leader;
            try
            {
                leader = scen.Persons.GetGameObject(int.Parse(tbFactionLeaderID.Text)) as Person;
                if (leader == null) throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                MessageBox.Show("君主ID錯誤或不存在");
                return;
            }
            Architecture architecture;
            try
            {
                architecture = scen.Architectures.GetGameObject(int.Parse(tbStartingArchitectureID.Text)) as Architecture;
                if (architecture == null) throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                MessageBox.Show("起始建築ID錯誤或不存在");
                return;
            }
            if (architecture.Persons.Count > 0)
            {
                MessageBox.Show("建築" + architecture.Name + "(ID: " + architecture.ID + ") 含有武將");
                return;
            }
            int color;
            try
            {
                color = int.Parse(tbColorID.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("顏色ID錯誤");
                return;
            }
            
            Faction f = new Faction();
            f.ID = scen.Factions.GetFreeGameObjectID();
            f.ColorIndex = color;
            f.LeaderID = leader.ID;
            f.Name = leader.Name;
            f.CapitalID = architecture.ID;
            f.ArchitecturesString = architecture.ID.ToString();
            f.BaseMilitaryKindsString = "0 1";
            f.UpgradingTechnique = -1;
            f.TransferingMilitaries = new MilitaryList();
            f.TransferingMilitariesString = "";
            f.TransferingMilitaryCount = 0;

            scen.Factions.Add(f);
            architecture.PersonsString = tbPersons.Text;

            this.Close();
        }
    }
}
