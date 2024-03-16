using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Utils.ConsoleCommands {
    internal class ConsoleCommandNotFoundException : Exception {
        internal string Identifier { get; }
        internal ConsoleCommandNotFoundException( string identifier ) {
            this.Identifier = identifier;
        }
    }
}
