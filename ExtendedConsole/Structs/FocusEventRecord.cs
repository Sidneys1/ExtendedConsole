using System.Runtime.InteropServices;

namespace ExtendedConsole.Structs
{
    /// <summary>
    /// Undocumented
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FocusEventRecord
    {
        /// <summary>
        /// Reserved
        /// </summary>
        public uint bSetFocus;
    }
}