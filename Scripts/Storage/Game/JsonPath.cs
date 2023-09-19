using System;

namespace Surreily.SomeWords.Scripts.Storage.Game {
    [Serializable]
    public class JsonPath {
        public int X { get; set; }
        public int Y { get; set; }
        public string State { get; set; }
        public string ColorId { get; set; }
    }
}
