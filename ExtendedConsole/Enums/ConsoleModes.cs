using System;

namespace ExtendedConsole.Enums
{
    [Flags]
    internal enum ConsoleModes : uint {
        EnableProcessedInput = 0x0001,
        EnableLineInput = 0x0002,
        EnableEchoInput = 0x0004,

        EnableWindowInput = 0x0008,
        EnableMouseInput = 0x0010,
        EnableInsertMode = 0x0020,
        EnableQuickEditMode = 0x0040,
        EnableExtendedFlags = 0x0080,
        EnableAutoPosition = 0x0100,
        EnableProcessedOutput = 0x0001,
        EnableWrapAtEolOutput = 0x0002,
        All = EnableProcessedInput | EnableLineInput | EnableEchoInput | EnableWindowInput | EnableMouseInput | EnableInsertMode | EnableQuickEditMode | EnableExtendedFlags | EnableAutoPosition
    }
}