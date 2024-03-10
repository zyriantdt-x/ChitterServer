using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat.Users {
    internal class UserNotFoundException : Exception {
        internal UserNotFoundException( string username )
            : base( $"Attempt to authenticate '{username}' failed" ) { }
    }
}
