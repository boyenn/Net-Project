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
    /// Interaction logic for FlagControl.xaml
    /// </summary>
    public partial class FlagControl : UserControl
    {
        private string land;
        private int x, y;
        private bool inCanvas = false;

        public FlagControl(string image, string land)
        {
            InitializeComponent();
            flagImage.Source = ImagePath.Bmprel(image);
            this.land = land;
            nameLabel.Content = GetLand();
        }
        public FlagControl(string image, string land, int x, int y)
        {
            InitializeComponent();
            flagImage.Source = ImagePath.Bmprel(image);
            this.land = land;
            this.x = x;
            this.y = y;
            nameLabel.Content = GetLand();
        }

        public string GetLand()
        {
            return Convert.ToString(land);
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public bool InCanvas
        {
            get { return inCanvas; }
            set { inCanvas = value; }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (inCanvas)
            {
                nameLabel.Visibility = Visibility.Visible;
            }
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (inCanvas)
            {
                nameLabel.Visibility = Visibility.Hidden;
            }
        }
    }
}
