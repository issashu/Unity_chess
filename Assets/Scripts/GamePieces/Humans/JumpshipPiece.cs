using System;
using System.Collections.Generic;
using Enums;
using GameBoard;
using UnityEngine;
using Utils;

namespace GamePieces.Humans
{
    public class JumpshipPiece : BasePiece
    {
        protected override void Awake()
        {
            this.maxMoveDistance = 0;
            this.maxAttackDistance = 1;
            this.attackPower = 2;
            this.hitPoints = 2;
            this.gameTeam = (int) FactionEnum.Humans;
            this.isAlive = true;
            this.isActive = true;
            this.currentTilePosition = new Point(0, 0);
            this.gameSprite = Resources.Load<Sprite>("Sprites/Humans/Jumpship");
            this.validMovesFromPosition = new List<Point>();
            this.threatenedTilesFromPosition = new List<Point>();

            this.movesXAxis = new [] {1, -1, 2, 2, 1, -1, -2, -2};
            this.movesYAxis = new [] {2, 2, 1, -1, -2, -2, -1, 1};
            
            this.attacksXAxis = new [] {0, 1, 0,-1};
            this.attacksYAxis = new [] {1, 0,-1, 0};

            this.allowedActions = new Dictionary<string, bool>
            {
                {"move", true}, {"attack", true}
            };
            
            this.boxColliderSettings = new Dictionary<string, float>
            {
                {"offsetX", 0.10f}, {"offsetY", 1.62f},
                {"sizeX", 1.16f},   {"sizeY", 3.64f}
            };
        }
        
        protected override void ListPossibleMovesFromPosition()
        {
            // Standalone move mechanic
            
            int directions = movesXAxis.Length;
            var gameBoard = GameBoard.GameBoard.Board;
            var boardMatrix = GameBoard.GameBoard.Board.BoardMatrix;

            for (int i = 0; i < directions; i++)
            {
                var newPosition = new Point(this.currentTilePosition.x + movesXAxis[i],
                                            this.currentTilePosition.y + movesYAxis[i]);
                if (!GameBoard.GameBoard.Board.isPointWithinBoardLimits(newPosition))
                    continue;
                
                // TODO: Code repetition with basic piece. Extract to a method 
                var tile = boardMatrix[newPosition.x, newPosition.y];
                if (!tile.GetComponent<BoardTile>().isTileOccupied())
                {
                    //Add valid move tile, only if no piece is there.
                    this.validMovesFromPosition.Add(newPosition);
                }
            }
        }
    }
}