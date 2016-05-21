using System.Runtime.InteropServices;

namespace ExtendedConsole.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;

        public SmallRect(short left, short top, short right, short bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public static SmallRect FromWidthHeight(short x, short y, short width, short height) => new SmallRect(x, y, (short)(x + width), (short)(y + height));

        public int Width => Right - Left;
        public int Height => Bottom - Top;

        public int Area => Width*Height;
    }
}