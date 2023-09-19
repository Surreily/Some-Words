using System;
using System.Collections.Generic;
using SomeWords.Scripts.Storage.Game;

namespace Surreily.SomeWords.Scripts.Storage.Game {
    public class JsonGame {
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartMapId { get; set; }
        public int StartMapX { get; set; }
        public int StartMapY { get; set; }
        public List<JsonColor> Colors { get; set; }
        public List<JsonMap> Maps { get; set; }
    }
}
