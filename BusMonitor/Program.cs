using BusMonitor.BLL.EMT;
using System;

namespace BusMonitor
{
    class Program {

        static void Main ( string[] args ) {

            EMTClient cli = new EMTClient();
            string token = cli.Login( "carlozzer@gmail.com" , "carlo33er@GMAIL.COM" );

            
            do
            {
                while (!Console.KeyAvailable)
                {
                    Console.Clear();
                    Console.WriteLine("Press ESC to stop");
                    int seconds = cli.TimeArrivalBus("2705", "C2", token);
                    Console.WriteLine($"C2 {(seconds / 60).ToString("00")}:{(seconds%60).ToString("00")}");
                    System.Threading.Thread.Sleep(10000);
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            
        }



    }
}
