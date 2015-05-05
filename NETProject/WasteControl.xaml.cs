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
    /// Interaction logic for WasteControl.xaml
    /// </summary>
    public partial class WasteControl : UserControl
    {
        private string wasteType;
        private string image;

        public WasteControl()
        {

        }
        public WasteControl(string image, string type)
        {
            InitializeComponent();
            this.image = image;
            wasteImage.Source = ImagePath.Bmprel(image);
            wasteType = type;
        }

        public string WasteType
        {
            get { return wasteType; }
            set { wasteType = value; }
        }

        public string Image
        {
            get { return image; }
            set { image = value; }
        }
    }
}
