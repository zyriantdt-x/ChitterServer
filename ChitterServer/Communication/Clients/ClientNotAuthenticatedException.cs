using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Clients {
    internal class ClientNotAuthenticatedException : Exception {
        internal string Ip { get; }
        internal string WsMessage { get; }
        internal ClientNotAuthenticatedException(string ip) {
            this.Ip = ip;
        }

        internal ClientNotAuthenticatedException( string ip, string message ) {
            this.Ip = ip;
            this.WsMessage = message;
        }
    }
}
