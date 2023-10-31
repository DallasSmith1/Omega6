using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega6.Classes.Exceptions
{
    internal class GameExceededTimeException : Exception
    {
        public GameExceededTimeException() { }
        public GameExceededTimeException(string message) : base(message) { }
    }
}
