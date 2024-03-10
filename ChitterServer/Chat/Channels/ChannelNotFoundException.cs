using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat.Channels {
    internal class ChannelNotFoundException : Exception {
        internal ChannelNotFoundException( string uuid )
            : base( $"Attempt to find non-existing channel with UUID {uuid}" ) { }
    }
}
