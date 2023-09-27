using Godot;
using Surreily.SomeWords.Scripts.Enums;

namespace Surreily.SomeWords.Scripts.Model.Game {
    public class PathModel {
        public Vector2I Position { get; set; }
        public PathState State { get; set; }
        public Color Color { get; set; }
        public DirectionFlags DirectionFlags { get; set; }
    }
}
