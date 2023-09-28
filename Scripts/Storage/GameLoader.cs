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

        #region Get game model

        private static GameModel GetGameModel(JsonGame jsonGame) {
            Dictionary<string, Color> colorsById = jsonGame.Colors
                .ToDictionary(
                    color => color.Id,
                    color => new Color(color.Red / 255f, color.Green / 255f, color.Blue / 255f));

            GameModel gameModel = new GameModel {
                Name = jsonGame.Name,
                Description = jsonGame.Description,
                StartMapId = jsonGame.StartMapId,
                StartMapX = jsonGame.StartMapX,
                StartMapY = jsonGame.StartMapY,
            };

            gameModel.Maps = jsonGame.Maps
                .Select(map => GetMapModel(gameModel, map, colorsById))
                .ToList();

            return gameModel;
        }

        private static MapModel GetMapModel(
            GameModel gameModel, JsonMap jsonMap, Dictionary<string, Color> colorsById) {

            MapModel mapModel = new MapModel {
                Game = gameModel,
                Id = jsonMap.Id,
            };

            mapModel.Decorations = jsonMap.Decorations
                .Select(decoration => GetDecorationModel(mapModel, decoration, colorsById))
                .ToList();

            mapModel.Paths = jsonMap.Paths
                .Select(path => GetPathModel(mapModel, path, colorsById))
                .ToList();

            mapModel.Levels = jsonMap.Levels
                .Select(level => GetLevelModel(mapModel, level, colorsById))
                .ToList();

            return mapModel;
        }

        private static DecorationModel GetDecorationModel(
            MapModel mapModel, JsonDecoration jsonDecoration, Dictionary<string, Color> colorsById) {

            return new DecorationModel {
                Map = mapModel,
                X = jsonDecoration.X,
                Y = jsonDecoration.Y,
                Material = jsonDecoration.Material,
            };
        }

        private static PathModel GetPathModel(
            MapModel mapModel, JsonPath jsonPath, Dictionary<string, Color> colorsById) {

            PathState state = !string.IsNullOrEmpty(jsonPath.State)
                ? Enum.Parse<PathState>(jsonPath.State)
                : PathState.Closed;

            return new PathModel {
                Map = mapModel,
                Position = new Vector2I(jsonPath.X, jsonPath.Y),
                State = Enum.Parse<PathState>(jsonPath.State),
                Color = colorsById[jsonPath.ColorId],
                DirectionFlags = DirectionFlags.Left | DirectionFlags.Up,
            };
        }

        private static LevelModel GetLevelModel(
            MapModel mapModel, JsonLevel jsonLevel, Dictionary<string, Color> colorsById) {

            LevelState state = !string.IsNullOrEmpty(jsonLevel.State)
                ? Enum.Parse<LevelState>(jsonLevel.State)
                : LevelState.Closed;

            LevelModel levelModel = new LevelModel {
                Map = mapModel,
                Id = jsonLevel.Id,
                Position = new Vector2I(jsonLevel.X, jsonLevel.Y),
                Title = jsonLevel.Title,
                Description = jsonLevel.Description,
                State = state,
                Color = colorsById[jsonLevel.ColorId],
                Width = jsonLevel.Width,
                Height = jsonLevel.Height,
                StartX = jsonLevel.StartX,
                StartY = jsonLevel.StartY,
            };

            levelModel.Tiles = jsonLevel.Tiles
                .Select(tile => GetTileModel(levelModel, tile))
                .ToList();
            levelModel.Goals = jsonLevel.Goals
                .Select(goal => GetGoalModel(levelModel, goal))
                .ToList();

            return levelModel;
        }

        private static TileModel GetTileModel(LevelModel levelModel, JsonTile jsonTile) {
            return new TileModel {
                Level = levelModel,
                X = jsonTile.X,
                Y = jsonTile.Y,
                Character = jsonTile.Character[0],
            };
        }

        private static GoalModel GetGoalModel(LevelModel levelModel, JsonGoal jsonGoal) {
            return new GoalModel {
                Level = levelModel,
                Id = jsonGoal.Id,
                Type = Enum.Parse<GoalType>(jsonGoal.Type),
                Word = jsonGoal.Word,
                Directions = jsonGoal.Directions
                    .Select(direction => Enum.Parse<Direction>(direction))
                    .ToList(),
            };
        }

        #endregion

    }
}
