using DatabaseManager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
        public Random rnd = new Random();

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

            Image img = Image.FromFile(currentDirectory + @"\SourceImg\" + imageName);
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
                        // Apply image filter
                        Bitmap resultBitmap = ApplyRandomFilters(bmpCroppedImage);

                        string fileName = captchaId + "-" + i + "-" + j + ".png";
                        //bmpCroppedImage.Save(currentDirectory + @"\OutputImg\" + fileName);
                        resultBitmap.Save(currentDirectory + @"\OutputImg\" + fileName);
                        imageDegreeLst.Add(rotationDegreeInt);
                        // Create imageURL image
                        ImageURL imageURL = new ImageURL();
                        imageURL.URL = @"http://122.11.177.14:9999/OutputImg/" + fileName;
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
            CaptchaSession captchaSession = new CaptchaSession();
            captchaSession = captchaSession.GetCaptchaSession(captchaId);

            if (captchaSession.IsValid)
            {
                captchaAttributes = captchaAttributes.GetCaptchaAttribute_ById(captchaId);

                var singleImageString = dataString.Split(';');

                for (int i = 0; i < singleImageString.Length; i++)
                {
                    if (String.IsNullOrWhiteSpace(singleImageString[i]))
                    {
                        break;
                    }
                    // CODE OPTIMIZATION HERE
                    if (i == 0 || result)
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

                captchaSession.UpdateIsValid(captchaId, false);
                DeleteCaptchaImage(captchaId);
            }


            return result;
        }

        #region Utility Methods

        private bool isImageDegreeCorrect(int currentDegree, short rotatedDegree)
        {
            bool result = false;
            int totalCompletedTurns = currentDegree / 360; // Will only return int
            int degreeTurnedUnder360 = currentDegree - (360 * totalCompletedTurns);
            int finalDegree = degreeTurnedUnder360 + rotatedDegree;
            if (rotatedDegree == 0)
            {
                if (finalDegree == 0)
                {
                    result = true;
                }
            }
            else
            {
                if (finalDegree == 360)
                {
                    result = true;
                }
            }
            return result;
        }

        private HashSet<int> GetRandomExcludedNumbers()
        {
            HashSet<int> excludedNumbers = new HashSet<int>();

            for (int i = 0; i < 3; i++)
            {
                var range = Enumerable.Range(1, 9).Where(x => !excludedNumbers.Contains(x));
                int index = rnd.Next(0, 9 - excludedNumbers.Count);
                excludedNumbers.Add(range.ElementAt(index));
            }
            return excludedNumbers;
        }

        private RotateFlipType GetRotationDegree(ref short rotationDegreeInt)
        {
            int rotateType = rnd.Next(1, 4);
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
            var fileArray = dirInfo.EnumerateFiles("*.*", SearchOption.AllDirectories).ToArray();
            string fileName = fileArray[rnd.Next(fileArray.Length)].Name;

            return fileName;
        }

        private Bitmap ApplyRandomFilters(Bitmap sourceBitmap)
        {
            // Apply image filter
            int randomNumber = rnd.Next(1, 4);
            Bitmap resultBitmap = null;
            switch (randomNumber)
            {
                case 1:
                    resultBitmap = ApplyColorTint(sourceBitmap, 0, 0.15F, 0);
                    break;
                case 2:
                    resultBitmap = ApplyColorTint(sourceBitmap, 0.15F, 0, 0);
                    break;

                case 3:
                    resultBitmap = ApplyColorTint(sourceBitmap, 0, 0, 0.15F);
                    break;
            }

            //Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height, PixelFormat.Format32bppArgb);
            //using (Graphics graphics = Graphics.FromImage(resultBitmap))
            //{
            //    ImageAttributes bmpAttributes = new ImageAttributes();
            //    ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            //                        {
            //                                        new float[] {.3f, .3f, .3f, 0, 0},
            //                                        new float[] {.59f, .59f, .59f, 0, 0},
            //                                        new float[] {.11f, .11f, .11f, 0, 0},
            //                                        new float[] {0, 0, 0, 1, 0},
            //                                        new float[] {0, 0, 0, 0, 1}
            //                        });
            //    bmpAttributes.SetColorMatrix(colorMatrix);

            //    graphics.DrawImage(sourceBitmap, new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
            //                     0, 0, sourceBitmap.Width, sourceBitmap.Height, GraphicsUnit.Pixel, bmpAttributes);
            //}

            return resultBitmap;
        }

        private Bitmap ApplyColorTint(Bitmap sourceBitmap, float redTint, float blueTint, float greenTint)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                           sourceBitmap.Width, sourceBitmap.Height),
                           ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            float blue = 0;
            float green = 0;
            float red = 0;


            for (int k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                blue = pixelBuffer[k] + (255 - pixelBuffer[k]) * blueTint;
                green = pixelBuffer[k + 1] + (255 - pixelBuffer[k + 1]) * greenTint;
                red = pixelBuffer[k + 2] + (255 - pixelBuffer[k + 2]) * redTint;


                if (blue > 255)
                { blue = 255; }


                if (green > 255)
                { green = 255; }


                if (red > 255)
                { red = 255; }


                pixelBuffer[k] = (byte)blue;
                pixelBuffer[k + 1] = (byte)green;
                pixelBuffer[k + 2] = (byte)red;


            }
            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                    resultBitmap.Width, resultBitmap.Height),
                                    ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            resultBitmap.UnlockBits(resultData);
            return resultBitmap;
        }

        private void DeleteCaptchaImage(int captchaId)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    string fileName = currentDirectory + @"\OutputImg\" + captchaId + "-" + i + "-" + j + ".png";
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }

                }
            }
        }

        #endregion
    }
}
