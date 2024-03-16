using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat.Users {
    internal class UserNotFoundException : Exception {
        internal string Username { get; }
        internal UserNotFoundException( string username ) {
            this.Username = username;
        }
    }
}
