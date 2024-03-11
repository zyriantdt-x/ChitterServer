using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Handlers.Outgoing {
    internal class ChatMessageHandler : OutgoingMessage {
        internal ChatMessageHandler( DateTime timestamp, string sender_display_name, string message )
            : base( "CHAT_MESSAGE" ) {
            base.AppendObject( "timestamp", timestamp );
            base.AppendObject( "sender_display_name", sender_display_name );
            base.AppendObject( "message", message );
        }
    }
}