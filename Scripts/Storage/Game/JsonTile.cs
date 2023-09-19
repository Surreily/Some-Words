using System;

namespace Surreily.SomeWords.Scripts.Storage.Game {
    [Serializable]
    public class JsonTile {
        public int X { get; set; }
        public int Y { get; set; }
        public string Character { get; set; }
    }
}
