using System;

namespace ExtendedConsole.Enums
{
    [Flags]
    public enum MouseButtonState
    {
        FirstButton = 1,
        RightmostButton = 2,
        SecondButton = 4,
        ThirdButton = 8,
        FourthButton = 16,
    }
}