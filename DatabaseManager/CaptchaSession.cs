using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    public class CaptchaSession
    {
        private long Id;
        private string imageName;
        private bool isValid;

        public CaptchaSession GetCaptchaSession_ById(long captchaSessionId)
        {
            //SAMPLE CODE
            CaptchaSession obj = new CaptchaSession();
            obj.Id = 1;
            obj.imageName = string.Empty;
            obj.isValid = true;
            return obj;
        }

        public bool Update(CaptchaSession obj)
        {
            // SAMPLE CODE
            // Update all the captcha session attributes into the database
            return true;
        }

        public bool Insert(CaptchaSession obj)
        {
            // SAMPLE CODE
            // Insert all the captcha session attributes into the database
            return true;
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
