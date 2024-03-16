using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat.Channels {
    internal class ChannelNotFoundException : Exception {
        string Uuid { get; }

        internal ChannelNotFoundException( string uuid ) {
            this.Uuid = uuid;
        }
    }
}
