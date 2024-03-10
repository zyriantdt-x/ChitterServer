using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Utils {
    internal class MessageStructure {
        [JsonProperty( "message" )]
        internal string Message { get; set;  }

        [JsonProperty( "body" )]
        internal Dictionary<string, object> Body { get; set; }

        internal MessageStructure( string Event ) {
            this.Message = Event;
            this.Body = new Dictionary<string, object>();
        }

        [JsonConstructor]
        internal MessageStructure() { }
    }
}
