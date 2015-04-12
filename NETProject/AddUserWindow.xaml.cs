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
        public AddUserWindow()
        {
            InitializeComponent();
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
                passwordQuestionLabel.Content = "Gebruikersnaam is in orde.";
            }
            else
            {
                passwordQuestionLabel.Content = "Enkel cijfers en letters. Moet tussen 6-20 karakters zijn.";
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
            if (word.All(Char.IsLetterOrDigit) && addPassword.Password.Length >= 6 && word.Length <= length)
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
            if (addPassword2.Password.Equals(addPassword2.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
