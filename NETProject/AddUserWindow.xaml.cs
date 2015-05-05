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
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        private int addUserType;
        private MainMenuWindow window;
        public AddUserWindow()
        {
            InitializeComponent();
            
            
        }

        public AddUserWindow(MainMenuWindow window)
        {
            InitializeComponent();
           

        }
        public AddUserWindow(int addUserType, MainMenuWindow window)
        {
            InitializeComponent();
            this.addUserType = addUserType;
            this.window = window;
        }
        

        private void addPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (stringCheck(addPassword.Password, 20))
            {
                passwordQuestionLabel.Content = "Wachtwoord is in orde.";
            }
            else
            {
                passwordQuestionLabel.Content = "Enkel cijfers en letters. Moet tussen 6-20 karakters zijn.";
            }
        }

        private void addPassword2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (stringCheck(addPassword.Password, 20))
            {
                if (passwordCheck())
                {
                    passwordQuestionLabel1.Content = "Correct.";
                }
                else
                {
                    passwordQuestionLabel1.Content = "Wachtwoord komt niet overeen";
                }
            }
            else
            {
                passwordQuestionLabel1.Content = "";
            }
        }

        private void addUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (stringCheck(addUsername.Text, 20))
            {
                addUsernameLabel.Content = "Gebruikersnaam is in orde.";
            }
            else
            {
                addUsernameLabel.Content = "Enkel cijfers en letters. Moet tussen 6-20 karakters zijn.";
            }
        }
        

        private void passwordCancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void passwordClearButton_Click(object sender, RoutedEventArgs e)
        {
            addPassword.Password = "";
            addPassword2.Password = "";
            addUsername.Text = "";
        }

        public Boolean stringCheck(string word, int length)
        {
            if (word.All(Char.IsLetterOrDigit) && word.Length >= 6 && word.Length <= length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean passwordCheck()
        {
            if (addPassword2.Password.Equals(addPassword.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void passwordOkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Boolean found = false;
                if (stringCheck(addUsername.Text, 20))
                {
                    if (stringCheck(addPassword.Password, 20))
                    {
                        if (passwordCheck())
                        {
                            foreach (User user in UserSummary.UserList)
                            {
                                if (string.Equals(user.UserName, addUsername.Text, StringComparison.OrdinalIgnoreCase))
                                {
                                    found = true;
                                }
                            }
                            if (!found)
                            {
                                UserSummary.UserList.Add(new User(addUsername.Text, addPassword.Password, addUserType, 0, 0));
                                UserSummary.WriteTextFile();
                                MessageBox.Show("Gebruiker toegevoegd.", "Wachtwoord Veranderd.", MessageBoxButton.OK, MessageBoxImage.Information);
                                window.AddStudentsToList();
                                window.AddTeachersToList();
                                this.Close();
                            }
                            else
                            {
                              
                                throw new UserAlreadyExistException("Gebruiker bestaat al");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Wachtwoorden komen niet overeen", "Wachtwoorden komen niet overeen", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Wachtwoord mag enkel cijfers en letters bevatten en moet tussen 6-20 karakters lang zijn.", "Fout", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Gebruikersnaam mag enkel cijfers en letters bevatten en moet tussen 6-20 karakters lang zijn.", "Fout", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (UserAlreadyExistException ex) {
                MessageBox.Show("Gebruiker bestaat al!", "Gebruiker bestaat al!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
