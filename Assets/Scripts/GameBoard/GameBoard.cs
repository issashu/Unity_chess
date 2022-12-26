using Unity.VisualScripting;
using UnityEngine;

namespace GameBoard
{
    public class GameBoard: MonoBehaviour
    {
        [SerializeField]private int _boardHeight;
        [SerializeField]private int _boardWidth;
        private GameObject[,] _boardMatrix;

        public ref GameObject[,] BoardMatrix
        {
            get
            {
                return ref _boardMatrix;
            }
        }

        private void Awake()
        {
            this._boardHeight = 8;
            this._boardWidth = 8;
            this._boardMatrix = new GameObject[this._boardHeight, this._boardWidth];
            
            var tmp = new GameObject("Tile");
            tmp.AddComponent<BoardTile>();
            
            for (var i = 0; i < this._boardHeight; i++)
            {
                for (var j = 0; j < this._boardWidth; j++)
                {
                    this._boardMatrix[i, j] = Instantiate(tmp, transform);
                    this._boardMatrix[i, j].name = "Tile: " + i + " " + j;
                }
            }
            
            Destroy(tmp);
        }

    }
}