using ChitterServer.Communication.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat.Users {
    internal class ChatUser {
        private string _Uuid;
        private string _Username;

        private CommunicationClient _CommunicationClient;

        internal ChatUser() {

        }

        internal static ChatUser TryAuthenticate( CommunicationClient communication_client, string username, string password ) {
            if( String.IsNullOrWhiteSpace( username ) )
                throw new ArgumentNullException( username );

            if( String.IsNullOrWhiteSpace( password ) )
                throw new ArgumentNullException( password );

            if( username == "ellis" && password == "password" ) {

            } else {
                return null;
            }

            throw new NotImplementedException();
        }

        internal string Uuid { get => _Uuid; }
        internal string Username { get => _Username; }
    }
}
