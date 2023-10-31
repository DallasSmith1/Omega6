using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omega6.Classes.Exceptions
{
    internal class PlayerNotInLobbyException : Exception
    {
        public PlayerNotInLobbyException() { }
        public PlayerNotInLobbyException(string message) : base(message) { }
    }
}
