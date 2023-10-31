using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega6.Classes.Exceptions
{
    public class UserAlreadyInQueueException : Exception
    {
        public UserAlreadyInQueueException() { }
        public UserAlreadyInQueueException(string message) : base(message) { }
    }
}
