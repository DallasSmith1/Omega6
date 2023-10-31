using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega6.Classes.Exceptions
{
    public class GameNotFoundException : Exception
    {
        public GameNotFoundException() { }
        public GameNotFoundException(string message) : base(message) { }
    }
}
