using System.Collections.Generic;
using System.Linq;
using Godot;
using Surreily.SomeWords.Scripts.Enums;
using Surreily.SomeWords.Scripts.Extensions;
using Surreily.SomeWords.Scripts.Model.Game;

namespace Surreily.SomeWords.Scripts.Algorithms {

    // TODO: Rename this!
    public class MapAlgorithm {
        private readonly Dictionary<Vector2I, PathModel> pathDictionary;
        private readonly Dictionary<Vector2I, LevelModel> levelDictionary;
        private readonly HashSet<Vector2I> updatePositions;

        public MapAlgorithm(MapModel map) {
            pathDictionary = map.Paths
                .ToDictionary(p => p.Position);
            levelDictionary = map.Levels
                .ToDictionary(l => l.Position);
            updatePositions = new HashSet<Vector2I>();
        }

        public void InitializeMap(MapModel map) {
            foreach (PathModel path in map.Paths) {
                ForceUpdatePath(path);
            }

            foreach (LevelModel level in map.Levels) {
                ForceUpdateLevel(level);
            }
        }

        public void OpenGoal(GoalModel goal) {
            foreach (Direction direction in goal.Directions) {
                Update(goal.Level.Position.GetInDirection(direction));
            }
        }

        private void Update(Vector2I position) {
            if (updatePositions.Contains(position)) {
                return;
            }

            if (pathDictionary.TryGetValue(position, out PathModel path)) {
                UpdatePath(path);
            } else if (levelDictionary.TryGetValue(position, out LevelModel level)) {
                UpdateLevel(level);
            }
        }

        private void UpdatePath(PathModel path) {
            bool up = GetShouldJoin(path.Position, Direction.Up, path.IsHidden);
            bool right = GetShouldJoin(path.Position, Direction.Right, path.IsHidden);
            bool down = GetShouldJoin(path.Position, Direction.Down, path.IsHidden);
            bool left = GetShouldJoin(path.Position, Direction.Left, path.IsHidden);

            DirectionFlags directionFlags = GetDirectionFlags(up, right, down, left);

            if (path.State == PathState.Open && path.DirectionFlags == directionFlags) {
                return;
            }

            path.State = PathState.Open;
            path.DirectionFlags = directionFlags;

            updatePositions.Add(path.Position);

            Update(path.Position.GetInDirection(Direction.Up));
            Update(path.Position.GetInDirection(Direction.Right));
            Update(path.Position.GetInDirection(Direction.Down));
            Update(path.Position.GetInDirection(Direction.Left));
        }

        private void ForceUpdatePath(PathModel path) {
            bool up = GetShouldJoin(path.Position, Direction.Up, path.IsHidden);
            bool right = GetShouldJoin(path.Position, Direction.Right, path.IsHidden);
            bool down = GetShouldJoin(path.Position, Direction.Down, path.IsHidden);
            bool left = GetShouldJoin(path.Position, Direction.Left, path.IsHidden);

            DirectionFlags directionFlags = GetDirectionFlags(up, right, down, left);

            path.State = PathState.Open;
            path.DirectionFlags = directionFlags;
        }

        private void UpdateLevel(LevelModel level) {
            if (level.State == LevelState.Open) {
                return;
            }

            level.State = LevelState.Open;

            updatePositions.Add(level.Position);
        }

        private void ForceUpdateLevel(LevelModel level) {
            level.State = LevelState.Open;
        }

        private bool GetShouldJoin(Vector2I position, Direction direction, bool isHidden) {
            return GetShouldJoin(position.GetInDirection(direction), isHidden);
        }

        private bool GetShouldJoin(Vector2I position, bool isHidden) {
            if (pathDictionary.TryGetValue(position, out PathModel path)) {
                return isHidden || path.State != PathState.Hidden;
            }

            if (levelDictionary.TryGetValue(position, out LevelModel level)) {
                return isHidden || level.State != LevelState.Hidden;
            }

            return false;
        }

        private static DirectionFlags GetDirectionFlags(bool up, bool right, bool down, bool left) {
            DirectionFlags directionFlags = DirectionFlags.None;

            if (up) {
                directionFlags |= DirectionFlags.Up;
            }

            if (right) {
                directionFlags |= DirectionFlags.Right;
            }

            if (down) {
                directionFlags |= DirectionFlags.Down;
            }

            if (left) {
                directionFlags |= DirectionFlags.Left;
            }

            return directionFlags;
        }
    }
}
