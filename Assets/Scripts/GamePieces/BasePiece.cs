using System;
using System.Collections.Generic;
using AI;
using Defines;
using Enums;
using GameBoard;
using Managers;
using UnityEngine;
using Utils;

namespace GamePieces
{
    public class BasePiece : MonoBehaviour  
    {
        /*---------MEMBERS---------------*/
        protected int maxMoveDistance;
        protected int maxAttackDistance;
        protected int attackPower;
        protected int hitPoints;
        protected int gameTeam;
        protected int pieceTypeAndPointsValue;
        protected bool isAlive;
        protected bool isActive;
        protected int [] movesXAxis;
        protected int [] movesYAxis;
        protected int[] attacksXAxis;
        protected int[] attacksYAxis;
        protected Sprite gameSprite;
        protected GameObject healthDisplayObject;
        protected HealthText healthDisplay;
        protected AIDecisionLogic unitAiBehaviourLogic;
        protected List<Point> validMovesFromPosition;
        protected List<Point> threatenedTilesFromPosition;
        protected Point currentTilePosition;
        protected Dictionary<string, float> boxColliderSettings; 
        protected Dictionary<string, bool> allowedActions;

        /*---------EVENTS---------------*/
        public static event EventHandler<BasePiece> OnHealthZero;
        public static event EventHandler<String> OnDamageTaken; 
        
        
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
        
        
        /*-----------METHODS------------*/
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
            
            this.healthDisplay = SetupHealthDisplay(this.HitPoints.ToString());
            this.healthDisplay.GetComponent<HealthText>().UpdateTextValue(this, this.CurrentPieceCoordinates.ToString());
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
        
        
        /*-----------MOVEMENT LOGIC----------*/
        public virtual void SetCurrentPosition(int x, int y)
        {
            this.currentTilePosition.x = x;
            this.currentTilePosition.y = y;
        }
        
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
        
        public virtual void PreciseMoveAction(BoardTile startLocation, BoardTile targetLocation)
        {
            if(!startLocation || !targetLocation)
                return;
            
            var moveLocationPoint = ConversionUtils.CreatePointObjectFromTile(targetLocation);
            
            if (!this.validMovesFromPosition.Contains(moveLocationPoint))
                return;

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
        public virtual void HighlightThreatenedTiles()
        {
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
        
        public virtual void AttackAction(BasePiece target)
        {
            if (!this.threatenedTilesFromPosition.Contains(target.currentTilePosition))
                return;

            target.TakeDamage(this.attackPower);
            this.allowedActions["attack"] = false;
            this.threatenedTilesFromPosition.Clear();
        }
        
        public virtual void OnDestroy()
        {
            this.isAlive = false;
        }
        
        public virtual void TakeDamage(int damage)
        {
            this.hitPoints -= damage;
            this.ValidateHealth();
            OnDamageTaken?.Invoke(this, this.hitPoints.ToString());
        }
        
        protected virtual void ListThreatenedTiles()
        {
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

                    if (!PieceManager.arePiecesSameTeam(tile.TileOccupant.GetComponent<BasePiece>(), this))
                        this.threatenedTilesFromPosition.Add(attackTile);
                    
                    break;  // We stop in any given direction after we hit first unit. Can be overriden for say..piercing bullets
                }
            }
        }
        
        protected virtual void ValidateHealth()
        {
            if (this.hitPoints < 0)
            {
                this.hitPoints = 0;
            }
        }

        /*------MISC UTILITY METHODS------*/
        public virtual void ChangePieceColor(Color newColor)
        {
            this.PieceSpriteRenderer.material.color = newColor;
        }

        public virtual void DeactivatePiece()
        {
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

        protected HealthText SetupHealthDisplay(string startValue)
        {
            // TODO The health event is subscribed by all units and all pieces update with same value instead of each their own...
            healthDisplayObject = new GameObject("Health Display");
            healthDisplayObject.AddComponent<HealthText>();
            healthDisplayObject.GetComponent<MeshRenderer>().sortingLayerName = GameBoardConstants.HP_TEXT_LAYER;
            healthDisplayObject.transform.position = new Vector3(0, 2.97f, 0);
            healthDisplayObject.transform.SetParent(transform, worldPositionStays: false);
    
            return healthDisplayObject.GetComponent<HealthText>();
        }
    }
}


