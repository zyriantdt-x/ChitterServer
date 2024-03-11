using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Utils.ConsoleCommands {
    internal class ConsoleCommandNotFoundException : Exception {
        internal ConsoleCommandNotFoundException( string identifier )
            : base( $"Attempt to find non-existing ConsoleCommand '{identifier}'" ) { }
    }
}
