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

      
            addTeacherButton.Click += manageTeachersButtonListener;
            changeTeacherButton.Click += manageTeachersButtonListener;
            deleteTeacherButton.Click += manageTeachersButtonListener;
        }

       
        void managePupilsButtonListener(object sender, RoutedEventArgs e)
        {
            User pupilObject;
            Button button = (Button)e.Source;

           
            switch (Convert.ToString(button.Content))
            {
                case "Toevoegen Leerling":  
                    
                    AddUserWindow addUserWindow = new AddUserWindow(0, this);
                    addUserWindow.ShowDialog();
                    
                    break;
                case "Wachtwoord Wijzigen":

                    pupilObject = (User)dataGridPupils.SelectedItem;

                    if (pupilObject == null)
                    {
                        MessageBox.Show(this, "Geen leerling geselecteerd",
                        "Melding", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        PasswordChangeWindow passwordChangeWindow = new PasswordChangeWindow(pupilObject);
                        passwordChangeWindow.ShowDialog();
                    }
                    
                    break;
                case "Verwijder Leerling":

                    pupilObject = (User)dataGridPupils.SelectedItem;

                    if (pupilObject == null)
                    {
                        MessageBox.Show(this, "Geen leerling geselecteerd",
                        "Melding", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        string message = "Bent u zeker dat u deze leerling wilt verwijderen?";
                        string caption = "Confirmation";
                        MessageBoxButton buttons = MessageBoxButton.YesNo;
                        MessageBoxImage icon = MessageBoxImage.Question;
                        
                        if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
                        {
                           
                           
                            for (int i = 0; i < UserSummary.UserList.Count; i++) {
                                User user = (User) UserSummary.UserList[i];
                                if (user.UserName == pupilObject.UserName)
                                {
                                    UserSummary.UserList.RemoveAt(i);
                                    UserSummary.WriteTextFile();
                                    AddStudentsToList();
                                }
                            }
                        }
                    }
                    break;
                    
            }
        }

 
    

        void manageTeachersButtonListener(object sender, RoutedEventArgs e)
        {
            User teacherObject;
            Button button = (Button)e.Source;

          
            switch (Convert.ToString(button.Content))
            {
                case "Toevoegen Leerkracht":
                    AddUserWindow addUserWindow = new AddUserWindow(1, this);
                  
                    addUserWindow.ShowDialog();
                   
                    break;
                case "Wachtwoord Wijzigen":
                    teacherObject = (User)dataGridTeachers.SelectedItem;

                    if (teacherObject == null)
                    {
                        MessageBox.Show(this, "Geen leerkracht geselecteerd",
                        "Melding", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        PasswordChangeWindow passwordChangeWindow = new PasswordChangeWindow(teacherObject);
                        passwordChangeWindow.ShowDialog();
                    }
                    break;
                case "Verwijder Leerkracht": 

                    teacherObject = (User)dataGridTeachers.SelectedItem;

                    if (teacherObject == null)
                    {
                        MessageBox.Show(this, "Geen leerkracht geselecteerd",
                        "Melding", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        string message = "Bent u zeker dat u deze leerkracht wilt verwijderen?";
                        string caption = "Confirmation";
                        MessageBoxButton buttons = MessageBoxButton.YesNo;
                        MessageBoxImage icon = MessageBoxImage.Question;
                        
                        if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.Yes)
                        {
                            for (int i = 0; i < UserSummary.UserList.Count; i++) {
                                User user = (User) UserSummary.UserList[i];
                                if (user.UserName == teacherObject.UserName)
                                {
                                    UserSummary.UserList.RemoveAt(i);
                                    UserSummary.WriteTextFile();

                                    AddTeachersToList();

                                }
                            }
                        }
                    }
                    break;

            }
        }

        void addUserWindow_Closed(object sender, EventArgs e)
        {
            MessageBox.Show("test");
        }

        void MainMenuButtonListener(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            switch (Convert.ToString(button.Content)) {
                case "Main Menu": 
                    mainMenuTab.IsSelected = true; 
                    break;
                case "Oefeningen": 
                    exercisesTab.IsSelected = true; 
                    break;
                case "Beheer": 
                    manageTab.IsSelected = true; 
                    break;
                case "Log Uit": 
                    MainWindow window = new MainWindow(); 
                    window.Show();
                    this.Close(); 
                    break;
            }
        }

        void ManageMenuButtonListener(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            switch (Convert.ToString(button.Content))
            {
                case "Beheer Oefeningen": 
                    excerciseManagementTab.IsSelected = true; 
                    break;
                case "Beheer Leerlingen": 
                    pupilManagementTab.IsSelected = true; 
                    AddStudentsToList(); 
                    break;
                case "Beheer Leerkrachten": 
                    teacherManagementTab.IsSelected = true; 
                    AddTeachersToList(); 
                    break;
               
            }
        }

        public void AddTeachersToList()
        {

            dataGridTeachers.Items.Clear();
            foreach (User user in UserSummary.UserList)
            {
                if (user.UserType == 1)
                {
                    var data = new User { UserName = user.UserName };
                    dataGridTeachers.Items.Add(data);
                }
            }

        }

        public void AddStudentsToList()
        {

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
                case 0: 
                    manageButton.Visibility = Visibility.Hidden;
                    break;
                case 1: 
                    teacherManagementButton.Visibility = Visibility.Hidden;
                    pointsLabel.Visibility = Visibility.Hidden; 
                    highscoreLabel.Visibility = Visibility.Hidden;  
                    pointsLabel2.Visibility = Visibility.Hidden; 
                    highscoreLabel2.Visibility = Visibility.Hidden; 
                    break;
                case 2: 
                    pointsLabel.Visibility = Visibility.Hidden; 
                    highscoreLabel.Visibility = Visibility.Hidden;  
                    pointsLabel2.Visibility = Visibility.Hidden; 
                    highscoreLabel2.Visibility = Visibility.Hidden; 
                    break;
            }
          
        }
        
        public void SetPersonalInfo() {
            usernameLabel2.Content = UserSummary.CurrentUser.UserName;
            pointsLabel2.Content = UserSummary.CurrentUser.UserPoints;
            highscoreLabel2.Content = UserSummary.CurrentUser.UserHighscore;
        }

       
        
    }
}
