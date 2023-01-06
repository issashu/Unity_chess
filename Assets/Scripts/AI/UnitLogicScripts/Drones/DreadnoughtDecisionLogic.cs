using Defines;
using GamePieces;
using Utils;


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
        /*-----------MEMBERS-------------------*/
        private BasePiece selectedUnit;
        
        /*-----------METHODS-------------------*/
        public override void ExecuteUnitBehaviour(BasePiece dreadUnit)
        {
            if (dreadUnit == null)
                return;
            
            if (!dreadUnit.IsPieceActive) 
                return;

            this.selectedUnit = dreadUnit;
            
            AiController.SelectUnit(this.selectedUnit);
            this.MoveDreadUnit();
            this.AttackWithDreadUnit();
        }

        private void MoveDreadUnit()
        {
            /*Kept it simple here, calculating distance between two points in 2D space, BUT if we add obstacles
             (set tiles to non-traversable in Awake() ) a BFS/DFS or similar path search algorithm would be more suited. Prefer to use queue/stack on heap for those then recursion. 
             
             Also permitted myself to add additional logic to the movement: 
             This is the AI killer machine! If within move range and it can hit two or more units, it will move there and
             do it! */
            
            if (!this.selectedUnit.AllowedActions["move"] || this.selectedUnit.AllowedMovesList.Count == 0)
                return;
            
            var moveLocationPoint = GameSettings.GAME_DIFFICULTY < 3 ? this.FindClosestEnemyPoint() : this.FindPointWithMultipleTargets();

            if (MiscUtils.DistanceBetweenTwoPoints(this.selectedUnit.CurrentPieceCoordinates, moveLocationPoint) >
                this.selectedUnit.MaxMoveDistance)
            {
                this.selectedUnit.GenericDirectionalMoveAction(moveLocationPoint);
            }
            else
            {
                var moveLocationTile = ConversionUtils.GetTileAtPoint(moveLocationPoint);
                var currentLocationTile = ConversionUtils.GetTileAtPoint(this.selectedUnit.CurrentPieceCoordinates);
            
                this.selectedUnit.PreciseMoveAction(currentLocationTile, moveLocationTile);
            }
        }
        
        private Point FindPointWithMultipleTargets()
        {
            // Store original position coordinates in a temp
            var tmpPoint = this.selectedUnit.CurrentPieceCoordinates;
            
            var targetLocationPointToMove = tmpPoint;
            int numberOfTargets = 1;
            foreach (var point in this.selectedUnit.AllowedMovesList)
            {
                this.selectedUnit.SetCurrentPosition(point.x, point.y);
                this.selectedUnit.ListAllThreatenedTiles();
                
                var tmpNumberTargets = this.selectedUnit.ThreatenedTiles.Count;
                if (tmpNumberTargets <= numberOfTargets) 
                    continue;
                
                targetLocationPointToMove = point;
                numberOfTargets = tmpNumberTargets;
            }
            // We reset back to original coordinates after we are done looking around
            this.selectedUnit.SetCurrentPosition(tmpPoint.x, tmpPoint.y);

            return targetLocationPointToMove;
        }

        private Point FindClosestEnemyPoint()
        {
            /* Used for initialisation */
            var rootXY = new Utils.Point(0, 0);
            var shortestDistanceInSquares = MiscUtils.DistanceBetweenTwoPoints(this.selectedUnit.CurrentPieceCoordinates, rootXY);
            var closestTargetPoint = rootXY;
                
            foreach (var unit in humanUnits)
            {
                var unitLocation = unit.GetComponent<BasePiece>().CurrentPieceCoordinates;
                var distanceToUnitInSquares = MiscUtils.DistanceBetweenTwoPoints(this.selectedUnit.CurrentPieceCoordinates, unitLocation);

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
            if (!this.selectedUnit.AllowedActions["attack"] || this.selectedUnit.ThreatenedTiles.Count == 0)
                return;
            
            BasePiece dummyParameter = null;
            this.selectedUnit.AttackAction(dummyParameter);
        }
        
    }
}