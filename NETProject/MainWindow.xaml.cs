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


//source tabcontrol no header https://social.msdn.microsoft.com/Forums/vstudio/en-US/f500ff28-83e7-439a-ad78-be0e6a3b7587/how-to-hide-the-tab-headers-for-a-tabcontrol?forum=wpf
//datagrid help http://www.wpftutorial.net/datagrid.html
//http://stackoverflow.com/questions/16251327/wpf-datagrid-add-new-row
//http://stackoverflow.com/questions/3046003/adding-a-button-to-a-wpf-datagrid
//messagebox help http://www.c-sharpcorner.com/UploadFile/mahesh/messagebox-in-wpf/

namespace NETProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     
        public  BitmapImage bmprel(string relativepath)
        {
            Uri x = new Uri(System.IO.Path.GetFullPath(relativepath), UriKind.Absolute);
              return new BitmapImage(x);
        }

        public MainWindow()
        {
            InitializeComponent();
          

            UserSummary.ReadTextFile();

            aaa.Source = bmprel("Resources/Images/PxlLogo.png");
            aaa.MouseDown += aaa_MouseDown; // empty function 
            
        }

        void aaa_MouseDown(object sender, MouseButtonEventArgs e)
        {
           //Example voor mouseclick 


        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            UserSummary.WriteTextFile();
            if (loginTextbox.Text.All(Char.IsLetterOrDigit) && passwordBox.Password.All(Char.IsLetterOrDigit))
            {
                Boolean fouteGebruikersnaam = false;
                foreach (User user in UserSummary.UserList)
                {
                        
                    if (string.Equals(loginTextbox.Text, user.UserName, StringComparison.OrdinalIgnoreCase))
                    {

                        if (passwordBox.Password.Equals(user.UserPassword))
                        {
                            UserSummary.CurrentUser = user;
                           
                            MainMenuWindow mainMenu = new MainMenuWindow();
                            
                            mainMenu.Show();
                            this.Close();
                            return;
                        }
                        else {

                            MessageBox.Show("Foutief wachtwoord ingegeven.");
                            return;
                        }
                    }
                    else
                    {
                        fouteGebruikersnaam = true;
                        
                    }
                }
                if (fouteGebruikersnaam) {
                    MessageBox.Show("Foutieve gebruikersnaam ingegeven.");
                   
                }
            }
            else {

                MessageBox.Show("Velden mogen enkel uit cijfers en letters bestaan en mogen niet leeg zijn.");

            }
        }

       

    }
}
