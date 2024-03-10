using Fleck;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Clients {
    internal class CommunicationClientManager {
        private static readonly ILog _Log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        private List<CommunicationClient> _CommunicationClients;

        internal CommunicationClientManager() {
            this._CommunicationClients = new List<CommunicationClient>();

            _Log.Info( "CommunicationClientManager -> INITIALISED!" );
        }

        public CommunicationClient GetCommunicationClient( string uuid ) {
            if( uuid == null )
                throw new ArgumentNullException( "uuid" );

            CommunicationClient communication_client = this._CommunicationClients.FirstOrDefault( x => x.ChatUser.Uuid == uuid );
            if( communication_client == null )
                throw new CommunicationClientNotFoundException( "uuid", uuid );

            return communication_client;
        }

        public CommunicationClient GetCommunicationClient( IWebSocketConnection web_socket_connection ) {
            if( web_socket_connection == null )
                throw new ArgumentNullException( "web_socket_connection" );

            CommunicationClient communication_client = this._CommunicationClients.FirstOrDefault( x => x.WebSocketConnection == web_socket_connection );
            if( communication_client == null )
                throw new CommunicationClientNotFoundException( "iwebsocketconnection", web_socket_connection.ConnectionInfo.ClientIpAddress );

            return communication_client;
        }

        public void RegisterCommunicationClient( CommunicationClient communication_client ) {
            if( communication_client == null )
                throw new ArgumentNullException( "communication_client" );

            this._CommunicationClients.Add( communication_client );
        }

        public void DeregisterCommunicationClient( CommunicationClient communication_client ) {
            if( communication_client == null )
                throw new ArgumentNullException( "communication_client" );

            this._CommunicationClients.Remove( communication_client );
        }

        public void DeregisterCommunicationClient( IWebSocketConnection web_socket_connection ) {
            if( web_socket_connection == null )
                throw new ArgumentNullException( "web_socket_connection" );

            CommunicationClient communication_client = this.GetCommunicationClient( web_socket_connection );

            DeregisterCommunicationClient( communication_client );
        }
    }
}
