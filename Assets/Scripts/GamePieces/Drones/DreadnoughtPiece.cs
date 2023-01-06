using System.Collections.Generic;
using AI;
using Enums;
using Managers;
using UnityEngine;
using Utils;

namespace GamePieces.Drones
{
    public class DreadnoughtPiece : BasePiece
    {
        /*-----------MEMBERS-------------------*/
        
        
        
        /*-----------METHODS-------------------*/
       
        public override void HighlightThreatenedTiles()
        {
            if (!this.AllowedActions["attack"])
                return;
            
            if (!this.isActive)
                return;
            
            var boardMatrix = GameBoard.GameBoard.Board.GameBoardMatrix;

            ListThreatenedTiles();

            foreach (var point in this.threatenedTilesFromPosition)
            {
                var tile = boardMatrix[point.x, point.y];
                if (!tile.isTileOccupied())
                    continue;
                
                if(PieceManager.arePiecesSameTeam(this, tile.TileOccupant))
                    continue;
                
                tile.ChangeTileColorTint(Color.red);
            }
        }
        
        public override void AttackAction(BasePiece target)
        {
            foreach (var location in this.threatenedTilesFromPosition)
            {
                var tile = ConversionUtils.GetTileAtPoint(location);
                if (!tile.isTileOccupied())
                    continue;
                
                if (!PieceManager.arePiecesSameTeam(this, tile.TileOccupant))
                    tile.TileOccupant.TakeDamage(this.attackPower);
            }
            
            this.allowedActions["attack"] = false;
            this.threatenedTilesFromPosition.Clear();
        }
        
        protected override void Awake()
        {
            this.maxMoveDistance = 1;
            this.maxAttackDistance = 1;
            this.attackPower = 2;
            this.hitPoints = 5;
            this.gameTeam = (int) FactionEnum.Drones;
            this.pieceTypeAndPointsValue = (int) DroneUnits.Dreadnought;
            this.isAlive = true;
            this.isActive = true;
            this.currentTilePosition = new Point(0, 0);
            this.gameSprite = Resources.Load<Sprite>("Sprites/Drones/Dreadnought");
            this.unitAiBehaviourLogic = this.GetComponent<DreadnoughtDecisionLogic>();
            this.currentTilePosition = new Point(0, 0);
            this.validMovesFromPosition = new List<Point>();
            this.threatenedTilesFromPosition = new List<Point>();
            
            // TODO Convert to arrays of Points called with the direction names :)
            this.movesXAxis = new [] { 0, 1, 1, 1, 0, -1,-1,-1};
            this.movesYAxis = new [] { 1, 1, 0,-1,-1, -1, 0, 1};
            
            this.attacksXAxis = new [] {0, 1, 1, 1, 0,-1,-1,-1};
            this.attacksYAxis = new [] {1, 1, 0,-1,-1,-1, 0, 1};
            
            this.allowedActions = new Dictionary<string, bool>
            {
                {"move", true}, {"attack", true}
            };
            
            this.boxColliderSettings = new Dictionary<string, float>
            {
                {"offsetX", 0.07f}, {"offsetY", 1.36f},
                {"sizeX", 1.40f},   {"sizeY", 2.88f}
            };
            
            this.healthDisplay = SetupHealthDisplay(this.HitPoints.ToString());
        }
        
        protected override void ListThreatenedTiles()
        {
            int shootingDirections = this.attacksXAxis.Length;
            var gameBoard = GameBoard.GameBoard.Board;

            for (int direction = 0; direction < shootingDirections; direction++)
            {
                for (int distance = 1; distance <= this.MaxAttackDistance; distance++)
                {
                    var attackTile = new Point(this.currentTilePosition.x + (distance * this.attacksXAxis[direction]),
                        this.currentTilePosition.y + (distance * this.attacksYAxis[direction]));
                    
                    if (!gameBoard.isPointWithinBoardLimits(attackTile))
                        continue;
                    
                    this.threatenedTilesFromPosition.Add(attackTile);
                    break;
                }
            }
        }
        
      
        
        
    }
}