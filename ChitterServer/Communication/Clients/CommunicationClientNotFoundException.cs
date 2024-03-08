using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Clients {
    internal class CommunicationClientNotFoundException : Exception {
        internal CommunicationClientNotFoundException( string search_type, string search_item ) 
            : base( $"CommunicationClient instance not found based on search parameters: {search_type} {search_item}" ) {}
    }
}
