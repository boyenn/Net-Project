using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETProject
{
    public class User
    {
        private int userType, userPoints, userHighscore;
        private string userName, userPassword;
 
        public User() { 

        }

        public User(string userName, string userPassword, int userType, int userPoints, int userHighscore)
        {
            this.userName = userName;
            this.userPassword = userPassword;
            this.userType = userType;
            this.userPoints = userPoints;
            this.userHighscore = userHighscore;
        }
        private int myVar;

     
        public string UserName
        {
            set { userName = value; }
            get { return userName; }
        }

        public int UserPoints
        {
            set { userPoints = value; }
            get { return userPoints; }
        }

        public int UserHighscore
        {
            set { userHighscore = value; }
            get { return userHighscore; }
        }

        public string UserPassword
        {
            set { userPassword = value; }
            get { return userPassword; }
        }

        public int UserType
        {
            set { userType = value; }
            get { return userType; }
        }

    }
}
