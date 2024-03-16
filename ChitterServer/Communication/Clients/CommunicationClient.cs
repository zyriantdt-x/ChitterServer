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
            _ = this._WebSocketConnection.Send( serialised_message );

            string display_name = this._IsAuthenticated ? this._ChatUser.Username : this._WebSocketConnection.ConnectionInfo.ClientIpAddress;
            CommunicationManager.LogOutboundMessage( display_name, serialised_message );
        }

        internal void Authenticate( ChatUser chat_user ) {
            this._ChatUser = chat_user ?? throw new ArgumentNullException( "chat_user" );
            this._IsAuthenticated = true;
        }

        public void Dispose() {
            ChitterEnvironment.CommunicationManager.CommunicationClientManager.DeregisterCommunicationClient( this );

            if( this._IsAuthenticated )
                this._ChatUser.Dispose();

            this._WebSocketConnection.Close();
        }

        internal IWebSocketConnection WebSocketConnection => this._WebSocketConnection;
        internal bool IsAuthenticated => this._IsAuthenticated;
        internal ChatUser ChatUser => this._IsAuthenticated ? this._ChatUser : throw new ClientNotAuthenticatedException( this._WebSocketConnection.ConnectionInfo.ClientIpAddress );
    }
}
