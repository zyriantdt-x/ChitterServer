using ChitterServer.Chat.Users;
using ChitterServer.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat.Channels {
    internal class ChannelUser {
        private ChatUser _ChatUser;

        private ChannelPrivileges _ChannelPrivileges;

        internal ChannelUser( ChatUser chat_user, DataRow channel_user_data ) {
            if( chat_user == null )
                throw new ArgumentNullException( "chat_user" );
            this._ChatUser = chat_user;

            if( channel_user_data == null )
                throw new ArgumentNullException( "channel_user_data" );

            if( channel_user_data[ "privilege_level" ] == null )
                throw new MalformedDataException( "privilege_level" );
            this._ChannelPrivileges = (ChannelPrivileges)Convert.ToInt32( channel_user_data[ "privilege_level" ] ); // we should probably find a way of testing if this is out of bounds
            
        }

        internal ChatUser ChatUser { get => this._ChatUser; }
        internal ChannelPrivileges ChannelPrivileges { get => this._ChannelPrivileges; }
    }
}
