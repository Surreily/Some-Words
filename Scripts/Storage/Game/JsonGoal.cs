using System.Collections.Generic;

namespace Surreily.SomeWords.Scripts.Storage.Game {
    public class JsonGoal {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Word { get; set; }
        public List<string> Directions { get; set; }
    }
}
