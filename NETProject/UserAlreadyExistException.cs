using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETProject
{
    class UserAlreadyExistException : ApplicationException
    {
        public UserAlreadyExistException(string message)
            : base()
        { 
            
        }
    }
}
