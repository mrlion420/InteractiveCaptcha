using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DatabaseManager
{
   class DatabaseManager
    {

        public MySqlConnection Connection { get; set; }

        private string server;
        private string database;
        private string uid;
        private string password;

        public DatabaseManager()
        {
            Initialize();
        }
        private void Initialize()
        {
            server = "http://122.11.177.14:";
            database = "interactive_captcha";
            uid = "root";
            password = "toor";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            Connection = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                Connection.Open();
                return true;
            }
            catch(MySqlException ex)
            {
                return false;
            }
        }
        public bool CloseConnection()
        {
            try
            {
                Connection.Close();
                Connection.Dispose();
                return true;
            }
            catch(MySqlException)
            {
                return false;
            }
        }


    }

    
}
