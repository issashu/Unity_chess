using System.Collections.Generic;
using Enums;
using UnityEngine;
using Utils;

namespace GamePieces.Humans
{
    public class TankPiece : BasePiece
    {
        /*-----------MEMBERS-------------------*/
        
        
        /*-----------METHODS-------------------*/
        protected override void Awake()
        {
            this.maxMoveDistance = 3;
            this.maxAttackDistance = 7;
            this.attackPower = 2;
            this.hitPoints = 4;
            this.gameTeam = (int) FactionEnum.Humans;
            this.pieceTypeAndPointsValue = (int) HumanUnits.Tank;
            this.isAlive = true;
            this.isActive = true;
            this.currentTilePosition = new Point(0, 0);
            this.gameSprite = Resources.Load<Sprite>("Sprites/Humans/Tank");
            this.validMovesFromPosition = new List<Point>();
            this.threatenedTilesFromPosition = new List<Point>();

            this.movesXAxis = new [] {0, 1, 1, 1, 0,-1,-1,-1};
            this.movesYAxis = new [] {1, 1, 0,-1,-1,-1, 0, 1};
            
            this.attacksXAxis = new [] {0, 1, 0,-1};
            this.attacksYAxis = new [] {1, 0,-1, 0};
            
            this.allowedActions = new Dictionary<string, bool>
            {
                {"move", true}, {"attack", true}
            };
            
            this.boxColliderSettings = new Dictionary<string, float>
            {
                {"offsetX", 0.01f}, {"offsetY", 1.04f},
                {"sizeX", 1.06f},   {"sizeY", 1.82f}
            };
            
            this.healthDisplay = SetupHealthDisplay(this.HitPoints.ToString());
        }
    }
}