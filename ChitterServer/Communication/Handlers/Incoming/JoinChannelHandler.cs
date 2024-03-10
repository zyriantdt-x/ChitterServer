using ChitterServer.Chat.Channels;
using ChitterServer.Communication.Clients;
using ChitterServer.Communication.Handlers.Outgoing;
using ChitterServer.Communication.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Handlers.Incoming {
    // in future this will likely be done by chat command
    // for now, this will remain in place for debug purposes
    internal class JoinChannelHandler : IIncomingMessageHandler {
        public string Identifier => "JOIN_CHANNEL";

        public void Handle( CommunicationClient communication_client, MessageStructure message ) {
            object channel_uuid_as_obj;

            if( !message.Body.TryGetValue( "uuid", out channel_uuid_as_obj ) )
                throw new MalformedPayloadException( "body.uuid" );

            string channel_uuid = Convert.ToString( channel_uuid_as_obj );

            Channel channel;

            try {
                channel = ChitterEnvironment.ChatManager.ChannelManager.GetChannel( channel_uuid );
            } catch( ChannelNotFoundException ) {
                communication_client.Send( new ChatMessageHandler( DateTime.Now, "SERVER", "Unable to find requested channel!" ) );
                return;
            }

            communication_client.ChatUser.JoinChannel( channel );
        }
    }
}
