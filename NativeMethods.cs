using System;
using System.IO;
using System.Runtime.InteropServices;
using ExtendedConsole.Structs;
using Microsoft.Win32.SafeHandles;

namespace ExtendedConsole {
    internal static class NativeMethods {
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetConsoleMode(SafeFileHandle hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("kernel32.dll", EntryPoint = "ReadConsoleInputW", CharSet = CharSet.Unicode)]
        internal static extern bool ReadConsoleInput(
            SafeFileHandle hConsoleInput,
            [Out] InputRecord[] lpBuffer,
            int nLength,
            out uint lpNumberOfEventsRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleMode(SafeFileHandle hConsoleHandle, uint dwMode);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool WriteConsoleOutput(
            SafeFileHandle hConsoleOutput,
            CharInfo[] lpBuffer,
            Coord dwBufferSize,
            Coord dwBufferCoord,
            ref SmallRect lpWriteRegion);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleScreenBufferInfoEx(
            SafeFileHandle consoleOutput,
            ref ConsoleScreenBufferInfoEx consoleScreenBufferInfoEx
            );

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetConsoleScreenBufferInfoEx(
            SafeFileHandle hConsoleOutput,
            ref ConsoleScreenBufferInfoEx consoleScreenBufferInfo
            );
    }
}
