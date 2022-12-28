using System.Collections.Generic;
using UnityEngine;

namespace GamePieces.Humans
{
    public class GruntPiece : BasePiece
    
    {
        protected override void Awake()
        {
            this.maxMoveDistance = 1;
            this.maxAttackDistance = 7;
            this.numberOfAttacks = 1;
            this.attackPower = 1;
            this.hitPoints = 2;
            this.isAlive = true;
            this.gameSprite = Resources.Load<Sprite>("Sprites/Humans/Grunt");
            
            this.allowedMoveDirections = new Dictionary<string, bool>
            {
                {"N", true}, {"NE", false}, {"E", true}, {"SE", false},
                {"S", true}, {"SW", false}, {"W", true}, {"NW", false},
            };
            
            this.allowedAttackDirections = new Dictionary<string, bool>
            {
                {"N", false}, {"NE", true}, {"E", false}, {"SE", true},
                {"S", false}, {"SW", true}, {"W", false}, {"NW", true},
            };
            
            this.allowedActions = new Dictionary<string, bool>
            {
                {"move", true}, {"attack", true}
            };
            
            this.boxColliderSettings = new Dictionary<string, float>
            {
                {"offsetX", 0.06f}, {"offsetY", 0.81f},
                {"sizeX", 0.55f},   {"sizeY", 1.57f}
            };
        }
    }
}