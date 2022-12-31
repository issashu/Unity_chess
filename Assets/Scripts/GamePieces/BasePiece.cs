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
        // TODO Do rethink to extract the validations in a separate class, so that pieces do not have access to board more then necessary. See UML
        {
            var gameBoard = GameObject.Find("GameBoard");
            var boardMatrix = gameBoard.GetComponent<GameBoard.GameBoard>().BoardMatrix;
            
            ListPossibleMovesFromPosition();

            foreach (var point in this.validMovesFromPosition)
            {
                var tile = boardMatrix[point.x, point.y].GetComponent<BoardTile>();
                tile.ChangeTileColorTint(Color.green);
            }
        }
        
        /**
         * <summary>Method to move the game piece to another location on the board. Returns true on success and false
         * on failure</summary>
         */
        protected virtual void MovePiece(BoardTile targetLocation)
        {
            // TODO: Rethink logic to get either a point and see, if it is allowed square to move or do that check before move action
            // If we pass nothing, just bail out
            var moveLocationPointStruct = ConversionUtils.CreatePointObjectFromTile(targetLocation);
            if (!this.validMovesFromPosition.Contains(moveLocationPointStruct))
                return;
            
            var moveLocationVector =
                ConversionUtils.WorldPositionFromCoordinates(targetLocation.XCoordinate,
                    targetLocation.YCoordinate);
            // TODO: Do check if we really need the SetCurrentPosition
            this.SetCurrentPosition(Mathf.RoundToInt(moveLocationVector.x), Mathf.RoundToInt(moveLocationVector.y));
            // TODO: Add logic for clearing previous positions
            this.transform.position = targetLocation.transform.position;
            targetLocation.SetOccupant(this.GameObject());
            this.validMovesFromPosition.Clear();
        }
        
        public virtual void AttackAction()
        {
            
        }

        public virtual void OnDeath()
        {
            
        }

        protected virtual void ListPossibleMovesFromPosition()
        {
            int directions = this.movesXAxis.Length;
            var gameBoard = GameObject.Find("GameBoard");
            var boardMatrix = gameBoard.GetComponent<GameBoard.GameBoard>().BoardMatrix;

            for (int direction = 0; direction < directions; direction++)
            {
                for (int distance = 1; distance <= this.MaxMoveDistance; distance++)
                {
                    var newPosition = new Point(this.currentTilePosition.x + (distance * this.movesXAxis[direction]),
                        this.currentTilePosition.y + (distance * this.movesYAxis[direction]));
                    if (!gameBoard.GetComponent<GameBoard.GameBoard>().isPointWithinBoardLimits(newPosition))
                    {
                        continue;
                    }

                    var tile = boardMatrix[newPosition.x, newPosition.y];
                    
                    if (!tile.GetComponent<BoardTile>().isTileOccupied())
                    {
                        //Add valid move tile, only if no piece is there.
                        this.validMovesFromPosition.Add(newPosition);
                    }
                }
            }
        }

        /*-----------PUBLIC-------------*/
        public int MaxMoveDistance => maxMoveDistance;
        public int MaxAttackDistance => maxAttackDistance;
        public int HitPoints => hitPoints;
        public bool IsUnitAlive => isAlive;
        public Point CurrentPieceCoordinates => this.currentTilePosition;
        public Sprite UnitSprite => gameSprite;
        public Dictionary<string, bool> AllowedActions => allowedActions;
        public Dictionary<string, float> BoxColliderSettings => boxColliderSettings;
        public void MoveAction(BoardTile targetLocation) => MovePiece(targetLocation);
    }
}


