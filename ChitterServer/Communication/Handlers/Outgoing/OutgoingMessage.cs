using ChitterServer.Communication.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Handlers.Outgoing {
    internal partial class OutgoingMessage {
        public MessageStructure Body;

        public OutgoingMessage( string event_name ) {
            Body = new MessageStructure( event_name );
        }

        protected void AppendObject( string key, object val ) {
            Body.Body.Add( key, val );
        }

        public override string ToString() {
            return JsonConvert.SerializeObject( this.Body );
        }
    }
}
