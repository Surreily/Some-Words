using System;

namespace Surreily.SomeWords.Scripts.Storage.Game {
    [Serializable]
    public class JsonColor {
        public string Id { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
    }
}
