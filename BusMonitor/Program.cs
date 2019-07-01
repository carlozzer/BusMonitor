using System;

namespace BusMonitor
{
    class Program {

        static void Main ( string[] args ) {

            EMTClient cli = new EMTClient();
            string token = cli.Login( "carlozzer@gmail.com" , "carlo33er@GMAIL.COM" );
            //cli.Lines(token);
            cli.TimeArrivalBus( "2705" , "069" , token );
        }



    }
}
