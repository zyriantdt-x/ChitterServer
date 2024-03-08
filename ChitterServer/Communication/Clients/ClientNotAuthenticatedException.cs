using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Clients {
    internal class ClientNotAuthenticatedException : Exception {
        internal ClientNotAuthenticatedException(string ip) 
            : base($"Attempt made to access ChatUser for an unauthenticated WebSocket client: {ip}") {}
    }
}
