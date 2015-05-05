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
    /// Interaction logic for BinControl.xaml
    /// </summary>
    public partial class BinControl : UserControl
    {
        private string wasteBinType; 
        public BinControl(string image)
        {
            InitializeComponent();
        
            setType(Convert.ToInt32(image.Substring(26, 1)));
            wasteBinImage.Source = ImagePath.Bmprel(image);
            wasteLabel.Content = wasteBinType;
        }

        public string WasteBinType
        {
            get { return wasteBinType; }
            set { wasteBinType = value; }
        }
        private void setType(int number) {
            switch (number)
            {
                case 1:
                    wasteBinType = "PMD";
                    break;
                case 2:
                    wasteBinType = "GFT";
                    break;
                case 3:
                    wasteBinType = "Papier en Karton";
                    break;
                case 4:
                    wasteBinType = "Glas";
                    break;
                case 5:
                    wasteBinType = "Restafval";
                    break;
            }
        }
    }
}
