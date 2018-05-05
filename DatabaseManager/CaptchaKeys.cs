using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    class CaptchaKeys
    {
        public string CaptchaKey { get; set; }
        DatabaseManager database = new DatabaseManager();
        public CaptchaKeys()
        {
            //CREATE TABLE CAPTCHA_KEYS(
            //CaptchaKey varchar(255) not null PRIMARY KEY,
            //IsActive BIT not null default 1
            //)
        }

        public bool Insert(CaptchaKeys obj)
        {
            bool result = false;
            string queryString = "INSERT INTO CaptchaKeys(CaptchaKey) values (@captchaKey)";
            if (database.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(queryString, database.Connection);                
                cmd.Parameters.AddWithValue("@captchaKey", obj.CaptchaKey);
                int numOfRows = cmd.ExecuteNonQuery();
                if (numOfRows >= 1)
                {
                    result = true;
                }
                database.CloseConnection();

            }
            return result;
        }

        public bool CheckIfKeyExists(string captchaKey)
        {
            bool result = false;
            string queryString = "SELECT * FROM captcha_keys WHERE CaptchaKey = @captchaKey";
            if (database.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(queryString, database.Connection);
                cmd.Parameters.AddWithValue("@captchaKey", captchaKey);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    result = true;
                }
                dataReader.Close();

                database.CloseConnection();
            }
            return result;
        }
    }
}
