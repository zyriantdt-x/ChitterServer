using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat.Commands {
    internal class ChatCommandsManager {
        private static readonly ILog _Log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        private List<IChatCommand> _ChatCommands;

        internal ChatCommandsManager() {
            this._ChatCommands = new List<IChatCommand>();

            this.RegisterChatCommands();

            _Log.Info( "ChatCommandsManager ( commands) -> INITIALISED!" );
        }

        private void RegisterChatCommands() {

        }

        private void RegisterChatCommand( IChatCommand chat_command ) {
            if( chat_command == null ) throw new ArgumentNullException( "chat_command" );

            this._ChatCommands.Add( chat_command );
        }
    }
}
