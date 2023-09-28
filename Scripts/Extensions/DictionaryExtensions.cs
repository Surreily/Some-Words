using System.Collections.Generic;
using Godot;
using Surreily.SomeWords.Scripts.Enums;

namespace Surreily.SomeWords.Scripts.Extensions {
    public static class DictionaryExtensions {
        public static T Get<T>(
            this Dictionary<Vector2I, T> dictionary, Vector2I position) {
            return dictionary[position];
        }

        public static T Get<T>(
            this Dictionary<Vector2I, T> dictionary, Vector2I position, Direction direction) {
            return dictionary.Get(position.GetInDirection(direction));
        }

        public static bool Exists<T>(
            this Dictionary<Vector2I, T> dictionary, Vector2I position) {
            return dictionary.ContainsKey(position);
        }

        public static bool Exists<T>(
            this Dictionary<Vector2I, T> dictionary, Vector2I position, Direction direction) {
            return dictionary.Exists(position.GetInDirection(direction));
        }
    }
}
