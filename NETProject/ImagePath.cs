using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace NETProject
{
    class ImagePath
    {
        public static BitmapImage Bmprel(string relativepath)
        {
            Uri x = null;
            try
            {
                x = new Uri(Path.GetFullPath(relativepath), UriKind.Absolute);
                return new BitmapImage(x);
            }
            catch (FileNotFoundException ex)
            {
                
            }
            catch (DirectoryNotFoundException ex) { 
            
            }
            return null;
        }
    }
}
