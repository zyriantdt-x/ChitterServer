using ChitterServer.Chat.Channels;
using ChitterServer.Chat.Users;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat {
    internal class ChatManager {
        private static readonly ILog _Log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        private ChatUserManager _ChatUserManager;
        private ChannelManager _ChannelManager;

        internal ChatManager() {
            this._ChatUserManager = new ChatUserManager();
            this._ChannelManager = new ChannelManager();

            _Log.Info( "ChatManager -> INITIALISED!" );
        }

        internal ChatUserManager ChatUserManager { get => this._ChatUserManager; }
        internal ChannelManager ChannelManager { get => this._ChannelManager; }
    }
}
