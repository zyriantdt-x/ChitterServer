using ChitterServer.Chat.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Utils.ConsoleCommands {
    internal class ChatUsersConsoleCommand : IConsoleCommand {
        public string Identifier => "chatusers";

        public void Handle( string[] argv ) {
            foreach( ChatUser chat_user in ChitterEnvironment.ChatManager.ChatUserManager.ChatUsers )  Console.WriteLine( $"{chat_user.Uuid} | {chat_user.Username} | {chat_user.ActiveChannel.DisplayName} ({chat_user.ActiveChannel.Uuid})" );
        }
    }
}
