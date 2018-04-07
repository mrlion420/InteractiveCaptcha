using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    public class CaptchaAttributes
    {
        private long captchaId;
        private long attributeId;
        private string attributeName;
        private string attributeValue;
        DatabaseManager database = new DatabaseManager();
        CaptchaAttributes obj = new CaptchaAttributes();

        public CaptchaAttributes()
        {

        }

        public CaptchaAttributes(long captchaId,long attributeId,string attributeName,string attributeValue)
        {
            captchaId = this.captchaId;
            attributeId = this.attributeId;
            attributeName = this.attributeName;
            attributeValue = this.attributeValue;
        }

        public bool Insert(CaptchaAttributes obj)
        {
            string ID = obj.captchaId.ToString();
            string captchaSessionid = obj.captchaId.ToString();
            string attributeName = obj.attributeName;
            string attributeValue = obj.AttributeValue;
            string query = "INSERT INTO captcha_attributes(captchaID,attributeID,attributeName,value) VALUES('"+ID+"','"+captchaSessionid+"','"+attributeName+"','"+attributeName+"')";
            if (database.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, database.Connection);
                cmd.ExecuteNonQuery();
                database.CloseConnection();
                return true;
            }
            else
            {
                return false;
            }
        }

        public CaptchaAttributes GetCaptchaAttribute_ById(long captchaId,long attributeId)
        {

            string query = "SELECT * FROM captcha_attributes WHERE captchaID='"+captchaId+"' AND attributeID='"+attributeId+"'";
            if (database.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query,database.Connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while(dataReader.Read())
                {
                    string captchaid = dataReader["captchaID"].ToString();
                    long ID = Convert.ToInt64(captchaid);
                    obj.captchaId = ID;

                    string attributeid = dataReader["attributeID"].ToString();
                    long AID = Convert.ToInt64(attributeid);
                    obj.attributeId = ID;

                    string attributeName = dataReader["attributeName"].ToString();
                    obj.attributeName = attributeName;

                    string value = dataReader["value"].ToString();
                    obj.AttributeValue = value;
                }
            }
            return obj;
        }





        //test
        public string AttributeValue
        {
            get { return attributeValue; }
            set { attributeValue = value; }
        }

        public string AttributeName
        {
            get { return AttributeName; }
            set { AttributeName = value; }
        }


        public long attributeID
        {
            get { return attributeId; }
            set { attributeId = value; }
        }


        public long captchaID
        {
            get { return captchaID; }
            set { captchaId = value; }
        }

    }
}
