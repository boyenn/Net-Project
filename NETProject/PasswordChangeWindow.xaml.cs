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
    /// Interaction logic for PasswordChangeWindow.xaml
    /// </summary>
    public partial class PasswordChangeWindow : Window
    {
        private User passwordUser;

        public PasswordChangeWindow()
        {
            InitializeComponent();
            questionUsernameLabel.Content = "";
        }
        public PasswordChangeWindow(User user)
        {
            InitializeComponent();
            questionUsernameLabel.Content = user.UserName;
            passwordUser = user;
        }

        private void passwordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordCheck1())
            {
                passwordQuestionLabel.Content = "Wachtwoord is in orde.";
            }
            else {
                passwordQuestionLabel.Content = "Enkel cijfers en letters. Moet tussen 6-20 karakters zijn.";
            }
        }

        private void passwordTextBox2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passwordCheck1())
            {
                if (passwordCheck2())
                {
                    passwordQuestionLabel1.Content = "Correct.";
                }
                else {
                    passwordQuestionLabel1.Content = "Wachtwoord komt niet overeen";
                }
            }else{
                passwordQuestionLabel1.Content = "";
            }
            
        }

        public Boolean passwordCheck1() {
            if (passwordTextBox.Password.All(Char.IsLetterOrDigit) && passwordTextBox.Password.Length >= 6 && passwordTextBox.Password.Length <= 20)
            {
                return true;
            }
            else {
                return false;
            }
        }
         public Boolean passwordCheck2() {
             if (passwordTextBox.Password.Equals(passwordTextBox2.Password))
             {
                 return true;
             }
             else
             {
                 return false;
             }
        }

         private void passwordClearButton_Click(object sender, RoutedEventArgs e)
         {
             passwordTextBox.Password = "";
             passwordTextBox2.Password = "";
         }

         private void passwordCancelButton_Click(object sender, RoutedEventArgs e)
         {
             this.Close();
         }

         private void passwordOkButton_Click(object sender, RoutedEventArgs e)
         {
             if (passwordCheck1())
             {
                 if (passwordCheck2())
                 {
                     foreach (User user in UserSummary.UserList) { 
                         if(user.UserName.Equals(passwordUser.UserName)){
                             user.UserPassword = passwordTextBox.Password;
                             UserSummary.WriteTextFile();
                             MessageBox.Show("Wachtwoord Veranderd.", "Wachtwoord Veranderd.",  MessageBoxButton.OK, MessageBoxImage.Information);
                             this.Close();
                         }
                     }
                 }
                 else
                 {
                     MessageBox.Show("Wachtwoord komt niet overeen", "Fout", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                 }
             }
             else
             {
                 MessageBox.Show("Ongeldig wachtwoord", "Fout", MessageBoxButton.OK, MessageBoxImage.Exclamation);
             }
         }
    }
}
