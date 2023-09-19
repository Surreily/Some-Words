using System;
using System.Collections.Generic;

namespace Surreily.SomeWords.Scripts.Storage.Game {
    [Serializable]
    public class JsonLevel {
        public string Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string ColorId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public List<JsonTile> Tiles { get; set; }
        public List<JsonGoal> Goals { get; set; }
    }
}
