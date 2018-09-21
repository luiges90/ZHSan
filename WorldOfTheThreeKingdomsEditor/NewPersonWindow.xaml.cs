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
using GameObjects;

namespace WorldOfTheThreeKingdomsEditor
{
    /// <summary>
    /// NewPersonWindow.xaml 的互動邏輯
    /// </summary>
    public partial class NewPersonWindow : Window
    {
        private GameScenario scen;

        public NewPersonWindow(GameScenario scen)
        {
            this.scen = scen;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            try
            {
                count = int.Parse(tbCount.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("請輸入有效數值");
                return;
            }
            for (int i = 0; i < count; ++i)
            {
                Architecture a = (Architecture) scen.Architectures.GetRandomObject();
                scen.Persons.Add(Person.createPerson(a, null, false, false));
            }

            this.Close();
        }

    }
}
