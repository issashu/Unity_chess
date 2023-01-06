using System;
using Mono.Cecil;
using UnityEngine;

namespace Utils
{
    public struct MiscUtils
    {
        /*-----------MEMBERS-------------------*/
        
        
        /*-----------METHODS-------------------*/
        public static void shouldBeWaiting(float waitPeriod)
        {
            while ((waitPeriod -= Time.deltaTime) > 0)
            {
                Debug.Log($"AI snoozing for {waitPeriod} more seconds.");
            }
        }
        
        public static int DistanceBetweenTwoPoints(Point start, Point end)
        {
            var differenceInCoordinates = DirectionBetweenPoints(start, end);
            var shortestDistanceInSquares = Math.Abs(differenceInCoordinates.x) + Math.Abs(differenceInCoordinates.y);

            return shortestDistanceInSquares;
        }

        public static Point DirectionBetweenPoints(Point start, Point end)
        {
            var directionPoint = end - start;

            return directionPoint;
        }

        public static Point NormalizeDirection(Point directionPoint)
        {
            int normalX = 0;
            int normalY = 0;
            
            if (directionPoint.x != 0)
                normalX = directionPoint.x / Math.Abs(directionPoint.x);
            
            if (directionPoint.y != 0)
                normalY = directionPoint.y / Math.Abs(directionPoint.y);
         
            var normalizedDirection = new Point(normalX, normalY);

            return normalizedDirection;
        }
    }
}