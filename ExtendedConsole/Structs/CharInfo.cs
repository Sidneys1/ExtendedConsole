using System;
using System.Runtime.InteropServices;

namespace ExtendedConsole.Structs
{
    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
        public CharInfo(char defaultChar = ' ',
            ConsoleColor foregroundColor = ConsoleColor.Gray,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            CharUnion = new CharUnion {UnicodeChar = defaultChar};
            Attributes = (ushort) ((int) foregroundColor | ((int) backgroundColor << 4));
        }

        [FieldOffset(0)] public CharUnion CharUnion;
        [FieldOffset(2)] public ushort Attributes;

        public ConsoleColor BackgroundColor
        {
            get { return (ConsoleColor) ((Attributes >> 4) & 0xF); }
            set { Attributes = (ushort) ((Attributes & 0xF) | ((int) value << 4)); }
        }

        public ConsoleColor ForegroundColor
        {
            get { return (ConsoleColor) (Attributes & 0xF); }
            set { Attributes = (ushort) ((Attributes & 0xF0) | (int) value); }
        }

        public char Char
        {
            get { return CharUnion.UnicodeChar; }
            set { CharUnion.UnicodeChar = value; }
        }

        public bool Underscore {
            get { return (Attributes & 0x8000) == 0x8000; }
            set {
                if (value)
                    Attributes |= 0x8000;
                else
                    Attributes &= 0x7FFF;
            }
        }

        public bool Overscore
        {
            get { return (Attributes & 0x0400) == 0x0400; }
            set
            {
                if (value)
                    Attributes |= 0x0400;
                else
                    Attributes &= 0xFBFF;
            }
        }
    }
}