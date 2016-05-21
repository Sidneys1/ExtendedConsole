using System;
using System.Runtime.InteropServices;

namespace ExtendedConsole.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ConsoleScreenBufferInfoEx
    {
        private uint StructSize;
        public Coord Size;
        public Coord CursorPosition;
        public short CurrentAttributes;
        public SmallRect WindowRectangle;
        public Coord MaximumWindowSize;
        public ushort PopupAttributes;
        public bool FullscreenSupported;

        public ColorRef Black;
        public ColorRef DarkBlue;
        public ColorRef DarkGreen;
        public ColorRef DarkCyan;
        public ColorRef DarkRed;
        public ColorRef DarkMagenta;
        public ColorRef DarkYellow;
        public ColorRef Gray;
        public ColorRef DarkGray;
        public ColorRef Blue;
        public ColorRef Green;
        public ColorRef Cyan;
        public ColorRef Red;
        public ColorRef Magenta;
        public ColorRef Yellow;
        public ColorRef White;

        public short Width
        {
            get { return Size.X; }
            set { Size.X = value; }
        }

        public short Height
        {
            get { return Size.Y; }
            set { Size.Y = value; }
        }

        public void SetColor(ConsoleColor color, byte all)
        {
            switch (color)
            {
                case ConsoleColor.Black:
                    Black.Set(all, all, all);
                    break;
                case ConsoleColor.DarkBlue:
                    DarkBlue.Set(all, all, all);
                    break;
                case ConsoleColor.DarkGreen:
                    DarkGreen.Set(all, all, all);
                    break;
                case ConsoleColor.DarkCyan:
                    DarkCyan.Set(all, all, all);
                    break;
                case ConsoleColor.DarkRed:
                    DarkRed.Set(all, all, all);
                    break;
                case ConsoleColor.DarkMagenta:
                    DarkMagenta.Set(all, all, all);
                    break;
                case ConsoleColor.DarkYellow:
                    DarkYellow.Set(all, all, all);
                    break;
                case ConsoleColor.Gray:
                    Gray.Set(all, all, all);
                    break;
                case ConsoleColor.DarkGray:
                    DarkGray.Set(all, all, all);
                    break;
                case ConsoleColor.Blue:
                    Blue.Set(all, all, all);
                    break;
                case ConsoleColor.Green:
                    Green.Set(all, all, all);
                    break;
                case ConsoleColor.Cyan:
                    Cyan.Set(all, all, all);
                    break;
                case ConsoleColor.Red:
                    Red.Set(all, all, all);
                    break;
                case ConsoleColor.Magenta:
                    Magenta.Set(all, all, all);
                    break;
                case ConsoleColor.Yellow:
                    Yellow.Set(all, all, all);
                    break;
                case ConsoleColor.White:
                    White.Set(all, all, all);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, "Not a valid ConsoleColor");
            }
        }

        public void SetColor(ConsoleColor color, byte r, byte g, byte b)
        {
            switch (color)
            {
                case ConsoleColor.Black:
                    Black.Set(r, g, b);
                    break;
                case ConsoleColor.DarkBlue:
                    DarkBlue.Set(r, g, b);
                    break;
                case ConsoleColor.DarkGreen:
                    DarkGreen.Set(r, g, b);
                    break;
                case ConsoleColor.DarkCyan:
                    DarkCyan.Set(r, g, b);
                    break;
                case ConsoleColor.DarkRed:
                    DarkRed.Set(r, g, b);
                    break;
                case ConsoleColor.DarkMagenta:
                    DarkMagenta.Set(r, g, b);
                    break;
                case ConsoleColor.DarkYellow:
                    DarkYellow.Set(r, g, b);
                    break;
                case ConsoleColor.Gray:
                    Gray.Set(r, g, b);
                    break;
                case ConsoleColor.DarkGray:
                    DarkGray.Set(r, g, b);
                    break;
                case ConsoleColor.Blue:
                    Blue.Set(r, g, b);
                    break;
                case ConsoleColor.Green:
                    Green.Set(r, g, b);
                    break;
                case ConsoleColor.Cyan:
                    Cyan.Set(r, g, b);
                    break;
                case ConsoleColor.Red:
                    Red.Set(r, g, b);
                    break;
                case ConsoleColor.Magenta:
                    Magenta.Set(r, g, b);
                    break;
                case ConsoleColor.Yellow:
                    Yellow.Set(r, g, b);
                    break;
                case ConsoleColor.White:
                    White.Set(r, g, b);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, "Not a valid ConsoleColor");
            }
        }

        public void Apply() => ExtendedConsole.SetConsoleInfo(ref this);

        public void Refresh() => ExtendedConsole.GetConsoleInfo(ref this);

        public static ConsoleScreenBufferInfoEx GetNew() => new ConsoleScreenBufferInfoEx {StructSize = 96};

        public static ConsoleScreenBufferInfoEx GetCurrent()
        {
            var ret = GetNew();
            ExtendedConsole.GetConsoleInfo(ref ret);
            return ret;
        }

        public ColorRef[] GetColors() =>
            new[]
            {
                Black, DarkBlue, DarkGreen, DarkCyan,
                DarkRed, DarkMagenta, DarkYellow, Gray,
                DarkGray, Blue, Green, Cyan,
                Red, Magenta, Yellow, White
            };

        public void SetColors(ColorRef[] colors)
        {
            if (colors.Length != 16)
                throw new ArgumentException("Array must have 16 entries", nameof(colors));

            Black = colors[0];
            DarkBlue = colors[1];
            DarkGreen = colors[2];
            DarkCyan = colors[3];
            DarkRed = colors[4];
            DarkMagenta = colors[5];
            DarkYellow = colors[6];
            Gray = colors[7];
            DarkGray = colors[8];
            Blue = colors[9];
            Green = colors[10];
            Cyan = colors[11];
            Red = colors[12];
            Magenta = colors[13];
            Yellow = colors[14];
            White = colors[15];
        }
    }
}