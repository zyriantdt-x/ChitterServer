using ChitterServer.Chat.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Utils.ConsoleCommands {
    internal class ChannelsConsoleCommand : IConsoleCommand {
        public string Identifier => "channels";

        public void Handle( string[] argv ) {
            if( argv.Length > 1 )
                this.HandleChannel( argv[ 1 ] );
            else
                this.HandleAllChannels();
        }

        private void HandleAllChannels() {
            foreach( Channel channel in ChitterEnvironment.ChatManager.ChannelManager.Channels ) Console.WriteLine( $"{channel.Uuid} | {channel.DisplayName} | {channel.ActiveUsers.Count}" );
        }

        private void HandleChannel( string channel_uuid ) {
            Channel channel;
            try {
                channel = ChitterEnvironment.ChatManager.ChannelManager.GetChannel( channel_uuid );
            } catch (ChannelNotFoundException) {
                Console.WriteLine( $"Couldn't find channel with UUID: {channel_uuid}" );
                return;
            }

            Console.WriteLine( $"{channel.Uuid} | {channel.DisplayName} | {channel.ActiveUsers.Count}" );
            foreach(ChannelUser active_user in channel.ActiveUsers) Console.WriteLine( $"{active_user.ChatUser.Uuid} | {active_user.ChatUser.Username}" );
        }
    }
}
