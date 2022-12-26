using Unity.VisualScripting;
using UnityEngine;

namespace GameBoard
{
    public class GameBoard: MonoBehaviour
    {
        [SerializeField]private int _boardHeight;
        [SerializeField]private int _boardWidth;
        private GameObject[,] _boardMatrix;

        public ref GameObject[,] BoardMatrix => ref _boardMatrix;
       

        private void Awake()
        {
            this._boardHeight = 8;
            this._boardWidth = 8;
            this._boardMatrix = new GameObject[this._boardHeight, this._boardWidth];
            
            var tmp = new GameObject("Tile");
            tmp.AddComponent<BoardTile>();
            tmp.AddComponent<SpriteRenderer>();
            
            for (var i = 0; i < this._boardHeight; i++)
            {
                for (var j = 0; j < this._boardWidth; j++)
                {
                    SetupTileObject(i, j, ref tmp);
                }
            }
            
            Destroy(tmp);
        }

        private void SetupTileObject(int boardX, int boardY, ref GameObject objectToFill)
        {
            ref var tmpObject = ref this._boardMatrix[boardX, boardY];
            
            tmpObject = Instantiate(objectToFill, transform);
            tmpObject.name = "Tile: " + boardX + " " + boardY;
            tmpObject.GetComponent<SpriteRenderer>().sprite = tmpObject.GetComponent<BoardTile>().TileSPrite;
            var height = tmpObject.GetComponent<BoardTile>().TileHeight;
            var width = tmpObject.GetComponent<BoardTile>().TileWidth;
            
            var xAxisPosition = (boardX * height + boardY * width) / 2f;
            var yAxisPosition = (boardX * height - boardY * width) / 4f;
            tmpObject.transform.position = new Vector2(xAxisPosition, yAxisPosition);
        }
    }
}