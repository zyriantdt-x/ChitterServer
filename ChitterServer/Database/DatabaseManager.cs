using ChitterServer.Database.Adapters;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Database {
    internal class DatabaseManager {
        private static readonly ILog _Log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        private readonly string _FileName;

        internal DatabaseManager( string file_name ) {
            this._FileName = file_name;
        }

        internal QueryReactor CreateQueryReactor() {
            try {
                DatabaseClient db_client = new DatabaseClient( this._FileName );

                db_client.Connect();

                return db_client.QueryReactor;
            } catch (Exception ex ) {
                _Log.Error( $"Unable to create query reactor -> {ex.Message}" );

                // is this game breaking?? maybe we should crash!

                throw;
            }
        }

        internal static ILog Log { get => _Log; }
    }
}
