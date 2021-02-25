using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace RockPaperScissors
{
    public class DBConnection
    {
        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public MySqlConnection Connection { get; set; }

        private static DBConnection _instance = null;

        public DBConnection(string server, string databaseName, string userName, string password)
        {
            this.Server = server;
            this.DatabaseName = databaseName;
            this.UserName = userName;
            this.Password = password;
        }

        public DBConnection(DBConnection inputDB)
        {
            this.Server = inputDB.Server;
            this.DatabaseName = inputDB.DatabaseName;
            this.UserName = inputDB.UserName;
            this.Password = inputDB.Password;
        }

        public DBConnection()
        {
        }

        public static DBConnection Instance()
        {
            return _instance ??= new DBConnection();
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(DatabaseName))
                    return false;
                string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", Server, DatabaseName, UserName, Password);
                Connection = new MySqlConnection(connstring);
                Connection.Open();
            }

            return true;
        }

        public void Close()
        {
            Connection.Close();
        }
    }
}

