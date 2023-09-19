using System.Collections.Generic;
using Surreily.SomeWords.Scripts.Enums;

namespace Surreily.SomeWords.Scripts.Model.Game {
    public class GoalModel {
        public string Id { get; set; }
        public GoalType Type { get; set; }
        public string Word { get; set; }
        public List<Direction> Directions { get; set; }
    }
}
