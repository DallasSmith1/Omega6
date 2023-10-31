using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega6.Classes.Exceptions
{
    internal class TooEarlyException : Exception
    {
        public TooEarlyException() { }
        public TooEarlyException(string message) : base(message) { }
    }
}
