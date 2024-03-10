using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Database.Adapters {
    internal class NoDataException : Exception {
        internal NoDataException(string query)
            : base($"Attempt to access data when query returned no results: {query}") { }
    }
}
