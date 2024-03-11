using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat.Channels {
    internal enum ChannelPrivileges {
        BASIC = 0,
        MODERATOR = 1,
        ADMINISTRATOR = 2
    }
}
