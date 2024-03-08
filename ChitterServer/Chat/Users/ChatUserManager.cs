using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat.Users {
    internal class ChatUserManager {
        private List<ChatUser> _ChatUsers;

        internal ChatUserManager() {
            this._ChatUsers = new List<ChatUser>();
        }

        internal void RegisterChatUser( ChatUser chat_user ) {
            if( chat_user == null )
                throw new ArgumentNullException( "chat_user" );

            this._ChatUsers.Add( chat_user );
        }

        internal void DeregisterChatUser( ChatUser chat_user ) {
            if( chat_user == null )
                throw new ArgumentNullException( "chat_user" );

            this._ChatUsers.Remove( chat_user );
        }

        internal ChatUser GetChatUser( string uuid ) {
            if( String.IsNullOrWhiteSpace( uuid ) )
                throw new ArgumentNullException( "uuid" );

            throw new NotImplementedException();
        }
    }
}
