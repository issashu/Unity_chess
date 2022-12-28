using GamePieces;
using UnityEngine;
using Utils;

namespace GameBoard
{
    public class BoardTile: MonoBehaviour
    {
        /*---------------PRIVATE---------------*/
        [SerializeField] private Sprite _tileSprite;
        private float _tileHeight;
        private float _tileWidth;
        [SerializeField] private bool _isPassable;
        private Point _objectCoordinates;
        private BasePiece _occupyingUnit;

        private void Awake()
        {
            this._tileSprite = Resources.Load<Sprite>("Sprites/BoardTiles/Board Tile");
            this._tileHeight = Defines.GameBoardConstants.TILE_HEIGHT;
            this._tileWidth = Defines.GameBoardConstants.TILE_WIDTH;
            this._isPassable = true;
        }
        
        /*---------------PUBLIC----------------*/
        public void UpdateTileGridPosition(int x, int y)
        {
            _objectCoordinates = new Point(x, y);
            Debug.Log(_objectCoordinates.ToString());
        }

        public void SetOccupant(BasePiece Occupant)
        {
            this._occupyingUnit = Occupant;
        }

        public void ClearOccupant()
        {
            this._occupyingUnit = null;
        }
        
        public float TileHeight => _tileHeight;
        public float TileWidth => _tileWidth;
        public Sprite TileSprite => _tileSprite;
        public bool IsTileTraversable => _isPassable;
        public int XCoordinate => _objectCoordinates.x;
        public int YCoordinate => _objectCoordinates.y;

        public BasePiece UnitOnTile => _occupyingUnit;
    }
}