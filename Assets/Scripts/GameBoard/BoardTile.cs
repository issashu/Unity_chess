using GamePieces;
using UnityEngine;
using Utils;

namespace GameBoard
{
    public class BoardTile: MonoBehaviour
    {
        /*-----------MEMBERS-------------------*/
        public static BoardTile Tile => _instance;
        private static BoardTile _instance; 
        
        private Sprite _tileSprite;
        private float _tileHeight;
        private float _tileWidth;
        private bool _isPassable;
        private Point _objectCoordinates;
        private BasePiece _occupyingUnit;

        public SpriteRenderer TileSpriteRenderer => this.GetComponent<SpriteRenderer>();
        public Sprite TileSprite => _tileSprite;
        public BasePiece TileOccupant => _occupyingUnit;
        public float TileHeight => _tileHeight;
        public float TileWidth => _tileWidth;
      
        public bool IsTileTraversable => _isPassable;
        public int XCoordinate => _objectCoordinates.x;
        public int YCoordinate => _objectCoordinates.y;
        

        /*-----------METHODS-------------------*/
           
        public void UpdateTileGridPosition(int x, int y)
        {
            _objectCoordinates = new Point(x, y);
        }

        public void ChangeTileColorTint(Color coloToChangeTo)
        {
            this.TileSpriteRenderer.material.color = coloToChangeTo;
        }

        public void SetOccupant(BasePiece Occupant)
        {
            this._occupyingUnit = Occupant;
        }

        public void ClearOccupant()
        {
            this._occupyingUnit = null;
        }

        public bool isTileOccupied()
        {
            return this._occupyingUnit is not null;
        }
        private void Awake()
        {
            this._tileSprite = Resources.Load<Sprite>("Sprites/BoardTiles/Board Tile");
            this._tileHeight = Defines.GameBoardConstants.TILE_HEIGHT;
            this._tileWidth = Defines.GameBoardConstants.TILE_WIDTH;
            this._isPassable = true;
            _instance = this;
        }
        

        
    }
}