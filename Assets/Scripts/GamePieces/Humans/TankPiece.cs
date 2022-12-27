using System.Collections.Generic;
using UnityEngine;

namespace GamePieces.Humans
{
    public class TankPiece : BasePiece
    {
        protected override void Awake()
        {
            this.maxMoveDistance = 3;
            this.maxAttackDistance = 8;
            this.attackPower = 2;
            this.hitPoints = 4;
            
            this.allowedDirections = new Dictionary<string, bool>
            {
                {"N", true}, {"NE", true}, {"E", true}, {"SE", true},
                {"S", true}, {"SW", true}, {"W", true}, {"NW", true},
            };
            
            this.allowedActions = new Dictionary<string, bool>
            {
                {"move", true}, {"attack", true}
            };
        }
    }
}