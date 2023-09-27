using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Newtonsoft.Json;
using SomeWords.Scripts.Storage.Game;
using Surreily.SomeWords.Scripts.Enums;
using Surreily.SomeWords.Scripts.Model.Game;
using Surreily.SomeWords.Scripts.Storage.Game;

namespace Surreily.SomeWords.Scripts.Storage {
    public static class GameLoader {
        public static GameModel Load() {
            JsonGame jsonGame = LoadJsonGame();
            return GetGameModel(jsonGame);
        }

        private static JsonGame LoadJsonGame() {
            FileAccess fileAccess = FileAccess.Open("res://data/Game.json", FileAccess.ModeFlags.Read);
            string json = fileAccess.GetAsText();
            return JsonConvert.DeserializeObject<JsonGame>(json);
        }

        private static GameModel GetGameModel(JsonGame jsonGame) {
            Dictionary<string, Color> colorsById = jsonGame.Colors
                .ToDictionary(
                    color => color.Id,
                    color => new Color(color.Red / 255f, color.Green / 255f, color.Blue / 255f));

            return new GameModel {
                Name = jsonGame.Name,
                Description = jsonGame.Description,
                StartMapId = jsonGame.StartMapId,
                StartMapX = jsonGame.StartMapX,
                StartMapY = jsonGame.StartMapY,
                Maps = jsonGame.Maps
                    .Select(map => GetMapModel(map, colorsById))
                    .ToList(),
            };
        }

        private static MapModel GetMapModel(
            JsonMap jsonMap, Dictionary<string, Color> colorsById) {

            return new MapModel {
                Id = jsonMap.Id,
                Decorations = jsonMap.Decorations
                    .Select(decoration => GetDecorationModel(decoration, colorsById))
                    .ToList(),
                Paths = jsonMap.Paths
                    .Select(path => GetPathModel(path, colorsById))
                    .ToList(),
                Levels = jsonMap.Levels
                    .Select(level => GetLevelModel(level, colorsById))
                    .ToList(),
            };
        }

        private static DecorationModel GetDecorationModel(
            JsonDecoration jsonDecoration, Dictionary<string, Color> colorsById) {

            return new DecorationModel {
                X = jsonDecoration.X,
                Y = jsonDecoration.Y,
                Material = jsonDecoration.Material,
            };
        }

        private static PathModel GetPathModel(
            JsonPath jsonPath, Dictionary<string, Color> colorsById) {

            PathState state = !string.IsNullOrEmpty(jsonPath.State)
                ? Enum.Parse<PathState>(jsonPath.State)
                : PathState.Closed;

            return new PathModel {
                Position = new Vector2I(jsonPath.X, jsonPath.Y),
                State = Enum.Parse<PathState>(jsonPath.State),
                Color = colorsById[jsonPath.ColorId],
                DirectionFlags = DirectionFlags.Left | DirectionFlags.Up,
            };
        }

        private static LevelModel GetLevelModel(
            JsonLevel jsonLevel, Dictionary<string, Color> colorsById) {

            LevelState state = !string.IsNullOrEmpty(jsonLevel.State)
                ? Enum.Parse<LevelState>(jsonLevel.State)
                : LevelState.Closed;

            return new LevelModel {
                Id = jsonLevel.Id,
                X = jsonLevel.X,
                Y = jsonLevel.Y,
                Title = jsonLevel.Title,
                Description = jsonLevel.Description,
                State = state,
                Color = colorsById[jsonLevel.ColorId],
                Width = jsonLevel.Width,
                Height = jsonLevel.Height,
                StartX = jsonLevel.StartX,
                StartY = jsonLevel.StartY,
                Tiles = jsonLevel.Tiles
                    .Select(tile => GetTileModel(tile))
                    .ToList(),
                Goals = jsonLevel.Goals
                    .Select(goal => GetGoalModel(goal))
                    .ToList(),
            };
        }

        private static TileModel GetTileModel(JsonTile tile) {
            return new TileModel {
                X = tile.X,
                Y = tile.Y,
                Character = tile.Character[0],
            };
        }

        private static GoalModel GetGoalModel(JsonGoal goal) {
            return new GoalModel {
                Id = goal.Id,
                Type = Enum.Parse<GoalType>(goal.Type),
                Word = goal.Word,
                Directions = goal.Directions
                    .Select(direction => Enum.Parse<Direction>(direction))
                    .ToList(),
            };
        }
    }
}
