using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega6.Classes.Exceptions
{
    public class AnotherAccountAlreadyInUseException : Exception
    {
        public AnotherAccountAlreadyInUseException() { }
        public AnotherAccountAlreadyInUseException(string message) : base(message) { }
    }
}
