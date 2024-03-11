using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat.Users {
    internal class ChatUserManager {
        private static readonly ILog _Log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        private List<ChatUser> _ChatUsers;

        internal ChatUserManager() {
            this._ChatUsers = new List<ChatUser>();

            _Log.Info( "ChatUserManager -> INITIALISED!" );
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

        internal List<ChatUser> ChatUsers { get => this._ChatUsers; } // i really don't like this, but i need it for console commands

        internal static ILog Log { get => _Log; }
    }
}
