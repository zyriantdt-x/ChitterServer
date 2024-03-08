using ChitterServer.Communication;
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

        internal static void Initialise() {
            ConsoleColor console_current_colour = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.White;
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

            _Log.Info( "TicTacToe Server has initialised successfully!\n" );
        }

        internal static CommunicationManager CommunicationManager {
            get => _CommunicationManager;
        }
    }
}
