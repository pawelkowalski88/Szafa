using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SzafaInterfaces
{
    public interface IImageService
    {
        BitmapImage RetrieveImage(string path);
        string SaveImage(string path, long id);
    }
}
