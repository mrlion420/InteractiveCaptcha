﻿using DatabaseManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        public string currentDirectory = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;

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

        public void GetOptions()
        {
        }

        public List<ImageURL> GetCaptcha()
        {
            List<ImageURL> lstImageURL = new List<ImageURL>();

            string imageFilePath = currentDirectory + @"\OutputImg\";
            string imageName = GetRandomImageName();

            Image img = Image.FromFile(currentDirectory + @"\SourceImg\" + imageName + ".jpg");
            Bitmap bmpImg = new Bitmap(img);
            int width = bmpImg.Width;
            int height = bmpImg.Height;
            int widthCropSize = width / 3;
            int heightCropSize = height / 3;

            int currentX = 0;
            int currentY = 0;
            int currentImageCount = 1;
            int captchaId = 0;

            List<short> imageDegreeLst = new List<short>();

            HashSet<int> excludedNumbers = GetRandomExcludedNumbers();

            CaptchaSession captchaSession = new CaptchaSession();
            captchaSession.ImageName = imageName;

            if (captchaSession.Insert(ref captchaId))
            {
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
                        short rotationDegreeInt = 0;
                        // Check if the current image is excluded from rotating or not 
                        if (!excludedNumbers.Contains(currentImageCount))
                        {
                            RotateFlipType rotationDegree = GetRotationDegree(ref rotationDegreeInt);
                            // Apply rotation
                            bmpCroppedImage.RotateFlip(rotationDegree);
                        }

                        string fileName = captchaId + "-" + i + "-" + j + ".png";
                        bmpCroppedImage.Save(currentDirectory + @"\OutputImg\" + fileName);
                        imageDegreeLst.Add(rotationDegreeInt);
                        // Create imageURL image
                        ImageURL imageURL = new ImageURL();
                        imageURL.URL = imageFilePath + fileName;
                        imageURL.CaptchaId = captchaId;
                        lstImageURL.Add(imageURL);
                        currentImageCount++;
                    }
                }

                CaptchaAttributes captchaAttri = new CaptchaAttributes();
                captchaAttri.CaptchaID = captchaId;
                captchaAttri.Tile1Angle = imageDegreeLst[0];
                captchaAttri.Tile2Angle = imageDegreeLst[1];
                captchaAttri.Tile3Angle = imageDegreeLst[2];
                captchaAttri.Tile4Angle = imageDegreeLst[3];
                captchaAttri.Tile5Angle = imageDegreeLst[4];
                captchaAttri.Tile6Angle = imageDegreeLst[5];
                captchaAttri.Tile7Angle = imageDegreeLst[6];
                captchaAttri.Tile8Angle = imageDegreeLst[7];
                captchaAttri.Tile9Angle = imageDegreeLst[8];

                if (!captchaAttri.Insert())
                {
                    // Return empy list if insertion throws an error
                    return new List<ImageURL>();
                }
            }
            return lstImageURL;
        }

        public bool CheckResult(int captchaId, string dataString)
        {
            bool result = false;
            CaptchaAttributes captchaAttributes = new CaptchaAttributes();
            captchaAttributes = captchaAttributes.GetCaptchaAttribute_ById(captchaId);

            var singleImageString = dataString.Split(';');

            for (int i = 0; i < singleImageString.Length; i++)
            {
                // CODE OPTIMIZATION HERE
                if (i == 0 && !String.IsNullOrWhiteSpace(singleImageString[i]) || result && !String.IsNullOrWhiteSpace(singleImageString[i]))
                {
                    var imageKVP = singleImageString[i].Split('=');
                    short currentDegree = Convert.ToInt16(imageKVP[1]);
                    switch (imageKVP[0])
                    {
                        case "captcha-1":
                            result = isImageDegreeCorrect(currentDegree, captchaAttributes.Tile1Angle);
                            break;

                        case "captcha-2":
                            result = isImageDegreeCorrect(currentDegree, captchaAttributes.Tile2Angle);
                            break;

                        case "captcha-3":
                            result = isImageDegreeCorrect(currentDegree, captchaAttributes.Tile3Angle);
                            break;

                        case "captcha-4":
                            result = isImageDegreeCorrect(currentDegree, captchaAttributes.Tile4Angle);
                            break;

                        case "captcha-5":
                            result = isImageDegreeCorrect(currentDegree, captchaAttributes.Tile5Angle);
                            break;

                        case "captcha-6":
                            result = isImageDegreeCorrect(currentDegree, captchaAttributes.Tile6Angle);
                            break;

                        case "captcha-7":
                            result = isImageDegreeCorrect(currentDegree, captchaAttributes.Tile7Angle);
                            break;

                        case "captcha-8":
                            result = isImageDegreeCorrect(currentDegree, captchaAttributes.Tile8Angle);
                            break;

                        case "captcha-9":
                            result = isImageDegreeCorrect(currentDegree, captchaAttributes.Tile9Angle);
                            break;
                    }
                }
                
            }

            // Update the captcha to invalid regardless of result
            CaptchaSession captchaSession = new CaptchaSession();
            captchaSession.UpdateIsValid(captchaId, false);

            return result;
        }

        #region Utility Methods

        private bool isImageDegreeCorrect(int currentDegree, short rotatedDegree)
        {
            bool result = false;
            int totalCompletedTurns = currentDegree / 360; // Will only return int
            int degreeTurnedUnder360 = currentDegree - (360 * totalCompletedTurns);
            int finalDegree = degreeTurnedUnder360 + rotatedDegree;
            if(rotatedDegree == 0)
            {
                if (finalDegree == 0)
                {
                    result = true;
                }
            }
            else
            {
                if(finalDegree == 360)
                {
                    result = true;
                }
            }
            return result;
        }

        private HashSet<int> GetRandomExcludedNumbers()
        {
            HashSet<int> excludedNumbers = new HashSet<int>();
            var rand = new Random();

            for (int i = 0; i < 3; i++)
            {
                var range = Enumerable.Range(1, 9).Where(x => !excludedNumbers.Contains(x));
                int index = rand.Next(0, 9 - excludedNumbers.Count);
                excludedNumbers.Add(range.ElementAt(index));
            }
            return excludedNumbers;
        }

        private RotateFlipType GetRotationDegree(ref short rotationDegreeInt)
        {
            Random random = new Random();
            int rotateType = random.Next(1, 4);
            RotateFlipType degree = RotateFlipType.RotateNoneFlipNone;
            switch (rotateType)
            {
                case 1:
                    degree = RotateFlipType.Rotate90FlipNone;
                    rotationDegreeInt = 90;
                    break;

                case 2:
                    degree = RotateFlipType.Rotate180FlipNone;
                    rotationDegreeInt = 180;
                    break;

                case 3:
                    degree = RotateFlipType.Rotate270FlipNone;
                    rotationDegreeInt = 270;
                    break;
            }

            return degree;
        }

        private string GetRandomImageName()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(currentDirectory + @"\SourceImg\");
            int fileCount = dirInfo.EnumerateFiles("*.*", SearchOption.AllDirectories).Count();
            Random rnd = new Random();
            int result = rnd.Next(1, fileCount + 1); // creates a number between 1 and 12

            return result.ToString();
        }

        #endregion
    }
}
