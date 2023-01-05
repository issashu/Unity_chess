using System;
using System.Linq;
using GamePieces;
using Utils;
using Unity.VisualScripting;

namespace AI
{
    /*
        * Behaviour:
        *       ● Drones move before any other AI piece.
        *       ● They must all move and attack if possible.
        *       ● They must shoot at a target if possible after attempting to move
        */
    public class DroneDecisionLogic : AIDecisionLogic
    {
        protected BasePiece selectedUnit;


        protected override void ExecuteUnitBehaviour(BasePiece droneUnit)
        {
            if (droneUnit == null)
                return;
            
            if (!droneUnit.IsPieceActive) 
                return;

            this.selectedUnit = droneUnit;
            
            aiActions.SelectUnit(this.selectedUnit);
            this.MoveDroneUnit();
            this.AttackWithDroneUnit();
        }
        
        protected void MoveDroneUnit()
        {
            if (!this.selectedUnit.AllowedActions["move"] || this.selectedUnit.AllowedMovesList.Count == 0)
                return;

            var moveLocationPoint = this.selectedUnit.AllowedMovesList[this.selectedUnit.MaxMoveDistance - 1];
            var moveLocationTile = ConversionUtils.GetTileAtPoint(moveLocationPoint);
            var currentLocationTile = ConversionUtils.GetTileAtPoint(this.selectedUnit.CurrentPieceCoordinates);
            
            this.selectedUnit.MoveAction(currentLocationTile, moveLocationTile);

        }

        protected BasePiece ChooseAttackTarget()
        { 
            /*Low damage unit, so we choose where impact will be greatest - min Health or max Importance*/
            var firstTargetPoint = ConversionUtils.GetTileAtPoint(this.selectedUnit.ThreatenedTiles[0]);
            var targetPiece = firstTargetPoint.TileOccupant;
            int targetHitPoints = targetPiece.HitPoints;
            
            foreach (var point in this.selectedUnit.ThreatenedTiles)
            {
                var potentialTarget = ConversionUtils.GetTileAtPoint(point).TileOccupant;

                if (potentialTarget.HitPoints < targetHitPoints)
                {
                    targetHitPoints = potentialTarget.HitPoints;
                    targetPiece = potentialTarget;
                }
                else 
                {
                    targetPiece = potentialTarget.PiecePointsValue > targetPiece.PiecePointsValue
                        ? potentialTarget
                        : targetPiece;
                }
            }

            return targetPiece;
        }

        protected void AttackWithDroneUnit()
        {
            if (!this.selectedUnit.AllowedActions["attack"] || this.selectedUnit.ThreatenedTiles.Count == 0)
                return;
            
            var licenceToKill = this.ChooseAttackTarget();
            this.selectedUnit.AttackAction(licenceToKill);
        }
        
    }
}