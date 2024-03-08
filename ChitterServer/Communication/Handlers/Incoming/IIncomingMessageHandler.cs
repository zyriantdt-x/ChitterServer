using ChitterServer.Communication.Clients;
using ChitterServer.Communication.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Handlers.Incoming {
    internal interface IIncomingMessageHandler {
        string Identifier { get; }

        // we can safely (?) assume that args passed here will never be null
        void Handle( CommunicationClient communication_client, MessageStructure message );
    }
}
