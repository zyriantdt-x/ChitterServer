using ChitterServer.Chat.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Handlers.Outgoing {
    internal class AuthenticationOKHandler : OutgoingMessage {
        internal AuthenticationOKHandler( ChatUser chat_user )
            : base( "AUTHENTICATION_OK" ) {
            base.AppendObject( "token", "69-420" );
            base.AppendObject( "uuid", chat_user.Uuid );
            base.AppendObject( "username", chat_user.Username );
        }
    }
}
