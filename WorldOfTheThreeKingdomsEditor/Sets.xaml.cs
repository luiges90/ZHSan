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
using GameObjects.Conditions;
using GameObjects.Influences;
using GameObjects.PersonDetail;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;

namespace WorldOfTheThreeKingdomsEditor
{
    /// <summary>
    /// Sets.xaml 的交互逻辑
    /// </summary>
    public partial class Sets : Window
    {

        private DataTable dt;
        private DataTable dtsce;
        public string scename;
        public string typ;
        public string path1;
        public Sets(GameScenario scen)
        {
            InitializeComponent();
          //  FieldInfo[] fields = getFieldInfos();
          //  PropertyInfo[] properties = getPropertyInfos();
        }

        public void InitialSets()
        {
            XmlDocument document = new XmlDocument();
            string allxml = System.IO.File.ReadAllText(path1);
            document.LoadXml(allxml);
            XmlNode nextSibling = document.FirstChild.NextSibling;
            dt = new DataTable();
            dt.Columns.Add("参数");
            dt.Columns.Add("中文");
            dt.Columns.Add("数值");
            for (int i = 0; i < nextSibling.Attributes.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = nextSibling.Attributes[i].Name;
                dr[1]= nextSibling.Attributes[i].Name;
                dr[2] = nextSibling.Attributes[i].Value;
                dt.Rows.Add(dr);
            }
            dt.Columns[0].ReadOnly = true;
            dt.Columns[1].ReadOnly = true;
            dgSetGlo2.ItemsSource = dt.AsDataView();
            if(this.scename !=null && this.scename !="")
            {
                string str = "";
                if (this.typ == "Glo")
                {
                    str = Environment.CurrentDirectory + @"\Content\Data\Scenario\" + scename + "GlobalVariables.xml";
                }
                else if (this.typ == "Para")
                {
                    str = Environment.CurrentDirectory + @"\Content\Data\Scenario\" + scename + "GameParameters.xml";
                }
                if (File.Exists(str))
                {
                    InitialSceSets(str);
                }
            }
        }

        private void InitialSceSets(string str)
        {
            XmlDocument document = new XmlDocument();
            string allxml = System.IO.File.ReadAllText(str);
            // string xml = Platform.Current.LoadText("Content/Data/GlobalVariables.xml");
            document.LoadXml(allxml);
            XmlNode nextSibling = document.FirstChild.NextSibling;
            dtsce = new DataTable();
            dtsce.Columns.Add("参数");
            dtsce.Columns.Add("中文");
            dtsce.Columns.Add("数值");
            for (int i = 0; i < nextSibling.Attributes.Count; i++)
            {
                DataRow dr = dtsce.NewRow();
                dr[0] = nextSibling.Attributes[i].Name;
                dr[1] = nextSibling.Attributes[i].Name;
                dr[2] = nextSibling.Attributes[i].Value;
                dtsce.Rows.Add(dr);
            }
            dtsce.Columns[0].ReadOnly = true;
            dtsce.Columns[1].ReadOnly = true;
            dgSetGlo.ItemsSource = dtsce.AsDataView();

        }

        private Type[] supportedTypes = new Type[]
{
            typeof(bool),
            typeof(byte),
            typeof(short),
            typeof(int),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(char),
            typeof(string),
            typeof(ConditionKind),
            typeof(InfluenceKind),
            typeof(TitleKind),
            typeof(List<int>),
            typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind),
            typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind)
};

        private FieldInfo[] getFieldInfos()
        {
            return typeof(GameGlobal.GlobalVariables).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x, typeof(DataMemberAttribute)))
                .Where(x => supportedTypes.Contains(x.FieldType) || x.FieldType.IsEnum)
                .ToArray();
        }

        private PropertyInfo[] getPropertyInfos()
        {
            return typeof(GameGlobal.GlobalVariables).GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x, typeof(DataMemberAttribute)))
                .Where(x => supportedTypes.Contains(x.PropertyType) || x.PropertyType.IsEnum)
                .ToArray();
        }


        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dgSetGlo.Width = 0.42 * this.Width;
            dgSetGlo2.Width = 0.42 * this.Width;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InitialSets();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string str = "";
            if(this.typ=="Glo")
            {
                str = Environment.CurrentDirectory + @"\Content\Data\Scenario\" + scename + "GlobalVariables.xml";
            }
            else if (this.typ == "Para")
            {
                str = Environment.CurrentDirectory + @"\Content\Data\Scenario\" + scename + "GameParameters.xml";
            }
            if (File.Exists(str))
            {
                InitialSceSets(str);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            XmlDocument document = new XmlDocument();
            XmlNode docNode = document.CreateXmlDeclaration("1.0", "utf-8", null);
            document.AppendChild(docNode);
            XmlElement element=document.CreateElement("GlobalVariables");
            string str = Environment.CurrentDirectory + @"\Content\Data\Scenario\" + scename + "GlobalVariables.xml";
            if (this.typ == "Para")
            {
                element = document.CreateElement("GameParameters");
                str = Environment.CurrentDirectory + @"\Content\Data\Scenario\" + scename + "GameParameters.xml";
            }
            for (int i = 0; i < dtsce.Rows.Count; i++)
            {
                element.SetAttribute(dtsce.Rows[i][0].ToString(), dtsce.Rows[i][2].ToString());
            }
            document.AppendChild(element);
            try
            {
                if (File.Exists(str))
                {
                    File.Delete(str);
                }
                File.WriteAllText(str, document.OuterXml);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            XmlDocument document = new XmlDocument();
            XmlNode docNode = document.CreateXmlDeclaration("1.0", "utf-8", null);
            document.AppendChild(docNode);
            XmlElement element = document.CreateElement("GlobalVariables");
            string str = Environment.CurrentDirectory + @"\Content\Data\GlobalVariables.xml";
            if (this.typ == "Para")
            {
                element = document.CreateElement("GameParameters");
                str = Environment.CurrentDirectory + @"\Content\Data\GameParameters.xml";
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                element.SetAttribute(dt.Rows[i][0].ToString(), dt.Rows[i][2].ToString());
            }
            document.AppendChild(element);
            try
            {
                if (File.Exists(str))
                {
                    File.Delete(str);
                }
                File.WriteAllText(str, document.OuterXml);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Sceaddset_Click(object sender, RoutedEventArgs e)
        {
            if (scename != null && scename != "")
            {
                if (dtsce == null)
                {
                    dtsce = new DataTable();
                    dtsce.Columns.Add("参数");
                    dtsce.Columns.Add("中文");
                    dtsce.Columns.Add("数值");
                }
                DataTable dt2 = dtsce;
                if (dgSetGlo2.SelectedCells.Count >= 1)
                {
                    for (int i = 0; i < dgSetGlo2.SelectedItems.Count; i++)
                    {
                        DataRow row = dt2.NewRow();
                        DataRowView dro = (DataRowView)dgSetGlo2.SelectedItems[i];
                        row[0] = dro.Row[0];
                        row[1] = dro.Row[1];
                        row[2] = dro.Row[2];
                        dt2.Rows.Add(row);
                    }
                }
                dgSetGlo.ItemsSource = dt2.AsDataView();
            }
        }

        private void Scedelset_Click(object sender, RoutedEventArgs e)
        {
            for (int i = dgSetGlo.SelectedCells.Count - 1; i > 0; i -= dgSetGlo.Columns.Count)
            {
                ((DataRowView)dgSetGlo.SelectedCells[i].Item).Row.Delete();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(Environment.CurrentDirectory + "\\转换生成文件\\" + 111 + "." + "xlsx")))
            {
              //  for (int i = 0; i < tabControl.Items.Count; i++)
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("11");
                   // tabControl.SelectedIndex = i;
                  //  Grid grid = (Grid)tabControl.SelectedContent;
                 //   DataGrid dataGrid = (DataGrid)grid.Children[0];
                    DataTable dt = ((DataView)dgSetGlo2.ItemsSource).Table;
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
        }
    }
}
