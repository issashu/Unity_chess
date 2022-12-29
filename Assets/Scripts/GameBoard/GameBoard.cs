using Defines;
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
            this._boardHeight = Defines.GameBoardConstants.BOARD_HEIGHT;
            this._boardWidth = Defines.GameBoardConstants.BOARD_WIDTH;
            this._boardMatrix = new GameObject[this._boardHeight, this._boardWidth];
            
            var tmp = new GameObject("Tile");
            tmp.AddComponent<BoardTile>();
            tmp.AddComponent<SpriteRenderer>();
            tmp.AddComponent<BoxCollider2D>();
            
            for (var x = 0; x < this._boardHeight; x++)
            {
                for (var y = 0; y < this._boardWidth; y++)
                {
                    SetupTileObject(x, y, ref tmp);
                }
            }
            
            Destroy(tmp);
        }

        private void SetupTileObject(int boardX, int boardY, ref GameObject objectToFill)
        {
            ref var tmpObject = ref this._boardMatrix[boardX, boardY];
            
            tmpObject = Instantiate(objectToFill, transform);
            tmpObject.name = "Tile: " + boardX + " " + boardY;
            tmpObject.GetComponent<SpriteRenderer>().sprite = tmpObject.GetComponent<BoardTile>().TileSprite;
            tmpObject.GetComponent<SpriteRenderer>().sortingLayerName = GameBoardConstants.BOARD_TILES_LAYER;
            tmpObject.GetComponent<Collider2D>().isTrigger = true;
            tmpObject.GetComponent<BoxCollider2D>().size = new Vector2(1.21f, 0.78f);
            
            var height = tmpObject.GetComponent<BoardTile>().TileHeight;
            var width = tmpObject.GetComponent<BoardTile>().TileWidth;
            var xAxisPosition = (boardX * height + boardY * width) / 2f;
            var yAxisPosition = (boardX * height - boardY * width) / 4f;
            tmpObject.transform.position = new Vector2(xAxisPosition, yAxisPosition);
            tmpObject.GetComponent<BoardTile>().UpdateTileGridPosition(boardX, boardY);

            Debug.Log($"Tile with coords: X:{tmpObject.GetComponent<BoardTile>().XCoordinate}; " +
                      $"Y: {tmpObject.GetComponent<BoardTile>().YCoordinate}");
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
    }
}