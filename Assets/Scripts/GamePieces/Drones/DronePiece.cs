using System.Collections.Generic;
using UnityEngine;

namespace GamePieces.Drones
{
    public class DronePiece : BasePiece
    {
        protected virtual void Awake()
        {
            this.maxMoveDistance = 1;
            this.maxAttackDistance = 7;
            this.numberOfAttacks = 1;
            this.attackPower = 1;
            this.hitPoints = 2;
            this.isAlive = true;
            this.gameSprite = Resources.Load<Sprite>("Sprites/Drones/Drone");

            this.allowedMoveDirections = new Dictionary<string, bool>
            {
                {"N", false}, {"NE", false}, {"E", false}, {"SE", false},
                {"S", true}, {"SW", false}, {"W", false}, {"NW", false}
            };

            this.allowedAttackDirections = new Dictionary<string, bool>
            {
                {"N", false}, {"NE", true}, {"E", false}, {"SE", true},
                {"S", false}, {"SW", true}, {"W", false}, {"NW", true}
            };

            this.allowedActions = new Dictionary<string, bool>
            {
                {"move", true}, {"attack", true}
            };
        }
        
    }
}