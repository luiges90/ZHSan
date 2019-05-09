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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorldOfTheThreeKingdomsEditor
{
    /// <summary>
    /// UserControl1_labeltext.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1_labeltext : UserControl
    {
        //public static readonly DependencyProperty TextProperty =DependencyProperty.Register("Text", typeof(string),typeof(UserControl1_labeltext),new PropertyMetadata("TextBox", new PropertyChangedCallback(OnTextChanged)));

        //public string Text
        //{
        //    get { return (string)GetValue(TextProperty); }

        //    set { SetValue(TextProperty, value); }
        //}

        //static void OnTextChanged(object sender, DependencyPropertyChangedEventArgs args)
        //{
        //    UserControl1_labeltext source = (UserControl1_labeltext)sender;
        //    source.tb.Text = (string)args.NewValue;
        //}

        //public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(UserControl1_labeltext), new PropertyMetadata("TextBlock", new PropertyChangedCallback(OnTitleChanged)));

        //static void OnTitleChanged(object sender, DependencyPropertyChangedEventArgs args)
        //{
        //    UserControl1_labeltext source = (UserControl1_labeltext)sender;
        //    source.tbtitle.Text = (string)args.NewValue;
        //}
        //public UserControl1_labeltext()
        //{
        //    InitializeComponent();
        //}

        public string Text
        {
            get {return this.tb.Text; }

            set { this.tb.Text = value; }
        }


        public string Title
        {
            get { return this.tbtitle.Text; }

            set { this.tbtitle.Text = value; }
        }

        public UserControl1_labeltext()
        {
            InitializeComponent();
        }
    }
}
