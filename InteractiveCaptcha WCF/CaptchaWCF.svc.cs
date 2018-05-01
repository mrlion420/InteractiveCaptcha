using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace Interactive_Captcha
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CaptchaWCF : ICaptchaWCF
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<ImageURL> GetCaptcha()
        {
            
            List<ImageURL> lstImageURL = new List<ImageURL>();
            string currentDirectory = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            string imageFilePath = currentDirectory + @"\OutputImg\";
            Image img = Image.FromFile(currentDirectory + @"\SourceImg\1.jpg");
            Bitmap bmpImg = new Bitmap(img);
            int width = bmpImg.Width;
            int height = bmpImg.Height;
            int widthCropSize = width / 3;
            int heightCropSize = height / 3;

            int currentX = 0;
            int currentY = 0;

            for (int i = 0; i < 3; i++)
            {
                if (i != 0)
                {
                    currentY += heightCropSize;
                }
                // Reset X coordinates when Y is changed
                currentX = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (j != 0)
                    {
                        currentX += widthCropSize;
                    }
                    Rectangle cropArea = new Rectangle(currentX, currentY, widthCropSize, heightCropSize);
                    //cropArea.Intersect(new Rectangle(0, 0, bmpImg.Width, bmpImg.Height));
                    Bitmap bmpCroppedImage = bmpImg.Clone(cropArea, System.Drawing.Imaging.PixelFormat.DontCare);
                    string fileName = i + "-" + j + ".png";
                    bmpCroppedImage.Save(currentDirectory + @"\OutputImg\" + fileName);

                    ImageURL imageURL = new ImageURL();
                    imageURL.URL = imageFilePath + fileName;
                    lstImageURL.Add(imageURL);
                }
            }
            return lstImageURL;
        }

        public int GetRandomImageName()
        {
            //using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            //{
            //    byte[] randomByte = new byte[5];
            //    rng.GetBytes(randomByte);
            //    result = BitConverter.ToInt32(randomByte, 0);
            //}
            Random rnd = new Random();
            int result = rnd.Next(1, 13); // creates a number between 1 and 12

            return result;
        }

        public bool CheckResult(long sessionId)
        {
            return true;
        }

        #region Utility Methods
   
        public string GetRandomImageFromFolder()
        {
            return string.Empty;
        }

        #endregion
    }
}
