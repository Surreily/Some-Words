using System.Collections.Generic;
using Godot;
using Surreily.SomeWords.Scripts.Enums;
using Surreily.SomeWords.Scripts.Model.Game;
using Surreily.SomeWords.Scripts.Storage;

namespace SomeWords.Scripts.Map {
    public partial class MapManager : Node {
        private TileMap tileMap;

        private Dictionary<DirectionFlags, Vector2I> pathPositions;

        public override void _Ready() {
            tileMap = GetNode<TileMap>("TileMap");
            tileMap.TileSet = SetUpTileSet();

            SetUpPathPositions();

            GameModel gameModel = GameLoader.Load();
            LoadMap(gameModel.Maps[0]);
        }

        private void LoadMap(MapModel map) {
            foreach (PathModel path in map.Paths) {
                tileMap.SetCell(0, path.Position, 0, pathPositions[path.DirectionFlags], 0);
            }
        }

        private TileSet SetUpTileSet() {
            TileSet tileSet = new TileSet();
            tileSet.TileShape = TileSet.TileShapeEnum.Square;
            tileSet.TileSize = new Vector2I(8, 8);

            TileSetAtlasSource tileSetSource = new TileSetAtlasSource();
            tileSetSource.Texture = GD.Load<Texture2D>("res://Textures/Map/MapPaths.png");
            tileSetSource.TextureRegionSize = new Vector2I(8, 8);

            for (int x = 0; x < 4; x++) {
                for (int y = 0; y < 4; y++) {
                    Vector2I position = new Vector2I(x, y);

                    tileSetSource.CreateTile(position);

                    tileSetSource.CreateAlternativeTile(position, 1);
                    TileData alternativeTileData = tileSetSource.GetTileData(position, 1);
                    alternativeTileData.Modulate = new Color(1f, 0f, 0f);
                }
            }

            tileSet.AddSource(tileSetSource, 0);

            return tileSet;
        }

        private void SetUpPathPositions() {
            pathPositions = new Dictionary<DirectionFlags, Vector2I> {
                { DirectionFlags.None, new Vector2I(0, 0) },
                { DirectionFlags.Up, new Vector2I(1, 0) },
                { DirectionFlags.Right, new Vector2I(2, 0) },
                { DirectionFlags.Down, new Vector2I(3, 0) },
                { DirectionFlags.Left, new Vector2I(0, 1) },
                { DirectionFlags.Up | DirectionFlags.Right, new Vector2I(1, 1) },
                { DirectionFlags.Up | DirectionFlags.Down, new Vector2I(2, 1) },
                { DirectionFlags.Up | DirectionFlags.Left, new Vector2I(3, 1) },
                { DirectionFlags.Right | DirectionFlags.Down, new Vector2I(0, 2) },
                { DirectionFlags.Right | DirectionFlags.Left, new Vector2I(1, 2) },
                { DirectionFlags.Down | DirectionFlags.Left, new Vector2I(2, 2) },
                { DirectionFlags.Up | DirectionFlags.Right | DirectionFlags.Down, new Vector2I(3, 2) },
                { DirectionFlags.Right | DirectionFlags.Down | DirectionFlags.Left, new Vector2I(0, 3) },
                { DirectionFlags.Down | DirectionFlags.Left | DirectionFlags.Up, new Vector2I(1, 3) },
                { DirectionFlags.Left | DirectionFlags.Up | DirectionFlags.Right, new Vector2I(2, 3) },
                { DirectionFlags.Up | DirectionFlags.Right | DirectionFlags.Down | DirectionFlags.Left, new Vector2I(3, 3) },
            };
        }
    }
}
