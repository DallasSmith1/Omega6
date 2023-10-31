using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega6.Classes.Exceptions
{
    public class TooLateException : Exception
    {
        public TooLateException() { }

        public TooLateException(string message) : base(message) { }
    }
}
