using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Clients {
    internal class CommunicationClientNotFoundException : Exception {
        internal string SearchType { get; }
        internal string SearchItem { get; }
        internal CommunicationClientNotFoundException( string search_type, string search_item ) {
            this.SearchType = search_type;
            this.SearchItem = search_item;
        }
    }
}
