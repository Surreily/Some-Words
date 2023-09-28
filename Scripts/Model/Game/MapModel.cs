using System.Collections.Generic;

namespace Surreily.SomeWords.Scripts.Model.Game {
    public class MapModel {
        public GameModel Game { get; set; }

        public string Id { get; set; }
        public List<DecorationModel> Decorations { get; set; }
        public List<PathModel> Paths { get; set; }
        public List<LevelModel> Levels { get; set; }
    }
}
