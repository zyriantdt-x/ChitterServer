﻿using ChitterServer.Database.Adapters;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitterServer.Chat.Channels {
    internal class ChannelManager {
        private static readonly ILog _Log = LogManager.GetLogger( System.Reflection.MethodBase.GetCurrentMethod().DeclaringType );

        private List<Channel> _Channels;

        internal ChannelManager() {
            // for now let's load all channels
            // in future, maybe only load active channels to save resources

            this._Channels = new List<Channel>();
            this.RegisterChannels();

            _Log.Info( $"ChannelManager ({this._Channels.Count} channels) -> INITIALISED!" );
        }

        private void RegisterChannels() {
            DataTable channels_table;

            using( QueryReactor reactor = ChitterEnvironment.DatabaseManager.CreateQueryReactor() ) {
                reactor.Query = "SELECT * FROM `channels`";

                try {
                    channels_table = reactor.Table;
                } catch( NoDataException ) {
                    // this isn't gamebreaking - just means there's no channels...
                    // maybe this is in the future?
                    return;
                } catch( Exception ex ) {
                    _Log.Error( $"Unable to load channels from database -> {ex.Message}", ex );
                    return; 
                }
            }

            // register each channel
            foreach( DataRow channel_row in channels_table.Rows ) this.RegisterChannel(new Channel( channel_row ));
        }

        internal void RegisterChannel( Channel channel ) {
            if( channel == null ) throw new ArgumentNullException( "channel" );

            this._Channels.Add( channel );
        }
        
        internal Channel GetChannel( string uuid ) {
            if( uuid == null ) throw new ArgumentNullException( "uuid" );

            Channel channel = this._Channels.FirstOrDefault( x => x.Uuid == uuid );

            return channel ?? throw new ChannelNotFoundException( uuid );
        }

        internal static ILog Log => _Log;

        internal List<Channel> Channels => this._Channels;
    }
}
