using System;
using System.Collections.Generic;
using System.Text;

namespace BusMonitor.Display
{
    public interface BusDisplay
    {
        void Clear();
        void DrawPixel(int x,int y,int col);
        void DrawText (int x, int y, int col, string text);
    }
}
