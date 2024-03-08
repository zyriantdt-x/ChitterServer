using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Handlers.Outgoing {
    internal class ShalomHandler : OutgoingMessage {
        internal ShalomHandler( string uuid ) 
            : base( "SHALOM" ) {
            AppendObject( "abc", "def" );
        }
    }
}
