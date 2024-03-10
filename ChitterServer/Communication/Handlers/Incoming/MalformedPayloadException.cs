using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Handlers.Incoming {
    internal class MalformedPayloadException : Exception {
        internal MalformedPayloadException()
            : base("Expected data was not provided in payload.") { }

        internal MalformedPayloadException(string missing_data)
            : base( $"Expected data was not provided in payload: {missing_data}" ) { }
    }
}
