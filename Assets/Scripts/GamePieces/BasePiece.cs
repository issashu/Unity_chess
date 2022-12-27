using System.Collections.Generic;
using UnityEngine;

namespace GamePieces
{
    public class BasePiece : MonoBehaviour
    {
        [SerializeField] protected int maxMoveDistance;
        [SerializeField] protected int maxAttackDistance;
        [SerializeField] protected int attackPower;
        [SerializeField] protected int hitPoints;
        [SerializeField] protected Sprite gameSprite;
        protected Dictionary<string, bool> allowedDirections;
        protected Dictionary<string, bool> allowedActions;

        protected virtual void Awake()
        {
            this.maxMoveDistance = 0;
            this.maxAttackDistance = 0;
            this.attackPower = 0;
            this.hitPoints = 0;

            this.allowedDirections = new Dictionary<string, bool>
            {
                {"N", true}, {"NE", true}, {"E", true}, {"SE", true},
                {"S", true}, {"SW", true}, {"W", true}, {"NW", true}
            };

            this.allowedActions = new Dictionary<string, bool>
            {
                {"move", true}, {"attack", true}
            };
        }

        protected virtual void HighlightMovePath()
        {
            
        }
        
        protected virtual int DamageDone()
        {
            return this.attackPower;
        }
        
        /*-----------PUBLIC-------------*/

        public int MaxMoveDistance => maxMoveDistance;
        public int MaxAttackDistance => maxAttackDistance;
        public int HitPoints => hitPoints;
        public Dictionary<string, bool> AllowedDistance => allowedDirections;
        public Dictionary<string, bool> AllowedActions => allowedActions;
    }
}


