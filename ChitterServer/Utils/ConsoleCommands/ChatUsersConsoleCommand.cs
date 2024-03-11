using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Utils.ConsoleCommands {
    internal class ChatUsersConsoleCommand : IConsoleCommand {
        public string Identifier => "chatusers";

        public void Handle( string[] argv ) {
            throw new NotImplementedException();
        }
    }
}
