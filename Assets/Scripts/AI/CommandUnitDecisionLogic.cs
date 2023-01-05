using System.Collections.Generic;
using GamePieces;
using Utils;

namespace AI
{
    /*
     * Behaviour:
     *   ● It must avoid getting hit, if possible, so it must make the best move out of the three options available
     *     (move one way, move the other way, or stay still). 
     */
    public class CommandUnitDecisionLogic : AIDecisionLogic
    {
        protected BasePiece droneUnitSelected;
        
        protected override void ExecuteUnitBehaviour(BasePiece commandUnit)
        {
            if (commandUnit == null)
                return;
            
            if (!commandUnit.IsPieceActive) 
                return;

            this.droneUnitSelected = commandUnit;
            
            aiActions.SelectUnit(this.droneUnitSelected);

            /* If safe - don't move*/
            if (!IsPositionDangerous(this.droneUnitSelected.CurrentPieceCoordinates))
                return;
            
            this.droneUnitSelected.ListAllMoveTiles();
            this.MoveControlUnit();
        }
        
        protected Point ChooseMovePoint()
        {
            /*Rather simple here, but a perfect candidate for a min-max algorithm. Right now will move to first safe spot.
             If all are dangerous, won't move, plug and pray.*/
            
            Point targetMovePoint = this.droneUnitSelected.CurrentPieceCoordinates;
            
            foreach (var movePoint in this.droneUnitSelected.AllowedMovesList)
            {
                if (!IsPositionDangerous(movePoint))
                {
                    targetMovePoint = movePoint;
                    break;
                }
            }

            return targetMovePoint;
        }

        protected bool IsPositionDangerous(Point targetMovePoint)
        {
            foreach (var potentialHooligan in this.humanUnits)
            {
                if (potentialHooligan.GetComponent<BasePiece>().ThreatenedTiles.Contains(targetMovePoint))
                    return true;
            }
            return false;
        }

        protected void MoveControlUnit()
        {
            var targetLocation = ConversionUtils.GetTileAtPoint(ChooseMovePoint());
            var startLocation = ConversionUtils.GetTileAtPoint(this.droneUnitSelected.CurrentPieceCoordinates);
            
            this.droneUnitSelected.MoveAction(startLocation, targetLocation);
        }
    }
}