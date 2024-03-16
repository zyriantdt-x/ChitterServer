using ChitterServer.Database.Adapters;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Utils.Settings {
    internal class SettingsManager {
        private static readonly ILog _Log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        private Dictionary<string, string> _Settings;

        internal SettingsManager() {
            this._Settings = new Dictionary<string, string>();

            this.RegisterSettings();

            _Log.Info( "SettingsManager -> INITALISED!" );
        }

        private void RegisterSettings() {
            DataTable settings_table;

            using( QueryReactor reactor = ChitterEnvironment.DatabaseManager.CreateQueryReactor() ) {
                reactor.Query = "SELECT * FROM `settings`";

                try {
                    settings_table = reactor.Table;
                } catch( NoDataException ) {
                    // non-issue (yet!)
                    return;
                } catch( Exception ex ) {
                    _Log.Error( $"Failed to initialise SettingsManager -> {ex.Message}" );
                    throw; // this is gamebreaking
                }
            }

            foreach(DataRow setting_row in settings_table.Rows) this.RegisterSetting( Convert.ToString( setting_row[ "key" ] ), Convert.ToString( setting_row[ "value" ] ) );
        }

        private void RegisterSetting( string key, string value ) {
            if( String.IsNullOrWhiteSpace( key ) ) throw new ArgumentNullException( "key" );

            if( String.IsNullOrWhiteSpace( value ) ) throw new ArgumentNullException( "value" );

            this._Settings.Add( key, value );
        }

        public string GetSetting( string key ) {
            if( String.IsNullOrWhiteSpace( key ) ) throw new ArgumentNullException( "key" );

            string value;

            return this._Settings.TryGetValue( key, out value ) ? value : throw new SettingNotFoundException( key );
        }
    }
}
