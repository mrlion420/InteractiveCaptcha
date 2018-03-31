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
        private MySqlConnection connection;

        public MySqlConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

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
            server = "localhost";
            database = "csci321";
            uid = "root";
            password = "Kennieisgreat1!";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch(MySqlException)
            {
                return false;
            }
        }
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch(MySqlException)
            {
                return false;
            }
        }


    }

    
}
