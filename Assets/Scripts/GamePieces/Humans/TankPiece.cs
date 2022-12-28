using System.Collections.Generic;
using UnityEngine;

namespace GamePieces.Humans
{
    public class TankPiece : BasePiece
    {
        protected override void Awake()
        {
            this.maxMoveDistance = 3;
            this.maxAttackDistance = 7;
            this.numberOfAttacks = 1;
            this.attackPower = 2;
            this.hitPoints = 4;
            this.isAlive = true;
            this.gameSprite = Resources.Load<Sprite>("Sprites/Humans/Tank");
            
            this.allowedMoveDirections = new Dictionary<string, bool>
            {
                {"N", true}, {"NE", true}, {"E", true}, {"SE", true},
                {"S", true}, {"SW", true}, {"W", true}, {"NW", true}
            };

            this.allowedAttackDirections = new Dictionary<string, bool>
            {
                {"N", true}, {"NE", false}, {"E", true}, {"SE", false},
                {"S", true}, {"SW", false}, {"W", true}, {"NW", false}
            };
            
            this.allowedActions = new Dictionary<string, bool>
            {
                {"move", true}, {"attack", true}
            };
            
            this.boxColliderSettings = new Dictionary<string, float>
            {
                {"offsetX", 0.01f}, {"offsetY", 1.04f},
                {"sizeX", 1.06f},   {"sizeY", 1.82f}
            };
        }
    }
}