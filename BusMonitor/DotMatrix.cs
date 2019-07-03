using System;
using System.Collections.Generic;
using System.Text;

namespace BusMonitor
{
    public class DotMatrix
    {
        char pixel => '*';

        ConsoleColor prev_back;
        ConsoleColor prev_fore;

        int[] buffer = new int[Console.WindowWidth*Console.WindowHeight];


        public DotMatrix() {

            prev_back = Console.BackgroundColor;
            prev_fore = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.Black;
        }

        ~DotMatrix() {

            Console.BackgroundColor = prev_back;
            Console.ForegroundColor = prev_fore;

        }

        ConsoleColor ResolveColor(int col) {

            ConsoleColor ret = ConsoleColor.DarkGray;

            switch (col)
            {
                case 1:
                    ret = ConsoleColor.Red;
                    break;
                case 2:
                    ret = ConsoleColor.Yellow;
                    break;
                default:
                    ret = ConsoleColor.DarkGray;
                    break;
            }

            return ret;

        }

        void RefreshBuffer() {

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            
            for ( int i = 0; i < buffer.Length; i++) {

                Console.ForegroundColor = ResolveColor(buffer[i]);
                Console.Write( pixel );

            }
        }

        public void Clear() {

            Array.Clear( buffer , 0 , buffer.Length );
            RefreshBuffer();
        }

    }
}
