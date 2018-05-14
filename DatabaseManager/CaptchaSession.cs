using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DatabaseManager;


namespace DatabaseManager
{
    public class CaptchaSession
    {
        public int CaptchaId { get; set; }
        public string ImageName { get; set; }
        public bool IsValid { get; set; }
        public string CaptchaKey { get; set; }

        DatabaseManager database = new DatabaseManager();

        public CaptchaSession()
        {
            //CREATE TABLE CAPTCHA_SESSION(
            //CaptchaId int AUTO_INCREMENT PRIMARY KEY,
            //ImageName VARCHAR(255) NOT NULL,
            //IsValid BIT DEFAULT 1,
            //CaptchaKey VARCHAR(255) NOT NULL,
            //FOREIGN KEY(CaptchaKey) REFERENCES captcha_keys(captchakey) on delete cascade
            //)
        }

        public CaptchaSession(long Id, string imageName, bool isValid)
        {
            
        }

        public CaptchaSession GetCaptchaSession(int captchaId, string captchaKey)
        {
            CaptchaSession obj = new CaptchaSession();
            
            string queryString = "SELECT * FROM captcha_session WHERE CaptchaId = @captchaId AND CaptchaKey = @captchaKey";
            if(database.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(queryString, database.Connection);
                cmd.Parameters.AddWithValue("@captchaId", captchaId);
                cmd.Parameters.AddWithValue("@captchaKey", captchaKey);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while(dataReader.Read())
                {
                    string tempHolder = dataReader["CaptchaId"].ToString();
                    obj.CaptchaId = Convert.ToInt32(tempHolder);
                    
                    obj.ImageName = dataReader["ImageName"].ToString();
                    
                    string isvalid = dataReader["IsValid"].ToString();

                    if(isvalid.Equals("0"))
                    {
                        obj.IsValid = false;
                    }
                    else
                    {
                        obj.IsValid = true;
                    }

                    obj.CaptchaKey = captchaKey;
                    
                }
                dataReader.Close();

                database.CloseConnection();

                
            }

            return obj;
        }

        public CaptchaSession GetCaptchaSession(int captchaId)
        {
            CaptchaSession obj = new CaptchaSession();

            string queryString = "SELECT * FROM captcha_session WHERE CaptchaId = @captchaId";
            if (database.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(queryString, database.Connection);
                cmd.Parameters.AddWithValue("@captchaId", captchaId);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    string tempHolder = dataReader["CaptchaId"].ToString();
                    obj.CaptchaId = Convert.ToInt32(tempHolder);

                    obj.ImageName = dataReader["ImageName"].ToString();

                    string isvalid = dataReader["IsValid"].ToString();

                    if (isvalid.Equals("0"))
                    {
                        obj.IsValid = false;
                    }
                    else
                    {
                        obj.IsValid = true;
                    }

                }
                dataReader.Close();

                database.CloseConnection();


            }

            return obj;
        }

        public bool UpdateIsValid(int captchaId, string captchaKey, bool isValid)
        {
            int valid;
            bool result = false;
            if (isValid)
            {
                valid = 1;
            }
            else
            {
                valid = 0;
            }
            
            string queryString = "UPDATE captcha_session SET IsValid = @isValid where CaptchaId = @captchaId and CaptchaKey = @captchaKey";
            if(database.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(queryString, database.Connection);
                cmd.Parameters.AddWithValue("@isValid", valid);
                cmd.Parameters.AddWithValue("@captchaId", captchaId);
                cmd.Parameters.AddWithValue("@captchaKey", captchaKey);
                int numOfRows = cmd.ExecuteNonQuery();
                if(numOfRows >= 1)
                {
                    result = true;
                }
                database.CloseConnection();
                
            }
            return result;
         
        }

        public bool UpdateIsValid(int captchaId, bool isValid)
        {
            int valid;
            bool result = false;
            if (isValid)
            {
                valid = 1;
            }
            else
            {
                valid = 0;
            }

            string queryString = "UPDATE captcha_session SET IsValid = @isValid where CaptchaId = @captchaId";
            if (database.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(queryString, database.Connection);
                cmd.Parameters.AddWithValue("@isValid", valid);
                cmd.Parameters.AddWithValue("@captchaId", captchaId);
                
                int numOfRows = cmd.ExecuteNonQuery();
                if (numOfRows >= 1)
                {
                    result = true;
                }
                database.CloseConnection();

            }
            return result;

        }


        public bool InsertWithKey(CaptchaSession obj)
        {
            bool result = false;
            string queryString = "INSERT INTO captcha_session(ImageName,CaptchaKey) values (@imageName, @captchaKey)";
            if(database.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(queryString, database.Connection);
                cmd.Parameters.AddWithValue("@imageName", obj.ImageName);
                cmd.Parameters.AddWithValue("@captchaKey", obj.CaptchaKey);
                int numOfRows = cmd.ExecuteNonQuery();
                if(numOfRows >= 1)
                {
                    result = true;
                }
                database.CloseConnection();
                
            }
            return result;
        }

        public bool Insert(ref int captchaId)
        {
            bool result = false;
            string queryString = "INSERT INTO captcha_session(ImageName) values (@imageName); SELECT LAST_INSERT_ID();";
            if (database.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(queryString, database.Connection);
                cmd.Parameters.AddWithValue("@imageName", ImageName);
                cmd.CommandType = System.Data.CommandType.Text;
                var obj = cmd.ExecuteScalar();
                if(obj != null)
                {
                    captchaId = Convert.ToInt32(obj);
                }
                result = true;
                
                database.CloseConnection();

            }
            return result;
        }

    }
}
