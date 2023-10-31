using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega6.Classes.Exceptions
{
    public class InGameNameAlreadyInUseException : Exception
    {
        public InGameNameAlreadyInUseException() { }
        public InGameNameAlreadyInUseException(string message) : base(message) { }
    }
}
