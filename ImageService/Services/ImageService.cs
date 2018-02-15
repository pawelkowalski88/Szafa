using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageServiceModuleLibrary.Services
{
    public class ImageService
    {
        public ImageService(IUnityContainer container)
        {
            this.container = container;
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
        IUnityContainer container;
    }

}
