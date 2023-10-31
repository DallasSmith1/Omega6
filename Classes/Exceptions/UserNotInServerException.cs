using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega6.Classes.Exceptions
{
    internal class UserNotInServerException : Exception
    {
        public UserNotInServerException() { }
        public UserNotInServerException(string message) : base(message) { }
    }
}
