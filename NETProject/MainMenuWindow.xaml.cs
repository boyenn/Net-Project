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

            UserControl1 mainMenuUserControl = new UserControl1("Resources/Images/1.png", "");
            UserControl1 game1UserControl = new UserControl1("Resources/Images/2.jpg", "Game 1");
            UserControl1 game2UserControl = new UserControl1("Resources/Images/3.jpg", "Game 2");
            UserControl1 game3UserControl = new UserControl1("Resources/Images/4.jpg", "Game 3");

            lst.Items.Add(mainMenuUserControl);
            lst.Items.Add(game1UserControl);
            lst.Items.Add(game2UserControl);
            lst.Items.Add(game3UserControl);
                 
            
        }
    }
}
