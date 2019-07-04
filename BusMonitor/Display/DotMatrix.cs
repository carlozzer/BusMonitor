using System;
using System.Collections.Generic;
using System.Text;

namespace BusMonitor.Display
{
    public class DotMatrix : BusDisplay
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

            //Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkGray;

            int prev = 0;
            for ( int i = 0; i < buffer.Length; i++) {

                int color = buffer[i];

                if ( color != prev ) {
                    Console.ForegroundColor = ResolveColor(color);
                }

                Console.Write( pixel );

                prev = color;
            }
        }

        public void Clear() {

            Array.Clear( buffer , 0 , buffer.Length );
            RefreshBuffer();
        }

        void SetPixel(int x, int y, int col) {
            int offset = ( (y-1) * Console.WindowWidth) + x;
            buffer[offset] = col;
        }

        public void DrawPixel(int x, int y, int col) {
            SetPixel(x, y, col);
            RefreshBuffer();
        }

        public void DrawText(int x, int y, int col,string txt)
        {
            //TODO
        }

    }
}
