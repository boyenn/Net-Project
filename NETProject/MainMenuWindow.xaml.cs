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

            
            setActionListeners();
            setVisibilityComponents();
            setPersonalInfo();

            var data = new DataGridPupils { Name1 = "Test1", Points1 = "Test2", Highscore1 = "Test3" };

             dataGridPupils.Items.Add(data);
        }

       
        
       

        public void setActionListeners() {
            mainMenuButton.Click += Button_Click;
            exercisesButton.Click += Button_Click;
            manageButton.Click += Button_Click;
            logoutButton.Click += Button_Click;

            teacherManagementButton.Click += Button2_Click;
            pupilManagementButton.Click += Button2_Click;
            teacherManagementButton.Click += Button2_Click;
        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            switch (Convert.ToString(button.Content)) {
                case "Main Menu": mainMenuTab.IsSelected = true; break;
                case "Oefeningen": exercisesTab.IsSelected = true; break;
                case "Beheer": manageTab.IsSelected = true; break;
                case "Log Uit": MainWindow window = new MainWindow(); 
                                window.Show();
                                this.Close(); break;
            }
        }

        void Button2_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            switch (Convert.ToString(button.Content))
            {
                case "Beheer Oefeningen": excerciseManagementTab.IsSelected = true; break;
                case "Beheer Leerlingen": pupilManagementTab.IsSelected = true; break;
                case "Beheer Leerkrachten": teacherManagementTab.IsSelected = true; break;
               
            }
        }

        public void setVisibilityComponents()
        {
            switch (UserSummary.CurrentUser.UserType) {
                case 0: manageButton.Visibility = Visibility.Hidden; break;
                case 1: teacherManagementButton.Visibility = Visibility.Hidden; break;
                case 2:     break;
            }
          
        }

        public void setPersonalInfo() {
            usernameLabel2.Content = UserSummary.CurrentUser.UserName;
            pointsLabel2.Content = UserSummary.CurrentUser.UserPoints;
            highscoreLabel2.Content = UserSummary.CurrentUser.UserHighscore;
        }

       
        
    }
}
