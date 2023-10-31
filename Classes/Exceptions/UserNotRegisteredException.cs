using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega6.Classes.Exceptions
{
    public class UserNotRegisteredException : Exception
    {
        public UserNotRegisteredException() { }
        public UserNotRegisteredException(string message) : base(message) { }
    }
}
