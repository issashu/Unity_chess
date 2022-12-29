using System.Collections.Generic;
using Defines;
using GameBoard;
using UnityEngine;
using Utils;

namespace GamePieces
{
    public class BasePiece : MonoBehaviour  {
        [SerializeField] protected int maxMoveDistance;
        [SerializeField] protected int maxAttackDistance;
        [SerializeField] protected int numberOfAttacks;
        [SerializeField] protected int attackPower;
        [SerializeField] protected int hitPoints;
        [SerializeField] protected bool isAlive;
        [SerializeField] protected Sprite gameSprite;
        protected Point currentTilePosition;
        protected Dictionary<string, float> boxColliderSettings;
        protected Dictionary<string, bool> allowedMoveDirections;
        protected Dictionary<string, bool> allowedAttackDirections;
        protected Dictionary<string, bool> allowedActions;

        protected virtual void Awake()
        {
            this.maxMoveDistance = 0;
            this.maxAttackDistance = 0;
            this.numberOfAttacks = 0;
            this.attackPower = 0;
            this.hitPoints = 0;
            this.isAlive = false;
            this.currentTilePosition = new Point(0, 0);

            this.allowedMoveDirections = new Dictionary<string, bool>
            {
                {"N", true}, {"NE", true}, {"E", true}, {"SE", true},
                {"S", true}, {"SW", true}, {"W", true}, {"NW", true}
            };

            this.allowedAttackDirections = new Dictionary<string, bool>
            {
                {"N", true}, {"NE", true}, {"E", true}, {"SE", true},
                {"S", true}, {"SW", true}, {"W", true}, {"NW", true}
            };

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

            for (var x = -this.maxMoveDistance; x <= this.maxMoveDistance; x++)
            {
                for (var y = -this.maxMoveDistance; y <= this.maxMoveDistance; y++)
                {
                    var row = this.currentTilePosition.x + x;
                    var col = this.currentTilePosition.y + y;

                    if (row < 0 || col < 0 || row > GameBoardConstants.BOARD_HEIGHT || col > GameBoardConstants.BOARD_WIDTH) 
                        continue;
                    
                    var tile = boardMatrix[row, col].GetComponent<BoardTile>();
                    tile.ChangeTileColorTint(Color.green);
                }
            }
        }
        
        // TODO: Currently unit teleports to new location. Make it slide there.
        protected virtual void MovePiece(Vector2 targetLocation)
        {
            var chosenTile = GameObject.Find("Tile: " + targetLocation.x + " " + targetLocation.y);
            this.transform.position = chosenTile.transform.position;
        }
        
        public virtual void AttackAction()
        {
            
        }

        public virtual void OnDeath()
        {
            
        }
        
        /*-----------PUBLIC-------------*/

        public int MaxMoveDistance => maxMoveDistance;
        public int MaxAttackDistance => maxAttackDistance;
        public int HitPoints => hitPoints;
        public bool IsUnitAlive => isAlive;
        public Sprite UnitSprite => gameSprite;
        public Dictionary<string, bool> AllowedMoveDirections => allowedMoveDirections;
        public Dictionary<string, bool> AllowedAttackDirection => allowedAttackDirections;
        public Dictionary<string, bool> AllowedActions => allowedActions;
        public Dictionary<string, float> BoxColliderSettings => boxColliderSettings;
        public void MoveAction(Vector2 targetLocation) => MovePiece(targetLocation);
    }
}


