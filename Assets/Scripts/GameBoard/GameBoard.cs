using System.Drawing;
using Defines;
using Unity.VisualScripting;
using UnityEngine;
using Color = UnityEngine.Color;
using Point = Utils.Point;

namespace GameBoard
{
    public class GameBoard: MonoBehaviour
    {
        private static GameBoard _instance;
        private int _boardHeight;
        private int _boardWidth;
        private GameObject[,] _boardMatrix;

        public ref GameObject[,] BoardMatrix => ref _boardMatrix;
        public static GameBoard Board => _instance;
       

        private void Awake()
        {
            _instance = this;
            this._boardHeight = Defines.GameBoardConstants.BOARD_HEIGHT;
            this._boardWidth = Defines.GameBoardConstants.BOARD_WIDTH;
            this._boardMatrix = new GameObject[this._boardHeight, this._boardWidth];

            for (var x = 0; x < this._boardHeight; x++)
            {
                for (var y = 0; y < this._boardWidth; y++)
                {
                    this._boardMatrix[x , y] = SetupTileObject(x, y);
                }
            }
        }

        private GameObject SetupTileObject(int boardX, int boardY)
        {
            var boardTile = new GameObject($"Tile {boardX} {boardY}");
            boardTile.AddComponent<BoardTile>();
            boardTile.AddComponent<SpriteRenderer>();
            boardTile.AddComponent<BoxCollider2D>();
            
            boardTile.GetComponent<SpriteRenderer>().sprite = BoardTile.Tile.TileSprite;
            boardTile.GetComponent<SpriteRenderer>().sortingLayerName = GameBoardConstants.BOARD_TILES_LAYER;
            boardTile.GetComponent<Collider2D>().isTrigger = true;
            boardTile.GetComponent<BoxCollider2D>().size = new Vector2(1.21f, 0.78f);
            
            var height = BoardTile.Tile.TileHeight;
            var width = BoardTile.Tile.TileWidth;
            var xAxisPosition = (boardX * height + boardY * width) * GameBoardConstants.HALF;
            var yAxisPosition = (boardX * height - boardY * width) * GameBoardConstants.QUARTER;
            
            boardTile.transform.position = new Vector2(xAxisPosition, yAxisPosition);
            BoardTile.Tile.UpdateTileGridPosition(boardX, boardY);
            boardTile.transform.parent = transform;

            return boardTile;
        }

        public GameObject GetTileFromMatrix(int x, int y)
        {
            return this._boardMatrix[x, y];
        }
        
        public void ClearBoardColors()
        {
            foreach (var tile in _boardMatrix)
            {
                tile.GetComponent<BoardTile>().ChangeTileColorTint(Color.white);
            }
        }

        public bool isPointWithinBoardLimits(Point point)
        {
            // Checks if tile is outside board and returns false if it is, or true - if it is within board
            var isOutsideBoard= point.x < 0 || point.y < 0 || point.x >= GameBoardConstants.BOARD_HEIGHT ||
                                point.y >= GameBoardConstants.BOARD_WIDTH;
            return !isOutsideBoard;
        }

    }
}