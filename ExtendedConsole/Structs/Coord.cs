using System.Runtime.InteropServices;

namespace ExtendedConsole.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
        public short X;
        public short Y;

        public Coord(short x, short y)
        {
            X = x;
            Y = y;
        }

        public static Coord Zero = new Coord(0, 0);
    };
}