using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Database {
    internal class MalformedDataException : Exception {
        internal MalformedDataException(string col)
            : base($"Expected data was not found in data row/table: {col}") { }
    }
}
