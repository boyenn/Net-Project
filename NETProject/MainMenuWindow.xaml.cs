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

            
            SetActionListeners();
            SetVisibilityComponents();
            SetPersonalInfo();
        }

        public void SetActionListeners() {
            mainMenuButton.Click += MainMenuButtonListener;
            exercisesButton.Click += MainMenuButtonListener;
            manageButton.Click += MainMenuButtonListener;
            logoutButton.Click += MainMenuButtonListener;

            excercisesManagementButton.Click += ManageMenuButtonListener;
            pupilManagementButton.Click += ManageMenuButtonListener;
            teacherManagementButton.Click += ManageMenuButtonListener;

            addPupilButton.Click += managePupilsButtonListener;
            changePupilButton.Click += managePupilsButtonListener;
            deletePupilButton.Click += managePupilsButtonListener;
        }

       
        void managePupilsButtonListener(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            switch (Convert.ToString(button.Content))
            {
                case "Toevoegen Leerling":  break;
                case "Wachtwoord Wijzigen":  break;
                case "Verwijder Leerling": break;
           
            }
        }

        void MainMenuButtonListener(object sender, RoutedEventArgs e)
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

        void ManageMenuButtonListener(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            switch (Convert.ToString(button.Content))
            {
                case "Beheer Oefeningen": excerciseManagementTab.IsSelected = true; break;
                case "Beheer Leerlingen": pupilManagementTab.IsSelected = true; AddStudentsToList(); break;
                case "Beheer Leerkrachten": teacherManagementTab.IsSelected = true; break;
               
            }
        }



        public void AddStudentsToList() {

            dataGridPupils.Items.Clear();
            foreach (User user in UserSummary.UserList)
            {
                if (user.UserType == 0)
                {
                    var data = new User { UserName = user.UserName, UserPoints = user.UserPoints, UserHighscore = user.UserHighscore };
                    dataGridPupils.Items.Add(data);
                }
            }
            
        }

        public void ShowDetails(object sender, RoutedEventArgs e)
        {
            User obj = ((FrameworkElement)sender).DataContext as User;
            MessageBox.Show(obj.UserName);
        }


        public void SetVisibilityComponents()
        {
            switch (UserSummary.CurrentUser.UserType) {
                case 0: manageButton.Visibility = Visibility.Hidden; break;
                case 1: teacherManagementButton.Visibility = Visibility.Hidden; break;
                case 2:     break;
            }
          
        }

        public void SetPersonalInfo() {
            usernameLabel2.Content = UserSummary.CurrentUser.UserName;
            pointsLabel2.Content = UserSummary.CurrentUser.UserPoints;
            highscoreLabel2.Content = UserSummary.CurrentUser.UserHighscore;
        }

       
        
    }
}
