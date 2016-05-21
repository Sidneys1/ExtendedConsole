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

        public bool Intersects(SmallRect comp) => comp.Top <= Bottom && comp.Bottom >= Top && comp.Left >= Right && comp.Right <= Left;

        

        public static SmallRect operator +(SmallRect left, Coord right) => new SmallRect((short) (left.Left+right.X), (short) (left.Top+right.Y), (short) (left.Right+right.X), (short) (left.Bottom+right.Y));
    }
}