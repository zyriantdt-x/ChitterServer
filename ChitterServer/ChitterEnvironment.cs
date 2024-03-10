using ChitterServer.Chat;
using ChitterServer.Communication;
using ChitterServer.Database;
using ChitterServer.Database.Adapters;
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

        internal static void Initialise() {
            ConsoleColor console_current_colour = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            //Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine( @"  _______ _   _______      _______         " );
            Console.WriteLine( @" |__   __(_) |__   __|    |__   __|        " );
            Console.WriteLine( @"    | |   _  ___| | __ _  ___| | ___   ___ " );
            Console.WriteLine( @"    | |  | |/ __| |/ _` |/ __| |/ _ \ / _ \" );
            Console.WriteLine( @"    | |  | | (__| | (_| | (__| | (_) |  __/" );
            Console.WriteLine( @"    |_|  |_|\___|_|\__,_|\___|_|\___/ \___|" );
            Console.WriteLine( "\n > Chitter Server by Ellis <\n\n" );
            Console.ForegroundColor = console_current_colour;

            _CommunicationManager = new CommunicationManager( "0.0.0.0", 1232 );
            _DatabaseManager = new DatabaseManager( "./chitter.db" );
            _ChatManager = new ChatManager();

            using( QueryReactor reactor = _DatabaseManager.CreateQueryReactor() ) {
                reactor.Query = "SELECT 1+1 AS `value`";
                Console.WriteLine( reactor.Row[ "value" ] );
            }

            _Log.Info( "TicTacToe Server has initialised successfully!\n" );
        }

        internal static CommunicationManager CommunicationManager { get => _CommunicationManager; }
        internal static DatabaseManager DatabaseManager { get => _DatabaseManager; }
        internal static ChatManager ChatManager { get => _ChatManager; }
    }
}
