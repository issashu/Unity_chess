using System.Collections.Generic;
using UnityEngine;

namespace GamePieces
{
    public class BasePiece : MonoBehaviour
    {
        [SerializeField] protected int maxMoveDistance;
        [SerializeField] protected int maxAttackDistance;
        [SerializeField] protected int numberOfAttacks;
        [SerializeField] protected int attackPower;
        [SerializeField] protected int hitPoints;
        [SerializeField] protected bool isAlive;
        [SerializeField] protected Sprite gameSprite;
        protected Dictionary<string, float> boxColliderSettings;
        protected Dictionary<string, bool> allowedMoveDirections;
        protected Dictionary<string, bool> allowedAttackDirections;
        protected Dictionary<string, bool> allowedActions;
        
        protected virtual void Awake()
        {
            this.maxMoveDistance = 0;
            this.maxAttackDistance = 0;
            this.numberOfAttacks = 0;
            this.attackPower = 0;
            this.hitPoints = 0;
            this.isAlive = false;

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

            this.boxColliderSettings = new Dictionary<string, float>
            {
                {"offsetX", 0.00f}, {"offsetY", 0.0f},
                {"sizeX", 0.0f}, {"sizeY", 0.0f}
            };
        }

        protected virtual void HighlightMovePath()
        {
            
        }
        
        // TODO: Currently unit teleports to new location. Make it slide there.
        protected virtual void MovePiece(Vector2 targetLocation)
        {
            this.transform.position = targetLocation;
        }
        
        protected virtual int DamageDone()
        {
            return this.attackPower;
        }
        
        /*-----------PUBLIC-------------*/

        public int MaxMoveDistance => maxMoveDistance;
        public int MaxAttackDistance => maxAttackDistance;
        public int HitPoints => hitPoints;
        public bool IsUnitAlive => isAlive;
        public Sprite UnitSprite => gameSprite;
        public Dictionary<string, bool> AllowedMoveDirections => allowedMoveDirections;
        public Dictionary<string, bool> AllowedAttackDirection => allowedAttackDirections;
        public Dictionary<string, bool> AllowedActions => allowedActions;
        public Dictionary<string, float> BoxColliderSettings => boxColliderSettings;
    }
}


