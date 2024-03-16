using ChitterServer.Chat.Users;
using ChitterServer.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat.Channels {
    internal class ChannelUser : IDisposable {
        private ChatUser _ChatUser;

        private Channel _Channel;

        private ChannelPrivileges _ChannelPrivileges;

        internal ChannelUser( ChatUser chat_user, Channel channel, DataRow channel_user_data ) {
            this._ChatUser = chat_user ?? throw new ArgumentNullException( "chat_user" );
            this._Channel = channel ?? throw new ArgumentNullException( "channel" );

            if( channel_user_data == null ) throw new ArgumentNullException( "channel_user_data" );

            if( channel_user_data[ "privilege_level" ] == null )
                throw new MalformedDataException( "privilege_level" );
            this._ChannelPrivileges = ( ChannelPrivileges )Convert.ToInt32( channel_user_data[ "privilege_level" ] ); // we should probably find a way of testing if this is out of bounds. apparently we can use 'as' here either...

            this._Channel.RegisterChannelUser( this );
        }

        public void Dispose() {
#pragma warning disable IDE0022 // Use expression body for methods
            this._Channel.DeregisterChannelUser( this );
#pragma warning restore IDE0022 // Use expression body for methods
        }

        internal ChatUser ChatUser => this._ChatUser;
        internal ChannelPrivileges ChannelPrivileges => this._ChannelPrivileges;
    }
}
