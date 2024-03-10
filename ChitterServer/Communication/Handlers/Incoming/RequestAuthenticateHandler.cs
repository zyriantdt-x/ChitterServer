using ChitterServer.Chat.Users;
using ChitterServer.Communication.Clients;
using ChitterServer.Communication.Handlers.Outgoing;
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
            if( communication_client.IsAuthenticated )
                return; // icl lets just discard this

            object username_as_obj;
            object password_as_obj;

            // verify args
            if( !message.Body.TryGetValue( "username", out username_as_obj ) ) {
                // reject
            }

            if( !message.Body.TryGetValue( "password", out password_as_obj ) ) {
                // reject
            }

            ChatUser chat_user;

            // attempt to authenticate
            try {
                chat_user = ChatUser.TryAuthenticate( communication_client, Convert.ToString( username_as_obj ), Convert.ToString( password_as_obj ) );
            } catch( UserNotFoundException ) {
                communication_client.Send( new AuthenticationFailedHandler() );
                return;
            } catch( Exception ex ) {
                // a worse exception
                return;
            }

            // auth OK
            ChitterEnvironment.ChatManager.ChatUserManager.RegisterChatUser( chat_user );
            communication_client.Authenticate( chat_user );
            communication_client.Send( new AuthenticationOKHandler( chat_user ) );
        }
    }
}
