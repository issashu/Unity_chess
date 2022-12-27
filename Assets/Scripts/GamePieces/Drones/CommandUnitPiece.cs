using System.Collections.Generic;
using UnityEngine;

namespace GamePieces.Drones
{
    public class CommandUnitPiece : BasePiece
    {
        protected virtual void Awake()
        {
            this.maxMoveDistance = 1;
            this.hitPoints = 5;
            this.isAlive = true;
            this.gameSprite = Resources.Load<Sprite>("Sprites/Drones/CommandUnit");
                
            this.allowedMoveDirections = new Dictionary<string, bool>
            {
                {"N", false}, {"NE", false}, {"E", true}, {"SE", false},
                {"S", false}, {"SW", false}, {"W", true}, {"NW", false}
            };

            this.allowedActions = new Dictionary<string, bool>
            {
                {"move", true}, {"attack", false}
            };
        }
        
    }
}