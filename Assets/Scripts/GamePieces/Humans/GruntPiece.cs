using System.Collections.Generic;
using Enums;
using UnityEngine;
using Utils;

namespace GamePieces.Humans
{
    public class GruntPiece : BasePiece
    
    {
        protected override void Awake()
        {
            this.maxMoveDistance = 1;
            this.maxAttackDistance = 7;
            this.attackPower = 1;
            this.hitPoints = 2;
            this.gameTeam = (int) FactionEnum.Humans;
            this.piecePointsValue = (int) HumanUnits.Grunt;
            this.isAlive = true;
            this.isActive = true;
            this.currentTilePosition = new Point(0, 0);
            this.gameSprite = Resources.Load<Sprite>("Sprites/Humans/Grunt");
            this.validMovesFromPosition = new List<Point>();
            this.threatenedTilesFromPosition = new List<Point>();
            this.movesXAxis = new [] {0, 1, 0, -1};
            this.movesYAxis = new [] {1, 0, -1, 0};
            
            this.attacksXAxis = new [] {1, 1,-1,-1};
            this.attacksYAxis = new [] {1,-1,-1, 1};

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