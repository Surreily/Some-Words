using Godot;
using Surreily.SomeWords.Scripts.Enums;

namespace Surreily.SomeWords.Scripts.Extensions {
    public static class Vector2IExtensions {
        public static Vector2I GetInDirection(this Vector2I position, Direction direction) {
            return new Vector2I(position.X + direction.GetXOffset(), position.Y + direction.GetYOffset());
        }
    }
}
