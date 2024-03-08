using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Database {
    internal class DatabaseManager {
        private readonly string _File_Name;

        internal DatabaseManager( string file_name ) {
            this._File_Name = file_name;
        }
    }
}
