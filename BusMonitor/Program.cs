﻿using BusMonitor.BLL.Clients;
using BusMonitor.Display;
using System;

namespace BusMonitor
{
    class Program {

        static void Main ( string[] args ) {

            EMTClient cli = new EMTClient();
            string token = cli.Login( "carlozzer@gmail.com" , "carlo33er@GMAIL.COM" );

            BusDisplay display = new Terminal();
            display.Clear();
            //display.DrawPixel(3, 3, 1);
            //display.DrawPixel(5, 10, 2);
            

            
            do
            {
                while (!Console.KeyAvailable)
                {
                    Console.Clear();
                    Console.WriteLine("Press ESC to stop");

                    //int seconds = cli.TimeArrivalBus("2705", new string[] { "C2" }, token);
                    //string line = $"2705 C2 {(seconds / 60).ToString("00")}:{(seconds%60).ToString("00")}";

                    //display.DrawText( 1 , 1 , 1 , line );

                    System.Threading.Thread.Sleep(10000);
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            
        }



    }
}
