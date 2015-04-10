using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NETProject
{
    public static class UserSummary
    {
        private static StreamReader inputStream = null;
        private static IList<User> userList = new List<User>();
        private static User currentUser;

        public static void ReadTextFile()
        {
            userList.Clear();
            string line = "";
            string[] words = new string[5];
            string fileToSearch = "Resources/Files/users.txt";

            try
            {
                inputStream = File.OpenText(fileToSearch);
                char seperator = ';';

                line = inputStream.ReadLine();

                while (line != null)
                {
                    words = line.Split(seperator);

                    userList.Add(new User(words[0], words[1], Convert.ToInt32(words[2]), Convert.ToInt32(words[3]), Convert.ToInt32(words[4])));

                    line = inputStream.ReadLine();

                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Error: Bestand niet gevonden: " + fileToSearch);
            }

            if (inputStream != null)
            {
                inputStream.Close();
            }
        }

        public static void WriteTextFile()
        {
            
            StreamWriter outputStream = File.CreateText("Resources/Files/users.txt");

            foreach (User user in userList)
            {
                outputStream.WriteLine(user.UserName + ";" + user.UserPassword + ";" + user.UserType + ";" + user.UserPoints + ";" + user.UserHighscore);
            }

            outputStream.Close();

        }


        public static User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }
        public static IList<User> UserList
        {
            get { return UserSummary.userList;  }
            set { UserSummary.userList = value; WriteTextFile(); }
        }
       
    }
}
