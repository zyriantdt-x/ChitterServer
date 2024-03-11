using ChitterServer.Chat.Channels;
using ChitterServer.Communication.Clients;
using ChitterServer.Communication.Handlers.Outgoing;
using ChitterServer.Database;
using ChitterServer.Database.Adapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat.Users {
    internal class ChatUser : IDisposable {
        private string _Uuid;
        private string _Username;

        private CommunicationClient _CommunicationClient;

        private Channel _ActiveChannel;

        internal ChatUser( CommunicationClient communcation_client, DataRow user_data ) {
            if( communcation_client == null )
                throw new ArgumentNullException( "communication_client" );
            this._CommunicationClient = communcation_client;

            if( user_data == null )
                throw new ArgumentNullException( "user_data" );

            if( user_data[ "uuid" ] == null )
                throw new MalformedDataException( "uuid" );
            this._Uuid = Convert.ToString( user_data[ "uuid" ] );

            if( user_data[ "username" ] == null )
                throw new MalformedDataException( "username" );
            this._Username = Convert.ToString( user_data[ "username" ] );

            // set active channel to default

            ChitterEnvironment.ChatManager.ChatUserManager.RegisterChatUser( this );
        }

        internal static ChatUser TryAuthenticate( CommunicationClient communication_client, string username, string password ) {
            if( String.IsNullOrWhiteSpace( username ) )
                throw new ArgumentNullException( username );

            if( String.IsNullOrWhiteSpace( password ) )
                throw new ArgumentNullException( password );

            DataRow user_data;

            using( QueryReactor reactor = ChitterEnvironment.DatabaseManager.CreateQueryReactor() ) {
                reactor.Query = "SELECT * FROM `users` WHERE `username` = @username AND `password` = @password";
                reactor.AddParameter( "username", username );
                reactor.AddParameter( "password", password );

                try {
                    user_data = reactor.Row;
                } catch( NoDataException ) {
                    throw new UserNotFoundException( username );
                } catch( Exception ex ) {
                    ChatUserManager.Log.Error( $"Unable to authenticate user -> {ex.Message}" );

                    throw;
                }
            }

            return new ChatUser( communication_client, user_data );
        }

        internal void JoinChannel( Channel channel ) {
            if( channel == null )
                throw new ArgumentNullException( "channel" );

            this._ActiveChannel.Leave( this );

            channel.Join( this );
            this._ActiveChannel = channel;

            this._CommunicationClient.Send( new ChatMessageHandler( DateTime.Now, "SERVER", $"Welcome to {channel.DisplayName}!" ) );
        }

        public void Dispose() {
            this._ActiveChannel.Leave( this );
            ChitterEnvironment.ChatManager.ChatUserManager.DeregisterChatUser( this );
        }

        internal string Uuid { get => this._Uuid; }
        internal string Username { get => this._Username; }

        internal CommunicationClient CommunicationClient { get => this._CommunicationClient; }

        internal Channel ActiveChannel { get => this._ActiveChannel; }
    }
}
