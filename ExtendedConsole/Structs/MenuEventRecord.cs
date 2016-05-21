using System.Runtime.InteropServices;

namespace ExtendedConsole.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MenuEventRecord
    {
        public uint CommandID;
    }
}