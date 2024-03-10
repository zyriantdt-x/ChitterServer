using ChitterServer.Chat;
using ChitterServer.Communication;
using ChitterServer.Database;
using ChitterServer.Database.Adapters;
using ChitterServer.Utils.Settings;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer {
    internal class ChitterEnvironment {
        private static readonly ILog _Log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        private static CommunicationManager _CommunicationManager;
        private static DatabaseManager _DatabaseManager;
        private static ChatManager _ChatManager;
        private static SettingsManager _SettingsManager;

        internal static void Initialise() {
            ConsoleColor console_current_colour = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;

            string[] title = {
                @"   ___ _    _ _   _           ",
                @"  / __| |_ (_) |_| |_ ___ _ _ ",
                @" | (__| ' \| |  _|  _/ -_) '_|",
                @"  \___|_||_|_|\__|\__\___|_|  ",
                @"",
                @"      > Chitter Server <      ",
                @""
            };

            foreach(string line in title) {
                Console.WriteLine( line );
            }

            Console.ForegroundColor = console_current_colour;

            _DatabaseManager = new DatabaseManager();
            _SettingsManager = new SettingsManager();
            _CommunicationManager = new CommunicationManager();
            _ChatManager = new ChatManager();

            _Log.Info( "Chitter Server has initialised successfully!\n" );
        }

        internal static CommunicationManager CommunicationManager { get => _CommunicationManager; }
        internal static DatabaseManager DatabaseManager { get => _DatabaseManager; }
        internal static ChatManager ChatManager { get => _ChatManager; }
        internal static SettingsManager SettingsManager { get => _SettingsManager; }
    }
}
