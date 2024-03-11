using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChitterServer {
    class Program {
        static void Main( string[] args ) {
            // configure log4net
            XmlDocument logging_config = new XmlDocument();
            logging_config.Load( File.OpenRead( "log4net.config" ) );
            log4net.Config.XmlConfigurator.Configure( logging_config[ "log4net" ] );

            // initialise the environment
            ChitterEnvironment.Initialise();

            while( true ) {
                if( Console.ReadKey( true ).Key == ConsoleKey.Enter ) {
                    Console.Write( "TTT > " );
                    ChitterEnvironment.ConsoleCommandsManager.HandleConsoleInput( Console.ReadLine() );
                }
            }
        }
    }
}
