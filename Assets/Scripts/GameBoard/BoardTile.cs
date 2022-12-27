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
            this._tileSprite = Resources.Load<Sprite>("Sprites/Tile1");
            this._tileHeight = 0.5f;
            this._tileWidth = 0.5f;
            this._isPassable = true;
        }

        /*---------------PUBLIC----------------*/
        public float TileHeight => _tileHeight;
        public float TileWidth => _tileWidth;
        public Sprite TileSPrite => _tileSprite;
    }
}