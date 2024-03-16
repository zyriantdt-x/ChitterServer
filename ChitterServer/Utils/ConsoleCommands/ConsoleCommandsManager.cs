using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Utils.ConsoleCommands {
    internal class ConsoleCommandsManager {
        private static readonly ILog _Log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        private List<IConsoleCommand> _ConsoleCommands;

        internal ConsoleCommandsManager() {
            this._ConsoleCommands = new List<IConsoleCommand>();
            this.RegisterConsoleCommands();

            _Log.Info( $"ConsoleCommandsManager ({this._ConsoleCommands.Count} commands) -> INITIALISED!" );
        }

        private void RegisterConsoleCommands() {
            this.RegisterConsoleCommand( new ChatUsersConsoleCommand() );
            this.RegisterConsoleCommand( new ChannelsConsoleCommand() );
        }

        private void RegisterConsoleCommand( IConsoleCommand console_command ) {
            if( console_command == null ) throw new ArgumentNullException( "console_command" );

            this._ConsoleCommands.Add( console_command );
        }

        private IConsoleCommand GetConsoleCommand( string identifier ) {
            if( String.IsNullOrWhiteSpace( identifier ) ) throw new ArgumentNullException( "identifier" );

            IConsoleCommand command = this._ConsoleCommands.FirstOrDefault( x => x.Identifier == identifier );

            return command ?? throw new ConsoleCommandNotFoundException( identifier );
        }

        internal void HandleConsoleInput( string input ) {
            if( String.IsNullOrWhiteSpace( input ) ) return; // we don't need to do anything with this... or even throw an exception which is nice!

            string[] split_string = input.Split( ' ' ); // split_string[ 0 ] is the identifier

            IConsoleCommand command;
            try {
                command = this.GetConsoleCommand( split_string[ 0 ] );
            } catch( ConsoleCommandNotFoundException ex ) {
                _Log.Error( $"Unable to handle console input -> {ex.Message}" );
                // this doesn't need to be handled any further
                return;
            }

            command.Handle( split_string );
        }
    }
}
