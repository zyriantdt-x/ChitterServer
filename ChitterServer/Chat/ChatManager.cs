using ChitterServer.Chat.Channels;
using ChitterServer.Chat.Commands;
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
        private ChatCommandsManager _ChatCommandsManager;

        internal ChatManager() {
            this._ChatUserManager = new ChatUserManager();
            this._ChannelManager = new ChannelManager();
            this._ChatCommandsManager = new ChatCommandsManager();

            _Log.Info( "ChatManager -> INITIALISED!" );
        }

        internal ChatUserManager ChatUserManager => this._ChatUserManager;
        internal ChannelManager ChannelManager => this._ChannelManager;
        internal ChatCommandsManager ChatCommandsManager => this._ChatCommandsManager;
    }
}
