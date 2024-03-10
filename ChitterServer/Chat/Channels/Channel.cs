using ChitterServer.Chat.Users;
using ChitterServer.Database;
using ChitterServer.Database.Adapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat.Channels {
    internal class Channel {
        private string _Uuid;
        private string _DisplayName;

        private List<ChannelUser> _ActiveUsers;

        internal Channel( DataRow channel_data ) {
            if( channel_data == null )
                throw new ArgumentNullException( "channel_data" );

            this._ActiveUsers = new List<ChannelUser>();

            if( channel_data[ "uuid" ] == null )
                throw new MalformedDataException( "uuid" );
            this._Uuid = Convert.ToString( channel_data[ "uuid" ] );

            if( channel_data[ "displayname" ] == null )
                throw new MalformedDataException( "displayname" );
            this._DisplayName = Convert.ToString( channel_data[ "displayname" ] );
        }

        private void CreateChannelUser( ChatUser chat_user ) {
            if( chat_user == null )
                throw new ArgumentException( "chat_user" );
        }

        internal ChannelUser GetChannelUser( ChatUser chat_user ) {
            if( chat_user == null )
                throw new ArgumentException( "chat_user" );

            ChannelUser channel_user = this._ActiveUsers.FirstOrDefault( x => x.ChatUser.Uuid == chat_user.Uuid );
            if( channel_user != null )
                return channel_user;

            DataRow channel_user_row;

            using( QueryReactor reactor = ChitterEnvironment.DatabaseManager.CreateQueryReactor() ) {
                reactor.Query = "SELECT * FROM `channel_users` WHERE `channel_uuid` = @channel_uuid AND `user_uuid` = @user_uuid";
                reactor.AddParameter( "channel_uuid", this._Uuid );
                reactor.AddParameter( "user_uuid", chat_user.Uuid );

                try {
                    channel_user_row = reactor.Row;
                } catch( NoDataException ) {
                    throw new ChannelUserNotFoundException();
                } catch( Exception ex ) {
                    ChannelManager.Log.Warn( $"Failed to load channel_users data -> {ex.Message}" );
                    throw; // let's expect this to be handled upstream
                }
            }

            return new ChannelUser( chat_user, channel_user_row );
        }

        private void RegisterChannelUser( ChannelUser channel_user ) {
            if( channel_user == null )
                throw new ArgumentNullException( "channel_user" );

            this._ActiveUsers.Add( channel_user );
        }

        internal void Join( ChatUser chat_user ) {
            if( chat_user == null )
                throw new ArgumentException( "chat_user" );

            // get channel user
            ChannelUser channel_user;
            try {
                channel_user = this.GetChannelUser( chat_user );
            } catch( ChannelUserNotFoundException ) {
                this.CreateChannelUser( chat_user );
                channel_user = this.GetChannelUser( chat_user ); // let's just hope this doesn't throw!
            } catch( Exception ex ) {
                ChannelManager.Log.Warn( $"Failed to join channel -> {ex.Message}" );
                throw; // let's expect this to be handled upstream
            }

            this.RegisterChannelUser( channel_user );
        }
    }
}
