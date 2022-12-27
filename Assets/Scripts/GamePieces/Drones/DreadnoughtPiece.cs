using System.Collections.Generic;
using UnityEngine;

namespace GamePieces.Drones
{
    public class DreadnoughtPiece : BasePiece
    {
        protected virtual void Awake()
        {
            this.maxMoveDistance = 1;
            this.maxAttackDistance = 1;
            this.numberOfAttacks = 8;
            this.attackPower = 2;
            this.hitPoints = 5;
            this.isAlive = true;
            this.gameSprite = Resources.Load<Sprite>("Sprites/Drones/Dreadnought");

            this.allowedMoveDirections = new Dictionary<string, bool>
            {
                {"N", true}, {"NE", true}, {"E", true}, {"SE", true},
                {"S", true}, {"SW", true}, {"W", true}, {"NW", true}
            };

            this.allowedAttackDirections = new Dictionary<string, bool>
            {
                {"N", true}, {"NE", true}, {"E", true}, {"SE", true},
                {"S", true}, {"SW", true}, {"W", true}, {"NW", true}
            };

            this.allowedActions = new Dictionary<string, bool>
            {
                {"move", true}, {"attack", true}
            };
        }
    }
}