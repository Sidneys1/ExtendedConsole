using System.Drawing;
using System.Runtime.InteropServices;

namespace ExtendedConsole.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    public struct ColorRef
    {
        public ColorRef(byte r, byte g, byte b)
        {
            Value = 0;
            R = r;
            G = g;
            B = b;
        }

        public ColorRef(uint value)
        {
            R = 0;
            G = 0;
            B = 0;
            Value = value & 0x00FFFFFF;
        }

        [FieldOffset(0)] public byte R;
        [FieldOffset(1)] public byte G;
        [FieldOffset(2)] public byte B;

        [FieldOffset(0)] public uint Value;

        public void Set(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public Color Color
        {
            get { return Color.FromArgb(R, G, B); }
            set
            {
                R = value.R;
                G = value.G;
                B = value.B;
            }
        }
    }
}