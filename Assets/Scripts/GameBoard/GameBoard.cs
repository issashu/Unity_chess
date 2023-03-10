using System;
using Defines;
using GamePieces;
using Managers;
using UnityEngine;
using Color = UnityEngine.Color;
using Point = Utils.Point;

namespace GameBoard
{
    public class GameBoard: MonoBehaviour
    {
        /*-----------MEMBERS-------------------*/
        public static GameBoard Board => _instance;
        public static event EventHandler<BasePiece> OnTileWipe; 
        private static GameBoard _instance;
        
        public ref BoardTile[,] GameBoardMatrix => ref _boardMatrix;
        private int _boardHeight;
        private int _boardWidth;
        private BoardTile[,] _boardMatrix;

        /*-----------METHODS-------------------*/
        
        public void ClearBoardColors()
        {
            foreach (var tile in _boardMatrix)
            {
                tile.ChangeTileColorTint(Color.white);
            }
        }
        public bool isPointWithinBoardLimits(Point point)
        {
            // Checks if tile is outside board and returns false if it is, or true - if it is within board
            var isOutsideBoard= point.x < 0 || point.y < 0 || point.x >= GameBoardConstants.BOARD_HEIGHT ||
                                point.y >= GameBoardConstants.BOARD_WIDTH;
            return !isOutsideBoard;
        }
        private void Awake()
        {
            // Mostly sanity check, for clones
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            _instance = this;
            
            this._boardHeight = Defines.GameBoardConstants.BOARD_HEIGHT;
            this._boardWidth = Defines.GameBoardConstants.BOARD_WIDTH;
            this._boardMatrix = new BoardTile[this._boardHeight, this._boardWidth];

            for (var x = 0; x < this._boardHeight; x++)
            {
                for (var y = 0; y < this._boardWidth; y++)
                {
                    this._boardMatrix[x , y] = SetupTileObject(x, y);
                }
            }
        }
        private void Start()
        {
            GameManager.OnDifficultySwitchWipe += WipeBoard;
            GameManager.OnDifficultySwitchSpawn += SpawnBoard;
        }
        private BoardTile SetupTileObject(int boardX, int boardY)
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

            return boardTile.GetComponent<BoardTile>();
        }
        private void WipeBoard(object eventSender, EventArgs args)
        {
            foreach (var tile in this._boardMatrix)
            {
                var unitOnTile = tile.GetComponent<BoardTile>().TileOccupant;

                if (!unitOnTile)
                    continue;
                
                OnTileWipe?.Invoke(this, unitOnTile);
            }
        }

        private void SpawnBoard(object eventSender, int gameDifficulty)
        {
            BoardSpawner.easyBoardSpawner(gameDifficulty);
        }
    }
}