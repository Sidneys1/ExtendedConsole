// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Local

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using ExtendedConsole.Enums;
using ExtendedConsole.Structs;
using Microsoft.Win32.SafeHandles;

namespace ExtendedConsole {
    public static class ExtendedConsole {
        #region Fields

        private static readonly Stack<ConsoleColor> BackgroundStack = new Stack<ConsoleColor>();

        private static readonly Stack<ConsoleColor> ForegroundStack = new Stack<ConsoleColor>();

        //private static readonly List<SafeFileHandle> _screenBuffers = new List<SafeFileHandle>(1);

        #endregion Fields

        #region Properties

        public static IntPtr ConsoleWindowHandle { get; }

        public static SafeFileHandle ConsoleInputHandle { get; }

        public static SafeFileHandle CurrentOutputBuffer { get;
            //set {
            //    if (NativeMethods.SetConsoleActiveScreenBuffer(value))
            //        _currOut = value;
            //    else
            //        throw new Win32Exception(Marshal.GetLastWin32Error());
            //}
        }

        //public static IEnumerable<SafeFileHandle> ScreenBuffers => _screenBuffers;

        #endregion

        #region Constructors

        static ExtendedConsole() {
            if ((ConsoleWindowHandle = NativeMethods.GetConsoleWindow()) == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());
            CurrentOutputBuffer = NativeMethods.CreateFile("CONOUT$", 0x80000000 | 0x40000000, 2, IntPtr.Zero,
                FileMode.Open, 0, IntPtr.Zero);
            
            if (CurrentOutputBuffer.IsInvalid)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            //_screenBuffers.Add(CurrentOutputBuffer);

            ConsoleInputHandle = NativeMethods.CreateFile("CONIN$", 0x80000000 | 0x40000000, 2, IntPtr.Zero,
                FileMode.Open, 0, IntPtr.Zero);
            if (ConsoleInputHandle.IsInvalid)
                throw new Win32Exception(Marshal.GetLastWin32Error());
            
        }
        
        #endregion Constructors

        #region Methods
        
        //public static SafeFileHandle CreateNewScreenBuffer() {
        //    var p = NativeMethods.CreateConsoleScreenBuffer(0x80000000 | 0x40000000, 3, IntPtr.Zero, 1, 0);
        //    if(p.IsInvalid)
        //        throw new Win32Exception(Marshal.GetLastWin32Error());
        //    _screenBuffers.Add(p);
        //    return p;
        //}

        /// <summary>
        /// Clears an area of the console buffer
        /// </summary>
        public static void ClearConsoleArea(short x, short y, short w, short h, CharInfo? character = null) {
            var defChar = character ?? new CharInfo (foregroundColor:Console.ForegroundColor, backgroundColor:Console.BackgroundColor);

            var r = new CharInfo[w * h].SetAllValues(defChar);

            var rect = new SmallRect {
                Top = y,
                Left = x,
                Bottom = (short)(y + (h - 1)),
                Right = (short)(x + (w - 1))
            };

            var size = new Coord(w, h);
            var pos = Coord.Zero;
            UpdateRegion(r, pos, size, ref rect);
        }

        /// <summary>
        /// Clears a single horizontal row of the console buffer
        /// </summary>
        public static void ClearConsoleLine(short line, CharInfo? character = null) {
            var defChar = character ?? new CharInfo(foregroundColor:Console.ForegroundColor, backgroundColor:Console.BackgroundColor);

            var r = new CharInfo[Console.BufferWidth].SetAllValues(defChar);

            var rect = new SmallRect { Top = line, Bottom = line, Left = 0, Right = (short)(r.Length - 1) };
            var size = new Coord((short)r.Length, 1);
            var pos = Coord.Zero;
            UpdateRegion(r, pos, size, ref rect);
        }

        /// <summary>
        /// Clears the current horizontal console buffer line
        /// </summary>
        public static void ClearCurrentConsoleLine() =>
            ClearConsoleLine((short)Console.CursorTop);

        /// <summary>
        /// Enables the current console window for mouse input
        /// </summary>
        public static void EnableMouseInput() {
            uint mode;
            if (!NativeMethods.GetConsoleMode(ConsoleInputHandle, out mode))
                throw new Win32Exception(Marshal.GetLastWin32Error());
            mode |= (uint)ConsoleModes.EnableMouseInput;
            mode &= ~(uint)ConsoleModes.EnableQuickEditMode;
            mode |= (uint)ConsoleModes.EnableExtendedFlags;

            if (!NativeMethods.SetConsoleMode(ConsoleInputHandle, mode))
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// Makes the current console window non-resizable
        /// </summary>
        public static void FixConsoleSize() =>
            NativeMethods.SetWindowLong(ConsoleWindowHandle, -16,
                NativeMethods.GetWindowLong(ConsoleWindowHandle, -16) ^ 0x00050000);

        /// <summary>
        /// Retrieves the current console buffer's info 
        /// </summary>
        public static void GetConsoleInfo(ref ConsoleScreenBufferInfoEx info) {
            if (!NativeMethods.GetConsoleScreenBufferInfoEx(CurrentOutputBuffer, ref info)) 
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// Gets console input records
        /// </summary>
        public static void GetConsoleInput(ref InputRecord[] buf) {
            uint r;
            if (!NativeMethods.ReadConsoleInput(ConsoleInputHandle, buf, buf.Length, out r))
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        #region Console Color Stack

        /// <summary>
        /// Sets the console foreground and background colors without popping
        /// </summary>
        public static void PeekConsoleColors() {
            if (ForegroundStack.Count == 0) return;
            Console.ForegroundColor = ForegroundStack.Peek();
            Console.BackgroundColor = BackgroundStack.Peek();
        }

        /// <summary>
        /// Sets the console foreground and background colors from the stack
        /// </summary>
        public static void PopConsoleColors() {
            if (ForegroundStack.Count == 0) return;
            Console.ForegroundColor = ForegroundStack.Pop();
            Console.BackgroundColor = BackgroundStack.Pop();
        }

        /// <summary>
        /// Pushes the current console foreground and background colors onto the stack
        /// </summary>
        public static void PushConsoleColors() {
            ForegroundStack.Push(Console.ForegroundColor);
            BackgroundStack.Push(Console.BackgroundColor);
        }

        #endregion

        /// <summary>
        /// Sets the current console buffer's info
        /// </summary>
        public static void SetConsoleInfo(ref ConsoleScreenBufferInfoEx info) {
            if(!NativeMethods.SetConsoleScreenBufferInfoEx(CurrentOutputBuffer, ref info))
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// Updates a region of the console buffer
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="point">The location in the buffer to draw the top-left buffer item</param>
        /// <param name="bufferSize">The size (width and height) of the supplied <paramref name="buffer"/></param>
        /// <param name="writeRegion">The masking rectangle of the destination console buffer. On return, is the actual region that was written to</param>
        public static void UpdateRegion(CharInfo[] buffer, Coord point, Coord bufferSize, ref SmallRect writeRegion) {
            if(! NativeMethods.WriteConsoleOutput(CurrentOutputBuffer, buffer, bufferSize, point, ref writeRegion))
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        
        #endregion Methods
    }
}