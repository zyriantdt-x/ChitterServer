using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Database.Adapters {
    internal class QueryReactor : IDisposable {
        private DatabaseClient _DatabaseClient;
        private SqliteCommand _SQLiteCommand;

        internal QueryReactor(DatabaseClient database_client) {
            if( database_client == null )
                throw new ArgumentNullException( "database_client" );

            this._DatabaseClient = database_client;

            this._SQLiteCommand = this._DatabaseClient.CreateSqliteCommand();
        }

        internal void AddParameter(string key, object value) {
            if( String.IsNullOrWhiteSpace( key ) )
                throw new ArgumentNullException( "key" );

            if( value == null )
                throw new ArgumentNullException( "value" );

            SqliteParameter param_added = this._SQLiteCommand.Parameters.AddWithValue( key, value );

            if( param_added == null )
                throw new Exception( $"Unable to add SQLite parameter {key}" );
        }

        internal bool HasResults {
            get {
                bool has_results;

                try {
                    using( SqliteDataReader reader = this._SQLiteCommand.ExecuteReader() ) {
                        has_results = reader.HasRows;
                    }
                } catch( Exception ex ) {
                    DatabaseManager.Log.Error( $"Unable to execute command to find results -> {ex.Message}" );

                    this.Dispose();

                    throw;
                }

                return has_results;
            }
        }

        internal DataRow Row {
            get {
                DataRow row;

                try {
                    DataTable data_table = new DataTable();
                    using( SqliteDataReader reader = this._SQLiteCommand.ExecuteReader() ) {
                        data_table.Load( reader );
                    }

                    if( data_table.Rows.Count > 0 )
                        row = data_table.Rows[ 0 ];
                    else
                        throw new NoDataException( this._SQLiteCommand.CommandText );
                } catch (Exception ex) {
                    DatabaseManager.Log.Error( $"Unable to get row -> {ex.Message}" );

                    this.Dispose();

                    throw;
                }

                return row;
            }
        }

        internal DataTable Table {
            get {
                DataTable table = new DataTable();

                try {
                    using( SqliteDataReader reader = this._SQLiteCommand.ExecuteReader() ) {
                        table.Load( reader );
                    }

                    if( table.Rows.Count < 1 )
                        throw new NoDataException( this._SQLiteCommand.CommandText );
                } catch (Exception ex) {
                    DatabaseManager.Log.Error( $"Unable to get table -> {ex.Message}" );

                    this.Dispose();

                    throw;
                }

                return table;
            }
        }

        internal string Query {
            get => this._SQLiteCommand.CommandText;

            set {
                this._SQLiteCommand.Parameters.Clear();
                this._SQLiteCommand.CommandText = value;
            }
        }

        internal void RunQuery() {
            try {
                this._SQLiteCommand.ExecuteNonQuery();
            } catch (Exception ex) {
                DatabaseManager.Log.Error( $"Unable to run query -> {ex.Message}" );

                this.Dispose();

                throw;
            }
        }

        public void Dispose() {
            this._SQLiteCommand.Dispose();
            this._DatabaseClient.Dispose();
        }
    }
}
