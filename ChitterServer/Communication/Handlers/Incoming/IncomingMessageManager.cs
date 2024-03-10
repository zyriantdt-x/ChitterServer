using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Communication.Handlers.Incoming {
    internal class IncomingMessageManager {
        private static readonly ILog _Log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        private List<IIncomingMessageHandler> _IncomingMessageHandlers;

        internal IncomingMessageManager() {
            this._IncomingMessageHandlers = new List<IIncomingMessageHandler>();
            this.RegisterMessageHandlers();

            _Log.Info( $"IncomingMessageManager ({this._IncomingMessageHandlers.Count} messages) -> INITIALISED!" );
        }

        private void RegisterMessageHandlers() {
            this.RegisterMessageHandler( new RequestAuthenticateHandler() );
            this.RegisterMessageHandler( new JoinChannelHandler() );
        }

        private void RegisterMessageHandler( IIncomingMessageHandler incoming_message_handler ) {
            if( incoming_message_handler == null )
                throw new ArgumentNullException( "incoming_message_handler" );

            this._IncomingMessageHandlers.Add( incoming_message_handler );
        }

        internal IIncomingMessageHandler GetMessageHandler( string identifier ) {
            if( String.IsNullOrWhiteSpace( identifier ) )
                throw new ArgumentNullException( "identifier" );

            IIncomingMessageHandler handler = this._IncomingMessageHandlers.FirstOrDefault( x => x.Identifier == identifier );

            if( handler == null )
                throw new IncomingEventNotFoundException( identifier );

            return handler;
        }
    }
}
