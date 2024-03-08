using ChitterServer.Communication.Clients;
using ChitterServer.Communication.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Handlers.Incoming {
    internal class RequestAuthenticateHandler : IIncomingMessageHandler {
        public string Identifier => "REQUEST_AUTHENTICATE";

        public void Handle( CommunicationClient communication_client, MessageStructure message ) {
            
        }
    }
}
