using System;

namespace Surreily.SomeWords.Scripts.Enums {
    [Flags]
    public enum DirectionFlags {
        None = 0,
        Up = 1,
        Right = 2,
        Down = 4,
        Left = 8,
    }
}
