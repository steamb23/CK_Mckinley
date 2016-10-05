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

namespace CK_Mckinley
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void textBox_id_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox_id.Text = string.Empty;
        }

        private void passwordBox_password_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordBox_password.Password = string.Empty;
        }

        private void button_login_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow();
        }

        void ChangeWindow()
        {
            Application.Current.MainWindow = new DataWindow();
            this.Close();
            Application.Current.MainWindow.Show();
        }
    }
}
