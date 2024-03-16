using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat.Channels {
    internal class ChannelUserNotFoundException : Exception {
        internal string UserUuid { get; }
        internal string ChannelUuid { get; }

        internal ChannelUserNotFoundException( string user_uuid, string channel_uuid ) {
            this.UserUuid = user_uuid;
            this.ChannelUuid = channel_uuid;
        }
    }
}
