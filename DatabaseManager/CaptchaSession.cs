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
        private long Id;
        private string imageName;
        private bool isValid;

        DatabaseManager database = new DatabaseManager();

        public CaptchaSession()
        {

        }

        public CaptchaSession(long Id, string imageName, bool isValid)
        {
            Id = this.Id;
            imageName = this.imageName;
            IsValid = this.isValid;
        }

        public CaptchaSession GetCaptchaSession_ById(long captchaSessionId)
        {
            //SAMPLE CODE
            CaptchaSession obj = new CaptchaSession();
            
            string queryString = "SELECT * FROM captcha_session WHERE ID ='" + captchaSessionId +"'";
            if(database.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(queryString, database.Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while(dataReader.Read())
                {
                    string Id = dataReader["ID"].ToString();
                    long ID = Convert.ToInt64(Id);
                    obj.Id = ID;
                  
                    string imagename = dataReader["IMAGENAME"].ToString();
                    obj.imageName = imagename;


                    string isvalid = dataReader["ISVALID"].ToString();
                    if(isvalid=="0")
                    {
                        obj.isValid = false;
                    }
                    else
                    {
                        obj.isValid = true;
                    }
                    
                }
                dataReader.Close();

                database.CloseConnection();

                return obj;
            }
            else
            {
                return obj;
            }
 
        }

        public bool UpdateIsValid(long captchaSessionId, bool isvalid)
        {
            // SAMPLE CODE
            // Update all the captcha session attributes into the database
            int valid;
            if (isvalid)
            {
                valid = 1;
            }
            else
            {
                valid = 0;
            }
            
            string queryString = "UPDATE captcha_session SET ISVALID ='"+valid.ToString()+"'WHERE ID = '"+captchaSessionId+"'";
            if(database.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = queryString;
                cmd.Connection = database.Connection;
                cmd.ExecuteNonQuery();
                database.CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
         
        }

        public bool Insert(CaptchaSession obj)
        {
            // SAMPLE CODE
            // Insert all the captcha session attributes into the database
            string imagename = obj.imageName;
            string queryString = "INSERT INTO captcha_session(Imagename) VALUES('"+imageName+"')";
            if(database.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(queryString, database.Connection);
                cmd.ExecuteNonQuery();
                database.CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        public string ImageName
        {
            get { return imageName; }
            set { imageName = value; }
        }

        public long ID
        {
            get { return Id; }
            set { Id = value; }
        }

        
    }
}
