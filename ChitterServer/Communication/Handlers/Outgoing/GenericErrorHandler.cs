using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Handlers.Outgoing {
    internal class GenericErrorHandler : OutgoingMessage {
        internal GenericErrorHandler(string error, string description)
            : base("GENERIC_ERROR") {
            base.AppendObject( "error", error );
            base.AppendObject( "description", description );
        }
    }
}
