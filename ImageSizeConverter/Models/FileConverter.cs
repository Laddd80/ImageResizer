using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageSizeConverter.Models
{
    public class FileConverter
    {
        public static void ConvertAllImages(string rootFolder, string targetFolder,int size,bool heightOrWidth)
        {
            List<string> imageNames = GetImageNames(rootFolder);

            foreach (string imageName in imageNames)
            {
                string rootString = rootFolder + imageName;
                Image image = Image.FromFile(rootString);

                SaveNewImage(image, size, heightOrWidth,targetFolder ,imageName);
            }
        }

        //Saves the new image. Needs to size and orientation to call the ScaleImage method, and the original name to create the new file name for saving
        private static void SaveNewImage(System.Drawing.Image image, int maxSize, bool width, string targetFolder, string originalName)
        {
            Image newImage = ScaleImage(image, maxSize, width);
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            myEncoder = Encoder.Quality;
            myEncoderParameter = new EncoderParameter(myEncoder, 75L);
            
            myImageCodecInfo = GetEncoderInfo("image/jpeg");
            string newFileName = NewFileName(maxSize, width, originalName);

            myEncoderParameters = new EncoderParameters();
            myEncoderParameters.Param[0] = myEncoderParameter;

            var path = Path.Combine(targetFolder, newFileName);
            FileStream stream = new FileStream(path, FileMode.Create);
            newImage.Save(stream, myImageCodecInfo, myEncoderParameters);
            stream.Close();
        }

        //Used by SaveNewImage method. 
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        //Used by SaveNewImage method. Cuts the .jped from the file end, and creates new name based on size and resizing orientation.
        private static string NewFileName(int maxSize, bool width, string originalName)
        {
            originalName = originalName.Substring(0, originalName.Length - 4);
            return originalName + "-" + maxSize + "-" + (width ? "w" : "h")+".jpg";
        }

        //Looks at the given folder, and creates a string list of all available JPEG files within the folder.
        private static List<string> GetImageNames(string rootfolder)
        {
            List<string> imageNames = new List<string>();
            try
            {
                string[] filePaths = Directory.GetFiles(rootfolder);

                foreach (var item in filePaths)
                {
                    if (item.EndsWith(".jpg"))
                    {
                        imageNames.Add(Path.GetFileName(item));
                    }                  
                }
            }
            catch (Exception)
            {
                throw;
            }
            return imageNames;
        }

        private static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxSize, bool width)
        {
            /* we will resize image based on the height/width ratio by passing expected height as parameter. Based on Expected height and current image height, new ratio will be arrived and using the same we will do the resizing of image width. */

            double ratio = 0;

            if (width)
            {
                ratio = (double)maxSize / image.Width;
            }
            else
            {
                ratio = (double)maxSize / image.Height;
            }

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
    }
}
