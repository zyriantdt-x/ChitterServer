using ChitterServer.Communication.Clients;
using ChitterServer.Communication.Handlers.Outgoing;
using ChitterServer.Communication.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Handlers.Incoming {
    internal class IncomingChatMessageHandler : IIncomingMessageHandler {
        public string Identifier => "CHAT_MESSAGE"; // non-standard

        public void Handle( CommunicationClient communication_client, MessageStructure message ) {
            object message_text_as_obj;

            if( !message.Body.TryGetValue( "message", out message_text_as_obj ) )
                throw new MalformedPayloadException( "body.message" );

            string message_text = Convert.ToString( message_text_as_obj );

            // we'll eventually implement a chat command manager
            // but for now, let's broadcast the message to the channel
            // we will remove HTML stuff just in case!

            message_text = Regex.Replace( message_text, "<.*?>", string.Empty ); // i don't think anyone knows how to use regex

            communication_client.ChatUser.ActiveChannel.Broadcast( new ChatMessageHandler( DateTime.Now, communication_client.ChatUser.Username, message_text ) );
        }
    }
}
