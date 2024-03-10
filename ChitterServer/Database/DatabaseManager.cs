using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Database {
    internal class DatabaseManager {
        private static readonly ILog _Log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        private readonly string _File_Name;

        internal DatabaseManager( string file_name ) {
            this._File_Name = file_name;
        }

        internal static ILog Log { get => _Log; }
    }
}
