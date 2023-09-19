using System.Collections.Generic;

namespace Surreily.SomeWords.Scripts.Model.Game {
    public class GameModel {
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartMapId { get; set; }
        public int StartMapX { get; set; }
        public int StartMapY { get; set; }

        public List<MapModel> Maps { get; set; }
    }
}
