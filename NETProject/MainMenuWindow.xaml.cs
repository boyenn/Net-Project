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
        private int imageIndex;
        private int difficulty;

        public MainMenuWindow()
        {
            InitializeComponent();

            difficulty = (int)difficultySlider.Value; 
         

            SetActionListeners();
            SetVisibilityComponents();
            SetPersonalInfo();
            SetMarginComponents();
            SetImages();
        }

        public void SetActionListeners() {
            mainMenuButton.Click += MainMenuButtonListener;
            exercisesButton.Click += MainMenuButtonListener;
            manageButton.Click += MainMenuButtonListener;
            logoutButton.Click += MainMenuButtonListener;

            image1.MouseDown += images_MouseDown;
            image2.MouseDown += images_MouseDown;
            image3.MouseDown += images_MouseDown;
            image4.MouseDown += images_MouseDown;
            image5.MouseDown += images_MouseDown; 
            image6.MouseDown += images_MouseDown;


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

        public void images_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image temporaryImage = (Image) e.Source;
            int index = Convert.ToInt32(temporaryImage.Name.Substring(temporaryImage.Name.Length - 1));
            SetFocusImage(index);
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

       
        public void MainMenuButtonListener(object sender, RoutedEventArgs e)
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

        public void ManageMenuButtonListener(object sender, RoutedEventArgs e)
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


        public void SetMarginComponents() {
            if (UserSummary.CurrentUser.UserType == 0)
            {
                usernameLabel.Margin = new Thickness(310,10,0,0);
                usernameLabel2.Margin = new Thickness(310,35,0,0);

                pointsLabel.Margin = new Thickness(398,10,0,0);
                pointsLabel2.Margin = new Thickness(398,35,0,0);

                highscoreLabel.Margin = new Thickness(484, 10, 0, 0);
                highscoreLabel2.Margin = new Thickness(484, 35, 0, 0);
            }
            else {
                usernameLabel.Margin = new Thickness(395, 10, 0, 0);
                usernameLabel2.Margin = new Thickness(395, 35, 0, 0);
            }
        }

        public void SetImages() {
            image1.Source = ImagePath.Bmprel("Resources/Images/hoofdRekenen.jpg");
            image2.Source = ImagePath.Bmprel("Resources/Images/vlaggen.jpg");
            image3.Source = ImagePath.Bmprel("Resources/Images/sorteren.jpg");
            image4.Source = ImagePath.Bmprel("Resources/Images/vlaggen.jpg");
            image5.Source = ImagePath.Bmprel("Resources/Images/fouteZinnen.jpg");
            image6.Source = ImagePath.Bmprel("Resources/Images/vlaggen.jpg");
        }

        public void SetFocusImage(int index)
        {
            border1.BorderBrush = Brushes.White;
            border2.BorderBrush = Brushes.White;
            border3.BorderBrush = Brushes.White;
            border4.BorderBrush = Brushes.White;
            border5.BorderBrush = Brushes.White;
            border6.BorderBrush = Brushes.White;

            switch (index) {
                case 1: border1.BorderBrush = Brushes.Red; break;
                case 2: border2.BorderBrush = Brushes.Red; break;
                case 3: border3.BorderBrush = Brushes.Red; break;
                case 4: border4.BorderBrush = Brushes.Red; break;
                case 5: border5.BorderBrush = Brushes.Red; break;
                case 6: border6.BorderBrush = Brushes.Red; break;
            }
        }
    }
}
