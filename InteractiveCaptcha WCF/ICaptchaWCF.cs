using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Interactive_Captcha
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICaptchaWCF
    {

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        bool CheckResult(int captchaId, string dataString);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json)]
        List<ImageURL> GetCaptcha();

        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "*")]
        void GetOptions();

    }

    [DataContract]
    public class ImageURL
    {

        [DataMember]
        public int CaptchaId { get; set; }


        [DataMember]
        public double Degree { get; set; }


        [DataMember]
        public string Base64String { get; set; }

    }
}
