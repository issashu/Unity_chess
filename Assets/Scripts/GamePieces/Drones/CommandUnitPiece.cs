using System.Collections.Generic;
using Enums;
using UnityEngine;
using Utils;

namespace GamePieces.Drones
{
    public class CommandUnitPiece : BasePiece
    {
        protected override void Awake()
        {
            this.maxMoveDistance = 1;
            this.maxAttackDistance = 0;
            this.attackPower = 0;
            this.hitPoints = 5;
            this.gameTeam = (int) FactionEnum.Drones;
            this.isAlive = true;
            this.currentTilePosition = new Point(0, 0);
            this.gameSprite = Resources.Load<Sprite>("Sprites/Drones/CommandUnit");
            this.currentTilePosition = new Point(0, 0);
            this.validMovesFromPosition = new List<Point>();
            this.threatenedTilesFromPosition = new List<Point>();

            this.movesXAxis = new [] {0, 0};
            this.movesYAxis = new [] {1,-1};
            
            this.attacksXAxis = new [] {0};
            this.attacksYAxis = new [] {0};

            this.allowedActions = new Dictionary<string, bool>
            {
                {"move", true}, {"attack", true}
            };
            
            this.boxColliderSettings = new Dictionary<string, float>
            {
                {"offsetX", 0.13f}, {"offsetY", 1.15f},
                {"sizeX", 1.16f},   {"sizeY", 2.34f}
            };
        }
    }
}