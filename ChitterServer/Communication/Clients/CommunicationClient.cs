using ChitterServer.Chat.Users;
using ChitterServer.Communication.Handlers.Outgoing;
using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Clients {
    internal class CommunicationClient : IDisposable {
        private IWebSocketConnection _WebSocketConnection;

        private bool _IsAuthenticated;

        private ChatUser _ChatUser;

        internal CommunicationClient( IWebSocketConnection web_socket_connection ) {
            this._WebSocketConnection = web_socket_connection;
            this._IsAuthenticated = false;

            ChitterEnvironment.CommunicationManager.CommunicationClientManager.RegisterCommunicationClient( this );
        }

        internal void Send( OutgoingMessage message ) {
            string serialised_message = message.ToString();
            WebSocketConnection.Send( serialised_message );

            string display_name = this._IsAuthenticated ? this._ChatUser.Username : this._WebSocketConnection.ConnectionInfo.ClientIpAddress;
            CommunicationManager.LogOutboundMessage( display_name, serialised_message );
        }

        internal void Authenticate( ChatUser chat_user ) {
            if( chat_user == null )
                throw new ArgumentNullException( "chat_user" );

            this._IsAuthenticated = true;
            this._ChatUser = chat_user;
        }

        public void Dispose() {
            ChitterEnvironment.CommunicationManager.CommunicationClientManager.DeregisterCommunicationClient( this );

            if( this._IsAuthenticated )
                _ChatUser.Dispose();

            _WebSocketConnection.Close();
        }

        internal IWebSocketConnection WebSocketConnection { get => _WebSocketConnection; }
        internal bool IsAuthenticated { get => _IsAuthenticated; }
        internal ChatUser ChatUser {
            get {
                if( !this._IsAuthenticated )
                    throw new ClientNotAuthenticatedException( this._WebSocketConnection.ConnectionInfo.ClientIpAddress );
                return _ChatUser;
            }
        }
    }
}
