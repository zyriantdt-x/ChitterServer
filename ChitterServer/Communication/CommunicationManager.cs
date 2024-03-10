using ChitterServer.Communication.Clients;
using ChitterServer.Communication.Handlers.Incoming;
using ChitterServer.Communication.Handlers.Outgoing;
using ChitterServer.Communication.Utils;
using Fleck;
using log4net;
using Newtonsoft.Json;
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

        private IncomingMessageManager _IncomingMessageMAnager;

        internal CommunicationManager( string host, uint port ) {
            this._WebSocketServer = new WebSocketServer( $"ws://{host}:{port}" ); // TODO: let's parse this better...

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

            this._CommunicationClientManager = new CommunicationClientManager();

            this._IncomingMessageMAnager = new IncomingMessageManager();

            this._WebSocketServer.Start( socket => {
                socket.OnOpen = () => {
                    _Log.Info( $"Connection opened -> {socket.ConnectionInfo.ClientIpAddress}" );

                    CommunicationClient communication_client = new CommunicationClient( socket );

                    communication_client.Send( new ShalomHandler("nig") );
                };

                socket.OnClose = () => {
                    try {
                        _Log.Info( $"Connection closed -> {socket.ConnectionInfo.ClientIpAddress}" );
                        CommunicationClient communication_client = _CommunicationClientManager.GetCommunicationClient( socket );
                        communication_client.Dispose();
                    } catch ( Exception ex ) {
                        _Log.Warn( $"Exception thrown whilst trying to close socket -> {ex.Message}" );
                        // i don't actually think this matters in most cases.
                    }
                };

                socket.OnMessage = ( message ) => {
                    CommunicationClient communication_client;
                    try {
                        communication_client = _CommunicationClientManager.GetCommunicationClient( socket );
                    } catch (CommunicationClientNotFoundException ex) {
                        _Log.Error( $"Failed to handle incoming message -> {ex.Message}" );

                        socket.Close();
                        return;
                    }

                    try {
                        string display_name = communication_client.IsAuthenticated ? communication_client.ChatUser.Username : communication_client.WebSocketConnection.ConnectionInfo.ClientIpAddress;
                        _Log.Info( $"Message received from {display_name} -> {message}" );

                        MessageStructure payload = JsonConvert.DeserializeObject<MessageStructure>( message );
                        if( payload == null )
                            throw new MalformedPayloadException();

                        if( String.IsNullOrWhiteSpace( payload.Message ) )
                            throw new MalformedPayloadException( "message" );

                        if( !communication_client.IsAuthenticated && ( payload.Message != "REQUEST_AUTHENTICATE" ) )
                            throw new ClientNotAuthenticatedException( communication_client.WebSocketConnection.ConnectionInfo.ClientIpAddress, payload.Message );

                        IIncomingMessageHandler message_handler = this._IncomingMessageMAnager.GetMessageHandler( payload.Message );

                        message_handler.Handle( communication_client, payload );
                    } catch ( Exception ex ) {
                        _Log.Error( $"Failed to handle incoming message -> {ex.Message}" );

                        communication_client.Dispose();
                    }
                };

                socket.OnError = ( ex ) => {

                };
            } );
        }

        internal static void LogOutboundMessage(string display_name, string json_msg) { 
            if( String.IsNullOrWhiteSpace( display_name ) )
                throw new ArgumentNullException( "display_name" );

            if( String.IsNullOrWhiteSpace( json_msg ) )
                throw new ArgumentNullException( "json_msg" );

            _Log.Info( $"Sent message to {display_name} -> {json_msg}" );
        }

        internal CommunicationClientManager CommunicationClientManager { get => this._CommunicationClientManager; }
    }
}
