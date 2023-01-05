using System;

using UnityEngine;
using GameBoard;
using GamePieces;
using GamePieces.Drones;
using Managers;
using Utils;

namespace AI
{
    public class AIActions : MonoBehaviour
    {
        private static AIActions _instance;
        public static AIActions AiActions => _instance;
        
        private GameBoard.GameBoard _gameBoard;
        private BoardTile[,] _boardMatrix;
        private PieceManager _pieceManager;
        private MouseEventUtils _mouseController;
        private TurnManager _turnManager;

        private void Awake()
        {
            // Mostly sanity check, for clones
            if (_instance != null && _instance != this)
            { 
                Destroy(this.gameObject);
                return;
            }
            _instance = this;
            
            _gameBoard = GameBoard.GameBoard.Board;
            _boardMatrix = _gameBoard.GameBoardMatrix;
            _pieceManager = PieceManager.Instance;
        }

        private void Start()
        {
            
        }
        
        public void SelectUnit(BasePiece droneUnitSelected)
        {
            this.HighlightUnitSelectedByAi(droneUnitSelected);
            droneUnitSelected.HighlightMovePath();
            droneUnitSelected.HighlightThreatenedTiles();
        }
        public void HighlightUnitSelectedByAi(BasePiece selectedUnit)
        {
            selectedUnit.ChangePieceColor(Color.cyan);
        }
    }
}