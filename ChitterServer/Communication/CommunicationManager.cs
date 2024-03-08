using ChitterServer.Communication.Clients;
using ChitterServer.Communication.Handlers.Outgoing;
using Fleck;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication {
    internal class CommunicationManager {
        private static readonly ILog _Log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        private IWebSocketServer _WebSocketServer;
        private CommunicationClientManager _CommunicationClientManager;

        internal CommunicationManager( string host, uint port ) {
            _WebSocketServer = new WebSocketServer( $"ws://{host}:{port}" ); // TODO: let's parse this better...

            // configure fleck to use log4net
            FleckLog.LogAction = ( level, message, ex ) => {
                switch( level ) {
                    case LogLevel.Debug:
                        //_Log.Debug( message, ex );
                        break;
                    case LogLevel.Error:
                        _Log.Error( message, ex );
                        break;
                    case LogLevel.Warn:
                        //_Log.Warn( message, ex );
                        break;
                    default:
                        //_Log.Info( message, ex );
                        break;
                }
            };

            FleckLog.Level = LogLevel.Error;

            _CommunicationClientManager = new CommunicationClientManager();

            _WebSocketServer.Start( socket => {
                socket.OnOpen = () => {
                    _Log.Info( $"Connection created -> {socket.ConnectionInfo.ClientIpAddress}" );

                    CommunicationClient communication_client = new CommunicationClient( socket );
                    _CommunicationClientManager.RegisterCommunicationClient( communication_client );
                    communication_client.Send( new ShalomHandler("nig") );
                };

                socket.OnClose = () => {

                };

                socket.OnMessage = ( message ) => {

                };

                socket.OnError = ( ex ) => {

                };
            } );
        }

        internal static void LogOutboundMessage(string identifier, string json_msg) { 
            if( String.IsNullOrWhiteSpace( identifier ) )
                throw new ArgumentNullException( "identifier" );

            if( String.IsNullOrWhiteSpace( json_msg ) )
                throw new ArgumentNullException( "json_msg" );

            _Log.Info( $"Sent message to {identifier} -> {json_msg}" );
        }
    }
}
