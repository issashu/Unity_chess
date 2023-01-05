using System;
using System.Linq;
using Defines;
using GamePieces;
using Unity.Mathematics;
using Utils;
using Unity.VisualScripting;

namespace AI
{
    /*
        * Behaviour:       
        *        ● Dreadnoughts move after all drones have moved.
        *        ● It must move 1 space, if possible, and must move towards the nearest enemy unit. It must try to
        *          attack after moving.
        */
    public class DreadnoughtDecisionLogic : AIDecisionLogic
    {
        private BasePiece droneUnitSelected;
        
        public override void ExecuteUnitBehaviour(BasePiece dreadUnit)
        {
            if (dreadUnit == null)
                return;
            
            if (!dreadUnit.IsPieceActive) 
                return;

            this.droneUnitSelected = dreadUnit;
            
            AiController.SelectUnit(this.droneUnitSelected);
            this.MoveDreadUnit();
            
            MiscUtils.shouldBeWaiting(GameSettings.DEFAULT_AI_WAIT_TIMER);
            
            this.AttackWithDreadUnit();
        }

        private void MoveDreadUnit()
        {
            /*Kept it simple here, calculating distance between two points in 2D space, BUT if we add obstacles BFS/DFS
             or similar path search algorithm would be more suited. Prefer to use queue/stack on heap for those then recursion. 
             Also allowed myself to add additional logic to the movement: 
             This is the AI killer machine! If within move range, it can hit two or more units, it will move there and
             do it */
            
            if (!this.droneUnitSelected.AllowedActions["move"] || this.droneUnitSelected.AllowedMovesList.Count == 0)
                return;
            
            // TODO Make the second part of if reachable. Game difficulty to be modifiable, not constant
            var moveLocationPoint = GameSettings.GAME_DIFFICULTY < 3 ? this.FindClosestEnemyPoint() : this.FindPointWithMultipleTargets();

            if (MiscUtils.DistanceBetweenTwoPoints(this.droneUnitSelected.CurrentPieceCoordinates, moveLocationPoint) >
                this.droneUnitSelected.MaxMoveDistance)
            {
                this.droneUnitSelected.GenericDirectionalMoveAction(moveLocationPoint);
            }
            else
            {
                // TODO Add sanity checks either here or in the returns
                var moveLocationTile = ConversionUtils.GetTileAtPoint(moveLocationPoint);
                var currentLocationTile = ConversionUtils.GetTileAtPoint(this.droneUnitSelected.CurrentPieceCoordinates);
            
                this.droneUnitSelected.PreciseMoveAction(currentLocationTile, moveLocationTile);
            }
        }
        
        private Point FindPointWithMultipleTargets()
        {
            // Store original position coordinates in a temp
            var tmpPoint = this.droneUnitSelected.CurrentPieceCoordinates;
            
            var targetLocationPointToMove = tmpPoint;
            int numberOfTargets = 1;
            foreach (var point in this.droneUnitSelected.AllowedMovesList)
            {
                this.droneUnitSelected.SetCurrentPosition(point.x, point.y);
                this.droneUnitSelected.ListAllThreatenedTiles();
                
                var tmpNumberTargets = this.droneUnitSelected.ThreatenedTiles.Count;
                if (tmpNumberTargets <= numberOfTargets) 
                    continue;
                
                targetLocationPointToMove = point;
                numberOfTargets = tmpNumberTargets;
            }
            // We reset back to original coordinates after we are done looking around
            this.droneUnitSelected.SetCurrentPosition(tmpPoint.x, tmpPoint.y);

            return targetLocationPointToMove;
        }

        private Point FindClosestEnemyPoint()
        {
            /* Used for initialisation */
            var rootXY = new Utils.Point(0, 0);
            var shortestDistanceInSquares = MiscUtils.DistanceBetweenTwoPoints(this.droneUnitSelected.CurrentPieceCoordinates, rootXY);
            var closestTargetPoint = rootXY;
                
            foreach (var unit in humanUnits)
            {
                var unitLocation = unit.GetComponent<BasePiece>().CurrentPieceCoordinates;
                var distanceToUnitInSquares = MiscUtils.DistanceBetweenTwoPoints(this.droneUnitSelected.CurrentPieceCoordinates, unitLocation);

                if (distanceToUnitInSquares < shortestDistanceInSquares)
                {
                    shortestDistanceInSquares = distanceToUnitInSquares;
                    closestTargetPoint = unitLocation;
                }
            }

            return closestTargetPoint;
        }

        private void AttackWithDreadUnit()
        {
            if (!this.droneUnitSelected.AllowedActions["attack"] || this.droneUnitSelected.ThreatenedTiles.Count == 0)
                return;
            BasePiece dummyParameter = null;
            this.droneUnitSelected.AttackAction(dummyParameter);
        }
        
    }
}