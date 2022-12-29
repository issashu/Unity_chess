using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace GamePieces.Humans
{
    public class JumpshipPiece : BasePiece
    {
        protected override void Awake()
        {
            this.maxMoveDistance = 0;
            this.maxAttackDistance = 1;
            this.attackPower = 2;
            this.hitPoints = 2;
            this.isAlive = true;
            this.currentTilePosition = new Point(0, 0);
            this.gameSprite = Resources.Load<Sprite>("Sprites/Humans/Jumpship");
            this.validMovesFromPosition = new List<Point>();
            this.movesXAxis = new [] {1, -1, 2, 2, 1, -1, -2, -2};
            this.movesYAxis = new [] {2, 2, 1, -1, -2, -2, -1, 1};

            this.allowedActions = new Dictionary<string, bool>
            {
                {"move", true}, {"attack", true}
            };
            
            this.boxColliderSettings = new Dictionary<string, float>
            {
                {"offsetX", 0.10f}, {"offsetY", 1.62f},
                {"sizeX", 1.16f},   {"sizeY", 3.64f}
            };
        }
        
        protected override void ListValidMovesFromPosition()
        {
            // Standalone move mechanic
            
            int directions = movesXAxis.Length;

            for (int i = 0; i < directions; i++)
            {
                var newPosition = new Point(this.currentTilePosition.x + movesXAxis[i],
                                            this.currentTilePosition.y + movesYAxis[i]);
                this.validMovesFromPosition.Add(newPosition);
            }
        }
    }
}