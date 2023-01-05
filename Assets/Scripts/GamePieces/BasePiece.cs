using System;
using System.Collections.Generic;
using AI;
using Enums;
using GameBoard;
using UnityEngine;
using Utils;

namespace GamePieces
{
    public class BasePiece : MonoBehaviour  
    {
        
        protected int maxMoveDistance;
        protected int maxAttackDistance;
        protected int attackPower;
        protected int hitPoints;
        protected int gameTeam;
        protected int pieceTypeAndPointsValue;
        protected bool isAlive;
        protected bool isActive;
        protected Sprite gameSprite;
        protected AIDecisionLogic unitAiBehaviourLogic;
        protected List<Point> validMovesFromPosition;
        protected List<Point> threatenedTilesFromPosition;
        protected Point currentTilePosition;
        protected Dictionary<string, float> boxColliderSettings; 
        protected Dictionary<string, bool> allowedActions;
        protected int [] movesXAxis;
        protected int [] movesYAxis;
        protected int[] attacksXAxis;
        protected int[] attacksYAxis;

        public static event EventHandler<BasePiece> OnHealthZero;

        protected virtual void Awake()
        {
            this.maxMoveDistance = 0;
            this.maxAttackDistance = 0;
            this.attackPower = 0;
            this.hitPoints = 0;
            this.gameTeam = (int) FactionEnum.None;
            this.pieceTypeAndPointsValue = (int) HumanUnits.None;
            this.isAlive = false;
            this.isActive = false;
            this.currentTilePosition = new Point(0, 0);
            this.validMovesFromPosition = new List<Point>();
            this.threatenedTilesFromPosition = new List<Point>();
            this.movesXAxis = new [] {0, 1, 1, 1, 0,-1,-1,-1};
            this.movesYAxis = new [] {1, 1, 0,-1,-1,-1, 0, 1};
            
            this.attacksXAxis = new [] {0, 1, 1, 1, 0,-1,-1,-1};
            this.attacksYAxis = new [] {1, 1, 0,-1,-1,-1, 0, 1};

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

        protected virtual void Update()
        {
            if (this.hitPoints == 0)
            {
                OnHealthZero?.Invoke(this, this);
            }

            if (this.allowedActions["move"] == false && this.allowedActions["attack"] == false)
            {
                this.DeactivatePiece();
            }
        }

        public virtual void SetCurrentPosition(int x, int y)
        {
            this.currentTilePosition.x = x;
            this.currentTilePosition.y = y;
        }
        
        // TODO Do rethink to extract the validations in a separate class, so that pieces do not have access to board more then necessary. See UML
        public virtual void HighlightMovePath()
        {
            if (!this.AllowedActions["move"])
                return;

            if (!this.isActive)
                return;
            
            var gameBoard = GameBoard.GameBoard.Board;
            var boardMatrix = gameBoard.GameBoardMatrix;

            ListPossibleMovesFromPosition();
            
            // TODO: Change foreach loop into simple for loop, since it should cost less resources
            foreach (var point in this.validMovesFromPosition)
            {
                var tile = boardMatrix[point.x, point.y];
                tile.ChangeTileColorTint(Color.green);
            }
        }

        public virtual void HighlightThreatenedTiles()
        {
            // TODO: Extract to Piece Manager and unify the two highlights. Code is almost the same except color...
            if (!this.AllowedActions["attack"])
                return;
            
            if (!this.isActive)
                return;
            
            var gameBoard = GameBoard.GameBoard.Board;
            var boardMatrix = gameBoard.GameBoardMatrix;

            ListThreatenedTiles();

            foreach (var point in this.threatenedTilesFromPosition)
            {
                var tile = boardMatrix[point.x, point.y];
                tile.ChangeTileColorTint(Color.red);
            }
        }
        
        /*-----------MOVEMENT LOGIC----------*/
        protected virtual void ListPossibleMovesFromPosition()
        {
            int directions = this.movesXAxis.Length;
            var gameBoard = GameBoard.GameBoard.Board;
            var boardMatrix = gameBoard.GameBoardMatrix;

            for (int direction = 0; direction < directions; direction++)
            {
                for (int distance = 1; distance <= this.MaxMoveDistance; distance++)
                {
                    var newPosition = new Point(this.currentTilePosition.x + (distance * this.movesXAxis[direction]),
                        this.currentTilePosition.y + (distance * this.movesYAxis[direction]));
                    if (!gameBoard.isPointWithinBoardLimits(newPosition))
                        continue;

                    var tile = boardMatrix[newPosition.x, newPosition.y];
                    if (tile.isTileOccupied())
                        break;

                    //Add valid move tile, only if no piece is there.
                    this.validMovesFromPosition.Add(newPosition);
                }
            }
        }
        /**
         * <summary>Method to move the game piece to another location on the board. Returns true on success and false
         * on failure</summary>
         */
        public virtual void PreciseMoveAction(BoardTile startLocation, BoardTile targetLocation)
        {
            if(!startLocation || !targetLocation)
                return;
            
            var moveLocationPoint = ConversionUtils.CreatePointObjectFromTile(targetLocation);
            
            if (!this.validMovesFromPosition.Contains(moveLocationPoint))
                return;
            
            /*var moveLocationVector = ConversionUtils.WorldPositionFromCoordinates(targetLocation.XCoordinate, targetLocation.YCoordinate);
            this.SetCurrentPosition(Mathf.RoundToInt(moveLocationVector.x), Mathf.RoundToInt(moveLocationVector.y));*/
            
            this.MovePiece(startLocation, targetLocation);
        }
        
        public virtual void GenericDirectionalMoveAction(Point moveLocationPoint)
        {
            var directionOfMove = MiscUtils.DirectionBetweenPoints(this.CurrentPieceCoordinates, moveLocationPoint);
            var unitStepDirection = MiscUtils.NormalizeDirection(directionOfMove);
            
            int targetPointX = this.CurrentPieceCoordinates.x + (unitStepDirection.x * this.maxMoveDistance);
            int targetPointY = this.CurrentPieceCoordinates.y + (unitStepDirection.y * this.maxMoveDistance);

            var startTile = ConversionUtils.GetTileAtPoint(this.CurrentPieceCoordinates);
            var targetTile = ConversionUtils.GetTileAtCoordinates(targetPointX, targetPointY);
            
            this.MovePiece(startTile, targetTile);
        }

        private void MovePiece(BoardTile startTile, BoardTile targetTile)
        {
            this.transform.position = targetTile.transform.position;
            
            var moveLocationVector = ConversionUtils.WorldPositionFromCoordinates(targetTile.XCoordinate, targetTile.YCoordinate);
            this.SetCurrentPosition(Mathf.RoundToInt(moveLocationVector.x), Mathf.RoundToInt(moveLocationVector.y));
            
            targetTile.SetOccupant(this);
            startTile.ClearOccupant();
            
            this.allowedActions["move"] = false;
            this.validMovesFromPosition.Clear();
            this.threatenedTilesFromPosition.Clear();
        }
        
        /*-----------ATTACK LOGIC----------*/
        protected virtual void ListThreatenedTiles()
        {
            // TODO: Extract actions to a separate class to avoid the repetition in move and attack.
            int shootingDirections = this.attacksXAxis.Length;
            var gameBoard = GameBoard.GameBoard.Board;
            var boardMatrix = gameBoard.GameBoardMatrix;

            for (int direction = 0; direction < shootingDirections; direction++)
            {
                for (int distance = 1; distance <= this.MaxAttackDistance; distance++)
                {
                    var attackTile = new Point(this.currentTilePosition.x + (distance * this.attacksXAxis[direction]),
                        this.currentTilePosition.y + (distance * this.attacksYAxis[direction]));
                    
                    if (!gameBoard.isPointWithinBoardLimits(attackTile))
                        continue;

                    var tile = boardMatrix[attackTile.x, attackTile.y];
                    if (!tile.isTileOccupied())
                        continue;
                    // TODO Add here logic not to continue targeting beyond first unit 

                    if (tile.TileOccupant.GetComponent<BasePiece>().PieceFaction == this.gameTeam) 
                        //TODO We have a method to compare teams. use it!
                    {
                        continue;
                    }
                    this.threatenedTilesFromPosition.Add(attackTile);
                    break; // We stop in any given direction after we hit first unit. Can be overriden for say..piercing bullets
                }
            }
        }
        public virtual void AttackAction(BasePiece target)
        {
            if (!this.threatenedTilesFromPosition.Contains(target.currentTilePosition))
                return;

            target.TakeDamage(this.attackPower);
            this.allowedActions["attack"] = false;
            this.threatenedTilesFromPosition.Clear();
        }

        public virtual void TakeDamage(int damage)
        {
            this.hitPoints -= damage;
            this.ValidateHealth();
        }

        protected virtual void ValidateHealth()
        {
            if (this.hitPoints < 0)
            {
                this.hitPoints = 0;
            }
        }

        public virtual void OnDestroy()
        {
            this.isAlive = false;
        }
        
        
        /*------MISC UTILITY METHODS------*/
        public virtual void ChangePieceColor(Color newColor)
        {
            this.PieceSpriteRenderer.material.color = newColor;
        }

        public virtual void DeactivatePiece()
        {
            // TODO Move to Piece Manager
            this.isActive = false;
            ChangePieceColor(Color.grey);
        }
        
        public virtual void ActivatePiece()
        {
            this.isActive = true;
            ChangePieceColor(Color.white);
        }

        public virtual void ResetPieceActions()
        {
            this.allowedActions["move"] = true;
            this.allowedActions["attack"] = true;
        }
        
        
        /*-----------PROPERTIES / GETTERS & SETTERS-------------*/
        public bool IsPieceActive
        {
            get => this.isActive;
            set => this.isActive = value;
        }
        public int MaxMoveDistance => maxMoveDistance;
        public int MaxAttackDistance => maxAttackDistance;
        public int HitPoints => hitPoints;
        public int DamageDone => attackPower;
        public bool IsPieceAlive => isAlive;
        public int PieceFaction => gameTeam;
        public int PieceTypeAndPointsValue => pieceTypeAndPointsValue;
        public Point CurrentPieceCoordinates => this.currentTilePosition;
        public SpriteRenderer PieceSpriteRenderer => this.GetComponent<SpriteRenderer>();
        public Sprite UnitSprite => gameSprite;
        public AIDecisionLogic UnitAiBehaviourLogic
        {
            get => this.unitAiBehaviourLogic;
            set => this.unitAiBehaviourLogic = value;
        }
        public Dictionary<string, bool> AllowedActions => allowedActions;
        public Dictionary<string, float> BoxColliderSettings => boxColliderSettings;
        public List<Point> AllowedMovesList => this.validMovesFromPosition;
        public List<Point> ThreatenedTiles => this.threatenedTilesFromPosition;

        public void ListAllThreatenedTiles() => ListThreatenedTiles();
        public void ListAllMoveTiles() => ListPossibleMovesFromPosition();
    }
}


