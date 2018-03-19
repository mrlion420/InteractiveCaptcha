using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    public class DatabaseManager
    {

        public SqlConnection CreateConnection()
        {
            // SAMPLE CODE
            SqlConnection conn = new SqlConnection();
            return conn;
        }    

        public void CloseConnection(SqlConnection conn)
        {
            conn.Close();
        }
        
    }

    
}
