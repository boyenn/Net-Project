using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StreamReader inputStream = null;
        private IList<User> userList;
        private static User currentUser;

        
        public MainWindow()
        {
            InitializeComponent();
            userList = new List<User>();
            ReadTextFile();
            
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (loginTextbox.Text.All(Char.IsLetterOrDigit) && passwordBox.Password.All(Char.IsLetterOrDigit))
            {

                foreach (User user in userList)
                {

                    if (string.Equals(loginTextbox.Text, user.UserName, StringComparison.OrdinalIgnoreCase))
                    {

                        if (passwordBox.Password.Equals(user.UserPassword))
                        {
                            currentUser = user;

                            MainMenuWindow mainMenu = new MainMenuWindow();
                        
                            mainMenu.Show();
                            this.Close();
                        }
                        else {
                            MessageBox.Show("Foutief wachtwoord ingegeven.");
                        }
                    }
                }
            }
            else {

                MessageBox.Show("Velden mogen enkel uit cijfers en letters bestaan en mogen niet leeg zijn.");

            }
        }

        public void ReadTextFile() {

            string line = "";
            string[] words = new string[6];
            string fileToSearch = "Resources/Files/users.txt";
        
            try
            {
                inputStream = File.OpenText(fileToSearch);
                char seperator = ';';

                line = inputStream.ReadLine();

                while (line != null)
                {
                    words = line.Split(seperator);

                    userList.Add(new User(Convert.ToInt32(words[0]), words[1], words[2], Convert.ToInt32(words[3]), Convert.ToInt32(words[4]), Convert.ToInt32(words[5])));

                    line = inputStream.ReadLine();

                }
            }
            catch (FileNotFoundException ex) {
                MessageBox.Show("Error: Bestand niet gevonden: " + fileToSearch);
            }

            if (inputStream != null) {
                inputStream.Close();
            }
        }

        public static User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }

        private void loginTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
