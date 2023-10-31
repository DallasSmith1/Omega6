using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega6.Classes.Exceptions
{
    internal class UniversalMatchException : Exception
    {
        public UniversalMatchException() { }
        public UniversalMatchException(string message) : base(message) { }
    }
}
