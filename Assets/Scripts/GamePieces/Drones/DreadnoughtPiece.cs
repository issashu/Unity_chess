using System.Collections.Generic;
using Enums;
using Managers;
using UnityEngine;
using Utils;

namespace GamePieces.Drones
{
    public class DreadnoughtPiece : BasePiece
    {
        protected override void Awake()
        {
            this.maxMoveDistance = 1;
            this.maxAttackDistance = 1;
            this.attackPower = 2;
            this.hitPoints = 5;
            this.gameTeam = (int) FactionEnum.Drones;
            this.isAlive = true;
            this.isActive = true;
            this.currentTilePosition = new Point(0, 0);
            this.gameSprite = Resources.Load<Sprite>("Sprites/Drones/Dreadnought");
            this.currentTilePosition = new Point(0, 0);
            this.validMovesFromPosition = new List<Point>();
            this.threatenedTilesFromPosition = new List<Point>();

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
        }
        
        protected override void ListThreatenedTiles()
        {
            // TODO: Extract actions to a separate class to avoid the repetition in move and attack.
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
        
        public override void HighlightThreatenedTiles()
        {
            if (!this.AllowedActions["attack"])
                return;
            
            if (!this.isActive)
                return;
            
            // TODO: Extract to Piece Manager and unify the two highlights. Code is almost the same except color...
            var boardMatrix = GameBoard.GameBoard.Board.GameBoardMatrix;

            ListThreatenedTiles();

            foreach (var point in this.threatenedTilesFromPosition)
            {
                var tile = boardMatrix[point.x, point.y];
                if (!tile.isTileOccupied())
                    continue;
                
                var targetUnit = tile.TileOccupant.GetComponent<BasePiece>();
                if(PieceManager.arePiecesSameTeam(this, targetUnit))
                    continue;
                
                tile.ChangeTileColorTint(Color.red);
            }
        }
        
        public override void AttackAction(BasePiece target, int damageDone)
        {
            foreach (var location in this.threatenedTilesFromPosition)
            {
                var tile = ConversionUtils.GetTileAtPoint(location);
                if (!tile.isTileOccupied())
                    continue;
                
                var targetUnit = tile.TileOccupant.GetComponent<BasePiece>();
                if (!PieceManager.arePiecesSameTeam(this, targetUnit))
                    targetUnit.TakeDamage(damageDone);
            }
            
            this.allowedActions["attack"] = false;
            this.threatenedTilesFromPosition.Clear();
        }
        
        
    }
}