using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Database {
    internal class MalformedDataException : Exception {
        internal string Column { get; }
        internal MalformedDataException(string col) {
            this.Column = col;
        }
    }
}
