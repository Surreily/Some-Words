using Godot;
using Surreily.SomeWords.Scripts.Enums;

namespace Surreily.SomeWords.Scripts.Model.Game {
    public class PathModel {
        public MapModel Map { get; set; }

        public Vector2I Position { get; set; }
        public PathState State { get; set; }
        public Color Color { get; set; }
        public DirectionFlags DirectionFlags { get; set; }

        public bool IsHidden => State == PathState.Hidden;
    }
}
