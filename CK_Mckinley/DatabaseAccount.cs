using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CK_Mckinley
{
    class DatabaseAccount : IDisposable
    {
        public const string Server = "25.9.249.60";
        public static DatabaseAccount CurrentAccount;

        string id;
        string password;

        MySqlConnection connection;
        public string Id => id;
        public string Password => password;
        public MySqlConnection Connection => connection;

        public DatabaseAccount(string id, string password)
        {
            this.id = id;
            this.password = password;

            connection = new MySqlConnection($"Server={Server};Database=ckgame;Uid={id};Pwd={password};");
        }

        public void Login()
        {
            connection.Open();
        }
        public void Logout()
        {
            connection.Close();
        }

        ~DatabaseAccount()
        {
            Dispose();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        public void Dispose()
        {
            if (!disposedValue)
            {
                connection.Dispose();
                connection = null;
                System.Windows.MessageBox.Show("로그아웃 되었습니다.");

                disposedValue = true;
            }
        }
        #endregion
    }
}
