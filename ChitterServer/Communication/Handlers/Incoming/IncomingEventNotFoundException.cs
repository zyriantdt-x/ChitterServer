using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Handlers.Incoming {
    internal class IncomingEventNotFoundException : Exception {
        internal IncomingEventNotFoundException( string identifier )
            : base( $"Unable to find incoming event '{identifier}'" ) { }
    }
}
