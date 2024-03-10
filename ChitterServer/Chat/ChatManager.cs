using ChitterServer.Chat.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat {
    internal class ChatManager {
        private ChatUserManager _ChatUserManager;

        internal ChatManager() {
            this._ChatUserManager = new ChatUserManager();
        }

        internal ChatUserManager ChatUserManager { get => this._ChatUserManager; }
    }
}
