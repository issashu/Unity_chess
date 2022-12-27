using Unity.VisualScripting;
using UnityEngine;

namespace GameBoard
{
    public class BoardTile : MonoBehaviour
    {
        /*---------------PRIVATE---------------*/
        [SerializeField] private Sprite _tileSprite;
        private float _tileHeight;
        private float _tileWidth;
        private bool _isPassable;

        private void Awake()
        {
            this._tileSprite = Resources.Load<Sprite>("Sprites/BoardTiles/Board Tile");
            this._tileHeight = Defines.GameBoardConstants.TILE_HEIGHT;
            this._tileWidth = Defines.GameBoardConstants.TILE_WIDTH;
            this._isPassable = true;
        }

        /*---------------PUBLIC----------------*/
        public float TileHeight => _tileHeight;
        public float TileWidth => _tileWidth;
        public Sprite TileSprite => _tileSprite;
        public bool IsTileTraversable => _isPassable;
    }
}