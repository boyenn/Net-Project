using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETProject
{
    public class User
    {
        private int userId, userType, userPoints, userHighscore;
        private string userName, userPassword;
 
        public User() { 

        }

        public User(int userId, string userName, string userPassword, int userType, int userPoints, int userHighscore)
        {
            this.userId = userId;
            this.userName = userName;
            this.userPassword = userPassword;
            this.userType = userType;
            this.userPoints = userPoints;
            this.userHighscore = userHighscore;
        }

        public string UserName
        {
            get { return userName; }
        }

        public int UserPoints
        {
            get { return userPoints; }
        }

        public int UserHighscore
        {
            get { return userHighscore; }
        }

        public string UserPassword
        {
            get { return userPassword; }
        }

        public int UserType
        {
            get { return userType; }
        }

    }
}
