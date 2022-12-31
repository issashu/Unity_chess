using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace GamePieces.Drones
{
    public class DronePiece : BasePiece
    {
        protected override void Awake()
        {
            this.maxMoveDistance = 1;
            this.maxAttackDistance = 7;
            this.attackPower = 1;
            this.hitPoints = 2;
            this.isAlive = true;
            this.gameSprite = Resources.Load<Sprite>("Sprites/Drones/Drone");
            this.currentTilePosition = new Point(0, 0);
            this.validMovesFromPosition = new List<Point>();
            this.movesXAxis = new [] {-1};
            this.movesYAxis = new [] { 0};

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