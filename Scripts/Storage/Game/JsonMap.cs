using System;
using System.Collections.Generic;
using Surreily.SomeWords.Scripts.Storage.Game;

namespace SomeWords.Scripts.Storage.Game {
    [Serializable]
    public class JsonMap {
        public string Id { get; set; }
        public List<JsonDecoration> Decorations { get; set; }
        public List<JsonPath> Paths { get; set; }
        public List<JsonLevel> Levels { get; set; }
    }
}
