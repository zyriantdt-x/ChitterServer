using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Handlers.Incoming {
    internal class MalformedPayloadException : Exception {
        internal string MissingData { get; }
        internal MalformedPayloadException() { }

        internal MalformedPayloadException(string missing_data) {
            this.MissingData = missing_data;
        }
    }
}
