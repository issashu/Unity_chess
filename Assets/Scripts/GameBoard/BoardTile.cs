using Unity.VisualScripting;
using UnityEngine;

namespace GameBoard
{
    public class BoardTile : MonoBehaviour
    {
        /*---------------PRIVATE---------------*/
        [SerializeField]private Sprite _tileSprite;
        private int _tileHeight;
        private int _tileWidth;
        private bool _isPassable;
        
        /*---------------PUBLIC----------------*/
        private void Awake()
        {
            this._tileSprite = Resources.Load<Sprite>("Sprites/Tile1");
            this._tileHeight = 1;
            this._tileWidth = 1;
            this._isPassable = true;
        }
    }
}