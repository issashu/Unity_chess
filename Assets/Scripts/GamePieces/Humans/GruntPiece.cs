using System.Collections.Generic;
using UnityEngine;

namespace GamePieces.Humans
{
    public class GruntPiece : BasePiece
    
    {
        protected override void Awake()
        {
            this.maxMoveDistance = 1;
            this.maxAttackDistance = 8;
            this.attackPower = 1;
            this.hitPoints = 2;
            
            this.allowedDirections = new Dictionary<string, bool>
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