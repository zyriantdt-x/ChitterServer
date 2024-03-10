using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Handlers.Outgoing {
    internal class AuthenticationFailedHandler : OutgoingMessage {
        internal AuthenticationFailedHandler()
            : base("AUTHENTICATION_FAILED") { }
    }
}
