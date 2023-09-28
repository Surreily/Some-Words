using System.Collections.Generic;
using Godot;
using Surreily.SomeWords.Scripts.Enums;

namespace Surreily.SomeWords.Scripts.Model.Game {
    public class LevelModel {
        public MapModel Map { get; set; }

        public string Id { get; set; }
        public Vector2I Position { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public LevelState State { get; set; }
        public Color Color { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }

        public List<TileModel> Tiles { get; set; }
        public List<GoalModel> Goals { get; set; }

        public bool IsHidden => State == LevelState.Hidden;
    }
}
