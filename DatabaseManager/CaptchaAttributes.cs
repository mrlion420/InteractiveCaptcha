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
        public int CaptchaAttributeID { get; set; }

        public int CaptchaID { get; set; }

        public short Tile1Angle { get; set; }

        public short Tile2Angle { get; set; }

        public short Tile9Angle { get; set; }

        public short Tile8Angle { get; set; }

        public short Tile7Angle { get; set; }

        public short Tile6Angle { get; set; }

        public short Tile5Angle { get; set; }

        public short Tile4Angle { get; set; }

        public short Tile3Angle { get; set; }

        DatabaseManager database = new DatabaseManager();
        CaptchaAttributes obj = new CaptchaAttributes();

        public CaptchaAttributes()
        {
            //CREATE TABLE CAPTCHA_ATTRIBUTES(
            //CaptchaAttributeId int auto_increment primary key,
            //CaptchaId int not null,
            //Tile1Angle smallint not null,
            //Tile2Angle smallint not null,
            //Tile3Angle smallint not null,
            //Tile4Angle smallint not null,
            //Tile5Angle smallint not null,
            //Tile6Angle smallint not null,
            //Tile7Angle smallint not null,
            //Tile8Angle smallint not null,
            //Tile9Angle smallint not null,
            //foreign key(CaptchaId) REFERENCES captcha_session(captchaid) on delete cascade
            //)
        }

        public bool Insert(CaptchaAttributes obj)
        {
            bool result = false;
            string query = @"INSERT INTO captcha_attributes(CaptchaAttributeId,Tile1Angle,Tile2Angle,Tile3Angle,Tile4Angle, " +
                "Tile5Angle, Tile6Angle, Tile7Angle, Tile8Angle, Tile9Angle) VALUES(@captchaAttributeId, @tile1, @tile2, @tile3, @tile4, @tile5, @tile6, @tile7, @tile8)";

            if (database.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, database.Connection);
                cmd.Parameters.AddWithValue("@captchaAttributeId", obj.CaptchaAttributeID);
                cmd.Parameters.AddWithValue("@tile1", obj.Tile1Angle);
                cmd.Parameters.AddWithValue("@tile2", obj.Tile2Angle);
                cmd.Parameters.AddWithValue("@tile3", obj.Tile3Angle);
                cmd.Parameters.AddWithValue("@tile4", obj.Tile4Angle);
                cmd.Parameters.AddWithValue("@tile5", obj.Tile5Angle);
                cmd.Parameters.AddWithValue("@tile6", obj.Tile6Angle);
                cmd.Parameters.AddWithValue("@tile7", obj.Tile7Angle);
                cmd.Parameters.AddWithValue("@tile8", obj.Tile8Angle);
                cmd.Parameters.AddWithValue("@tile9", obj.Tile9Angle);

                int numberOfRows = cmd.ExecuteNonQuery();
                if(numberOfRows >= 1)
                {
                    result = true;
                }
                database.CloseConnection();
            }

            return result;
        }

        public CaptchaAttributes GetCaptchaAttribute_ById(long captchaAttributeId)
        {

            string query = "SELECT * FROM captcha_attributes WHERE CaptchaAttributeId = @captchaAttributeId";
            if (database.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, database.Connection);
                cmd.Parameters.AddWithValue("@captchaAttributeId", captchaAttributeId);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    string tempHolder = dataReader["CaptchaId"].ToString();
                    obj.CaptchaID = Convert.ToInt16(tempHolder);

                    tempHolder = dataReader["CaptchaAttributeId"].ToString();
                    obj.CaptchaAttributeID = Convert.ToInt16(tempHolder);

                    tempHolder = dataReader["Tile1Angle"].ToString();
                    obj.Tile1Angle = Convert.ToInt16(tempHolder);

                    tempHolder = dataReader["Tile2Angle"].ToString();
                    obj.Tile2Angle = Convert.ToInt16(tempHolder);

                    tempHolder = dataReader["Tile3Angle"].ToString();
                    obj.Tile3Angle = Convert.ToInt16(tempHolder); 

                    tempHolder = dataReader["Tile4Angle"].ToString();
                    obj.Tile4Angle = Convert.ToInt16(tempHolder);

                    tempHolder = dataReader["Tile5Angle"].ToString();
                    obj.Tile5Angle = Convert.ToInt16(tempHolder);

                    tempHolder = dataReader["Tile6Angle"].ToString();
                    obj.Tile6Angle = Convert.ToInt16(tempHolder);

                    tempHolder = dataReader["Tile7Angle"].ToString();
                    obj.Tile7Angle = Convert.ToInt16(tempHolder);

                    tempHolder = dataReader["Tile8Angle"].ToString();
                    obj.Tile8Angle = Convert.ToInt16(tempHolder);

                    tempHolder = dataReader["Tile9Angle"].ToString();
                    obj.Tile9Angle = Convert.ToInt16(tempHolder);
                }
            }
            return obj;
        }



    }
}
