using System.Collections.Generic;
using UnityEngine;

namespace GamePieces.Humans
{
    public class JumpshipPiece : BasePiece
    {
        protected override void Awake()
        {
            this.maxMoveDistance = 4;
            this.maxAttackDistance = 1;
            this.numberOfAttacks = 4;
            this.attackPower = 2;
            this.hitPoints = 2;
            this.isAlive = true;
            this.gameSprite = Resources.Load<Sprite>("Sprites/Humans/Jumpship");
            
            this.allowedMoveDirections = new Dictionary<string, bool>
            {
                {"N", true}, {"NE", false}, {"E", true}, {"SE", false},
                {"S", true}, {"SW", false}, {"W", true}, {"NW", false},
            };
            
            this.allowedAttackDirections = new Dictionary<string, bool>
            {
                {"N", true}, {"NE", false}, {"E", true}, {"SE", false},
                {"S", true}, {"SW", false}, {"W", true}, {"NW", false},
            };
            
            this.allowedActions = new Dictionary<string, bool>
            {
                {"move", true}, {"attack", true}
            };
        }
    }
}