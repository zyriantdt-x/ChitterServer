using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Database.Adapters {
    internal class NoDataException : Exception {
        internal string Query { get; }
        internal NoDataException(string query) {
            this.Query = query;
        }
    }
}
