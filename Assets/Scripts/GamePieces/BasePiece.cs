using System.Collections.Generic;
using Defines;
using GameBoard;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace GamePieces
{
    public class BasePiece : MonoBehaviour  {
        [SerializeField] protected int maxMoveDistance;
        [SerializeField] protected int maxAttackDistance;
        [SerializeField] protected int attackPower;
        [SerializeField] protected int hitPoints;
        [SerializeField] protected bool isAlive;
        [SerializeField] protected Sprite gameSprite;
        protected List<Point> validMovesFromPosition;
        protected Point currentTilePosition;
        protected Dictionary<string, float> boxColliderSettings; // check to make named tuple C#7
        protected Dictionary<string, bool> allowedActions;
        protected int [] movesXAxis;
        protected int [] movesYAxis;

        protected virtual void Awake()
        {
            this.maxMoveDistance = 0;
            this.maxAttackDistance = 0;
            this.attackPower = 0;
            this.hitPoints = 0;
            this.isAlive = false;
            this.currentTilePosition = new Point(0, 0);
            this.validMovesFromPosition = new List<Point>();
            this.movesXAxis = new [] {0, 1, 1, 1, 0,-1,-1,-1};
            this.movesYAxis = new [] {1, 1, 0,-1,-1,-1, 0, 1};

            this.allowedActions = new Dictionary<string, bool>
            {
                {"move", true}, {"attack", true}
            };

            this.boxColliderSettings = new Dictionary<string, float>
            {
                {"offsetX", 0.00f}, {"offsetY", 0.0f},
                {"sizeX", 0.0f}, {"sizeY", 0.0f}
            };
        }

        public virtual void SetCurrentPosition(int x, int y)
        {
            this.currentTilePosition.x = x;
            this.currentTilePosition.y = y;
        }
        
        public virtual void HighlightMovePath()
        // TODO Make it a list with valid move tiles. Start going in each of the directions and check up to movement distance. Add all valid to a list
        // Second step will be to check if there aren't enemies there. Remove ones with enemies. After highlight legal only
        {
            var gameBoard = GameObject.Find("GameBoard");
            var boardMatrix = gameBoard.GetComponent<GameBoard.GameBoard>().BoardMatrix;

            ListValidMovesFromPosition();
            
            foreach (var point in this.validMovesFromPosition)
            {
                if (point.x < 0 || point.y < 0 || point.x >= GameBoardConstants.BOARD_HEIGHT || point.y >= GameBoardConstants.BOARD_WIDTH) 
                    continue;
                    
                var tile = boardMatrix[point.x, point.y].GetComponent<BoardTile>();
                tile.ChangeTileColorTint(Color.green);
            }
        }
        
        protected virtual void MovePiece(Vector2 targetLocation)
        {
            var chosenTile = GameObject.Find("Tile: " + targetLocation.x + " " + targetLocation.y);
            this.SetCurrentPosition(Mathf.RoundToInt(targetLocation.x), Mathf.RoundToInt(targetLocation.y));
            this.transform.position = chosenTile.transform.position;
        }
        
        public virtual void AttackAction()
        {
            
        }

        public virtual void OnDeath()
        {
            
        }

        protected virtual void ListValidMovesFromPosition()
        {
            int directions = this.movesXAxis.Length;

            for (int direction = 0; direction < directions; direction++)
            {
                for (int distance = 1; distance <= this.MaxMoveDistance; distance++)
                {
                    var newPosition = new Point(this.currentTilePosition.x + (distance * this.movesXAxis[direction]),
                        this.currentTilePosition.y + (distance * this.movesYAxis[direction]));
                
                    this.validMovesFromPosition.Add(newPosition);
                }
            }
        }
        
        /*-----------PUBLIC-------------*/
        public int MaxMoveDistance => maxMoveDistance;
        public int MaxAttackDistance => maxAttackDistance;
        public int HitPoints => hitPoints;
        public bool IsUnitAlive => isAlive;
        public Sprite UnitSprite => gameSprite;
        public Dictionary<string, bool> AllowedActions => allowedActions;
        public Dictionary<string, float> BoxColliderSettings => boxColliderSettings;
        public void MoveAction(Vector2 targetLocation) => MovePiece(targetLocation);
    }
}


