using ChitterServer.Database.Adapters;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Database {
    internal class DatabaseManager {
        private const string DB_NAME = "./chitter.db";

        private static readonly ILog _Log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        private readonly string _FileName;

        internal DatabaseManager() {
            this._FileName = DB_NAME;

            _Log.Info( "DatabaseManager -> INITIALISED!" );
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

        internal static ILog Log => _Log;
    }
}
