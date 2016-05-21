using System;

namespace ExtendedConsole
{
    public static class ExtensionMethods
    {
        #region Struct Array

        public static T[] SetAllValues<T>(this T[] array, T value) where T : struct {
            for (long i = 0; i < array.LongLength; i++) array[i] = value;
            return array;
        }

        #endregion
        
        #region String/FormattedString

        public static FormattedText Reset(this string str)
            => new FormattedText(str, reset: true);

        public static FormattedText Blue(this string str, bool dark = false)
            => new FormattedText(str, dark ? ConsoleColor.DarkBlue : ConsoleColor.Blue);

        public static FormattedText Blue(this FormattedText str, bool dark = false) {
            str.ForegroundColor = dark ? ConsoleColor.DarkBlue : ConsoleColor.Blue;
            return str;
        }

        public static FormattedText BlueBack(this string str, bool dark = false)
            => new FormattedText(str, bcolor: dark ? ConsoleColor.DarkBlue : ConsoleColor.Blue);

        public static FormattedText BlueBack(this FormattedText str, bool dark = false) {
            str.BackgroundColor = dark ? ConsoleColor.DarkBlue : ConsoleColor.Blue;
            return str;
        }

        public static FormattedText Cyan(this string str, bool dark = false)
            => new FormattedText(str, dark ? ConsoleColor.DarkCyan : ConsoleColor.Cyan);

        public static FormattedText Cyan(this FormattedText str, bool dark = false) {
            str.ForegroundColor = dark ? ConsoleColor.DarkCyan : ConsoleColor.Cyan;
            return str;
        }

        public static FormattedText CyanBack(this string str, bool dark = false)
            => new FormattedText(str, bcolor: dark ? ConsoleColor.DarkCyan : ConsoleColor.Cyan);

        public static FormattedText CyanBack(this FormattedText str, bool dark = false) {
            str.BackgroundColor = dark ? ConsoleColor.DarkCyan : ConsoleColor.Cyan;
            return str;
        }

        public static FormattedText DarkGray(this string str, bool dark = false)
            => new FormattedText(str, dark ? ConsoleColor.Black : ConsoleColor.DarkGray);

        public static FormattedText DarkGray(this FormattedText str, bool dark = false) {
            str.ForegroundColor = dark ? ConsoleColor.Black : ConsoleColor.DarkGray;
            return str;
        }

        public static FormattedText DarkGrayBack(this string str, bool dark = false)
            => new FormattedText(str, bcolor: dark ? ConsoleColor.Black : ConsoleColor.DarkGray);

        public static FormattedText DarkGrayBack(this FormattedText str, bool dark = false) {
            str.BackgroundColor = dark ? ConsoleColor.Black : ConsoleColor.DarkGray;
            return str;
        }

        public static FormattedText Green(this string str, bool dark = false)
            => new FormattedText(str, dark ? ConsoleColor.DarkGreen : ConsoleColor.Green);

        public static FormattedText Green(this FormattedText str, bool dark = false) {
            str.ForegroundColor = dark ? ConsoleColor.DarkGreen : ConsoleColor.Green;
            return str;
        }

        public static FormattedText GreenBack(this string str, bool dark = false)
            => new FormattedText(str, bcolor: dark ? ConsoleColor.DarkGreen : ConsoleColor.Green);

        public static FormattedText GreenBack(this FormattedText str, bool dark = false) {
            str.BackgroundColor = dark ? ConsoleColor.DarkGreen : ConsoleColor.Green;
            return str;
        }

        public static FormattedText Magenta(this string str, bool dark = false)
            => new FormattedText(str, dark ? ConsoleColor.DarkMagenta : ConsoleColor.Magenta);

        public static FormattedText Magenta(this FormattedText str, bool dark = false) {
            str.ForegroundColor = dark ? ConsoleColor.DarkMagenta : ConsoleColor.Magenta;
            return str;
        }

        public static FormattedText MagentaBack(this string str, bool dark = false)
            => new FormattedText(str, bcolor: dark ? ConsoleColor.DarkMagenta : ConsoleColor.Magenta);

        public static FormattedText MagentaBack(this FormattedText str, bool dark = false) {
            str.BackgroundColor = dark ? ConsoleColor.DarkMagenta : ConsoleColor.Magenta;
            return str;
        }

        public static FormattedText Red(this string str, bool dark = false)
            => new FormattedText(str, dark ? ConsoleColor.DarkRed : ConsoleColor.Red);

        public static FormattedText Red(this FormattedText str, bool dark = false) {
            str.ForegroundColor = dark ? ConsoleColor.DarkRed : ConsoleColor.Red;
            return str;
        }

        public static FormattedText RedBack(this string str, bool dark = false)
            => new FormattedText(str, bcolor: dark ? ConsoleColor.DarkRed : ConsoleColor.Red);

        public static FormattedText RedBack(this FormattedText str, bool dark = false) {
            str.BackgroundColor = dark ? ConsoleColor.DarkRed : ConsoleColor.Red;
            return str;
        }

        public static FormattedText White(this string str, bool dark = false)
            => new FormattedText(str, dark ? ConsoleColor.Gray : ConsoleColor.White);

        public static FormattedText White(this FormattedText str, bool dark = false) {
            str.ForegroundColor = dark ? ConsoleColor.Gray : ConsoleColor.White;
            return str;
        }

        public static FormattedText WhiteBack(this string str, bool dark = false)
            => new FormattedText(str, bcolor: dark ? ConsoleColor.Gray : ConsoleColor.White);

        public static FormattedText WhiteBack(this FormattedText str, bool dark = false) {
            str.BackgroundColor = dark ? ConsoleColor.Gray : ConsoleColor.White;
            return str;
        }

        public static FormattedText Yellow(this string str, bool dark = false)
            => new FormattedText(str, dark ? ConsoleColor.DarkYellow : ConsoleColor.Yellow);

        public static FormattedText Yellow(this FormattedText str, bool dark = false) {
            str.ForegroundColor = dark ? ConsoleColor.DarkYellow : ConsoleColor.Yellow;
            return str;
        }

        public static FormattedText YellowBack(this string str, bool dark = false)
            => new FormattedText(str, bcolor: dark ? ConsoleColor.DarkYellow : ConsoleColor.Yellow);

        public static FormattedText YellowBack(this FormattedText str, bool dark = false) {
            str.BackgroundColor = dark ? ConsoleColor.DarkYellow : ConsoleColor.Yellow;
            return str;
        }

        #endregion
    }
}