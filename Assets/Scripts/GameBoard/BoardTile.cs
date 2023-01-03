using System.Collections.Generic;
using GamePieces;
using UnityEngine;
using Utils;

namespace GameBoard
{
    public class BoardTile: MonoBehaviour
    {
        /*---------------STATICS---------------*/
        private static BoardTile _instance;
        public static BoardTile Tile => _instance;

        /*---------------PRIVATE---------------*/
        private Sprite _tileSprite;
        private float _tileHeight;
        private float _tileWidth;
        private bool _isPassable;
        private Point _objectCoordinates;
        private GameObject _occupyingUnit;

        private void Awake()
        {
            this._tileSprite = Resources.Load<Sprite>("Sprites/BoardTiles/Board Tile");
            this._tileHeight = Defines.GameBoardConstants.TILE_HEIGHT;
            this._tileWidth = Defines.GameBoardConstants.TILE_WIDTH;
            this._isPassable = true;
            _instance = this;
        }
        
        /*---------------PUBLIC----------------*/
        public void UpdateTileGridPosition(int x, int y)
        {
            _objectCoordinates = new Point(x, y);
        }

        public void ChangeTileColorTint(Color coloToChangeTo)
        {
            this.GetComponent<SpriteRenderer>().material.color = coloToChangeTo;
        }

        public void SetOccupant(GameObject Occupant)
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

        
        public float TileHeight => _tileHeight;
        public float TileWidth => _tileWidth;
        public Sprite TileSprite => _tileSprite;
        public bool IsTileTraversable => _isPassable;
        public int XCoordinate => _objectCoordinates.x;
        public int YCoordinate => _objectCoordinates.y;

        public GameObject TileOccupant => _occupyingUnit;
    }
}