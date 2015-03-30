using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NETProject
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public BitmapImage bmprel(string relativepath)
        {
            Uri x = new Uri(System.IO.Path.GetFullPath(relativepath), UriKind.Absolute);
            return new BitmapImage(x);
        }
        public UserControl1(String imgpath, String titel)
        {

            InitializeComponent();
            img.Source = bmprel(imgpath);
            img.Stretch = Stretch.Fill;
            Titel.Content = titel;
        }
    }
}
