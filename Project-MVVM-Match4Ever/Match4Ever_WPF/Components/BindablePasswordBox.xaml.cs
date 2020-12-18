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

namespace Match4Ever_WPF.Components
{
    /// <summary>
    /// Interaction logic for BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        public static readonly DependencyProperty PasswdProperty = DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Passwd
        {
            get { return (string)GetValue(PasswdProperty); }
            set { SetValue(PasswdProperty, value); }
        }

        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        private void mvvmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Passwd = mvvmPasswordBox.Password;
        }
    }
}
