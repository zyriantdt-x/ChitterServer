using ChitterServer.Communication.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Handlers.Outgoing {
    internal partial class OutgoingMessage {
        internal MessageStructure Body;

        internal OutgoingMessage( string event_name ) {
            this.Body = new MessageStructure( event_name );
        }

        protected void AppendObject( string key, object val ) => this.Body.Body.Add( key, val );

        public override string ToString() => JsonConvert.SerializeObject( this.Body );
    }
}
