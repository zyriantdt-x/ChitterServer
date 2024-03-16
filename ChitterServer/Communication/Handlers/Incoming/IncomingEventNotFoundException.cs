using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Handlers.Incoming {
    internal class IncomingEventNotFoundException : Exception {
        internal string Identifier { get; }
        internal IncomingEventNotFoundException( string identifier ) {
            this.Identifier = identifier;
        }
    }
}
