﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.Sqlite;
using ChitterServer.Database.Adapters;

namespace ChitterServer.Database {
    internal class DatabaseClient : IDisposable {
        private readonly SqliteConnection _Connection;
        private readonly QueryReactor _QueryReactor;

        internal DatabaseClient( string file_name ) {
            SqliteConnectionStringBuilder sqlite_string = new SqliteConnectionStringBuilder();
            sqlite_string.DataSource = file_name;

            this._Connection = new SqliteConnection( sqlite_string.ToString() );
            this._QueryReactor = new QueryReactor( this );
        }

        internal void Connect() {
            if( this._Connection.State == ConnectionState.Closed ) {
                try {
                    this._Connection.Open();
                } catch( Exception ex ) {
                    DatabaseManager.Log.Error( $"Exception when attempting to connect to SQLite Database -> {ex.Message}" );

                    this.Dispose();
                }
            }
        }

        internal void Disconnect() {
            if( this._Connection.State == ConnectionState.Open )
                this._Connection.Close();
        }

        internal SqliteCommand CreateSqliteCommand() {
            return this._Connection.CreateCommand();
        }

        internal QueryReactor QueryReactor {
            get => this._QueryReactor;
        }

        public void Dispose() {
            if( this._Connection.State == ConnectionState.Open )
                this._Connection.Close();

            this._Connection.Dispose();

            GC.SuppressFinalize( this );
        }
    }
}