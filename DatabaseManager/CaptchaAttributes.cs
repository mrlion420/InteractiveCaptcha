using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    public class CaptchaAttributes
    {
        private long Id;
        private long captchaSessionId;
        private string attributeName;
        private string attributeValue;

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


        public long CaptchaSessionID
        {
            get { return captchaSessionId; }
            set { captchaSessionId = value; }
        }


        public long ID
        {
            get { return Id; }
            set { Id = value; }
        }

    }
}
