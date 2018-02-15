using Microsoft.Practices.Unity;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageServiceModuleLibrary.Services
{
    public class ImageService
    {

        public ImageService()
        {
        }

        public BitmapImage RetrieveImage(string path)
        {
            if (File.Exists(path))
            { 
                return GenerateImageData(path);
            }

            String AbsolutePart = AppDomain.CurrentDomain.BaseDirectory;
            String newpath = AbsolutePart + path;

            if (File.Exists(newpath))
            {
                return GenerateImageData(newpath);
            }
            return null;
        }

        private BitmapImage GenerateImageData(string path)
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                    return bitmapImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return null;
        }

        public string SaveImage(string path, long id)
        {
            string NewPicturePath = GenerateSavePath(path, id);

            //if old path and new path are the same then do nothing
            if (path == NewPicturePath || path =="") return path;

            try
            {
                if (File.Exists(NewPicturePath))
                {
                    File.Delete(NewPicturePath);
                }

                if (File.Exists(path))
                {
                    File.Copy(path, NewPicturePath);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return NewPicturePath;
        }

        private String GenerateSavePath(String p, long i)
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + "szafa\\picture" + i.ToString() + Path.GetExtension(p); ;
        }
    }
}
