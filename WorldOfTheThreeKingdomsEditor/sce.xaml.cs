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
    public partial class SceWindow : Window
    {
        private GameScenario scen;

        public SceWindow(GameScenario scen)
        {
            this.scen = scen;
            InitializeComponent();

            tbYear.Text = scen.Date.Year.ToString();
            tbMonth.Text = scen.Date.Month.ToString();
            tbDay.Text = scen.Date.Day.ToString();
            tbName.Text = scen.ScenarioTitle;
            tbScenarioDescription.Text = scen.ScenarioDescription;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                scen.ScenarioTitle = tbName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("剧本名称格式有误");
                return;
            }
            try
            {
                scen.Date.Year = int.Parse(tbYear.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("剧本时间错误，年只能是整数");
                return;
            }
            try
            {
                scen.Date.Month = int.Parse(tbMonth.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("剧本时间错误，月只能是1-12的整数");
                return;
            }
            try
            {
                scen.Date.Day = int.Parse(tbDay.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("剧本时间错误，日只能是1-30的整数");
                return;
            }

            try
            {
                scen.ScenarioDescription = tbScenarioDescription.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("剧本介绍为空");
                return;
            }

            this.Close();
        }
    }
}
