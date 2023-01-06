using System;
using System.Collections;
using System.Collections.Generic;
using GameBoard;
using Unity.VisualScripting;
using UnityEngine;


namespace Utils
{
    public class ConversionUtils
    {
        /*-----------MEMBERS-------------------*/
        
        /*-----------METHODS-------------------*/
        public static Vector2 WorldPositionFromCoordinates(int x, int y, int objectSize=1)
        {
            return new Vector2(x, y) * objectSize;
        }

        public static Point CreatePointObjectFromTile(BoardTile tile)
        {
            var tmpPoint = new Point(tile.XCoordinate, tile.YCoordinate);

            return tmpPoint;
        }

        public static BoardTile GetTileAtPoint(Point coordinates)
        {
            var boardMatrix = GameBoard.GameBoard.Board.GameBoardMatrix;
            return boardMatrix[coordinates.x, coordinates.y];
        }

        public static BoardTile GetTileAtCoordinates(int x, int y)
        {
            var boardMatrix = GameBoard.GameBoard.Board.GameBoardMatrix;
            return boardMatrix[x, y];
        }
    }
    
    public struct Point
    {
        /*-----------MEMBERS-------------------*/
        public int x;
        public int y;
        
        /*-----------METHODS-------------------*/
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(Point other)
        {
            this.x = other.x;
            this.y = other.y;
        }

        public override string ToString()
        {
            return $"x: {this.x} / y: {this.y}";
        }
        
        public static bool operator == (Point one, Point other)
        {
            return one.x == other.x && one.y == other.y;
        }

        public static bool operator != (Point one, Point other)
        {
            return one.x != other.x || one.y != other.y;
        }

        public static Point operator + (Point one, Point other)
        {
            var tmp = new Point(0, 0);
            tmp.x = one.x + other.x;
            tmp.y = one.y + other.y;

            return tmp;
        }
        
        public static Point operator - (Point one, Point other)
        {
            var tmp = new Point(0, 0);
            tmp.x = one.x - other.x;
            tmp.y = one.y - other.y;

            return tmp;
        }
    }
}