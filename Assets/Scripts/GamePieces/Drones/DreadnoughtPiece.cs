using System.Collections.Generic;
using Enums;
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
    }
}