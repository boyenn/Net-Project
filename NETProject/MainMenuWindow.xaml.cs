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
using System.Windows.Shapes;

namespace NETProject
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : Window
    {
        public MainMenuWindow()
        {
            InitializeComponent();
            MessageBox.Show(MainWindow.CurrentUser.UserName);
      
            
            for (int i = 0; i < 500; i++)
            {
                 UserControl1 uc = new UserControl1("Resources/Images/pxlLogo.png", "PXL JONGE");
                 uc.grid.Background = new SolidColorBrush(Color.FromRgb((byte)(i%255),(byte) (255-(i%255)), (byte)(i%255)));
                 lst.Items.Add(uc);
            }
            
        }
    }
}
