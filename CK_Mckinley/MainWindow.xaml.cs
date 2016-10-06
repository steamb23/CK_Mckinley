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
using MySql.Data.MySqlClient;

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
            textBox_id.GotFocus -= textBox_id_GotFocus;
        }

        private void passwordBox_password_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordBox_password.Password = string.Empty;
            passwordBox_password.GotFocus -= passwordBox_password_GotFocus;
        }

        private void button_login_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }
        void Login()
        {
            DatabaseAccount.CurrentAccount = new DatabaseAccount(textBox_id.Text, passwordBox_password.Password);
            try
            {
                DatabaseAccount.CurrentAccount.Login();
                ChangeWindow();
            }
            catch (MySqlException ex)
            {
                switch ((MySqlErrorCode)ex.Number)
                {
                    case MySqlErrorCode.AccessDenied:
                        MessageBox.Show("로그인에 실패하였습니다. 아이디와 비밀번호를 확인해주세요.", "로그인 오류");
                        break;
                    default:
                        MessageBox.Show(ex.Message, "알수없는 오류");
                        break;
                }

                DatabaseAccount.CurrentAccount.Dispose();
                DatabaseAccount.CurrentAccount = null;
            }
        }

        void ChangeWindow()
        {
            Application.Current.MainWindow = new DataWindow();
            this.Close();
            Application.Current.MainWindow.Show();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login();
        }
    }
}
