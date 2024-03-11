using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Utils.ConsoleCommands {
    interface IConsoleCommand {
        string Identifier { get; }

        void Handle( string[] argv );
    }
}
