using System.Collections.Generic;
using Defines;
using Enums;
using UnityEngine;
using GameBoard;
using GamePieces;
using Managers;
using Utils;

namespace AI
{
    public class AiController : MonoBehaviour
    {
        /*-----------MEMBERS-------------------*/
        public static AiController AIController => _instance;
        private static AiController _instance;
        
        protected List<GameObject> droneUnits;
        protected List<GameObject> commandUnits;
        private GameBoard.GameBoard _gameBoard;
        private BoardTile[,] _boardMatrix;
        private PieceManager _pieceManager;
        private TurnManager _turnManager;
        
        /*-----------METHODS-------------------*/
        
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
            TurnManager.OnTurnSwitch += PlayTurn;
            droneUnits = PieceManager.Instance.AiPieces;
            commandUnits = PieceManager.Instance.DroneCommandUnits;
        }

        private void PlayTurn(object sender, FactionEnum turnFaction)
        {
            if (turnFaction != FactionEnum.Drones)
                return;
            
            this.ExecuteAiControlLogic();
            MiscUtils.shouldBeWaiting(GameSettings.DEFAULT_AI_WAIT_TIMER);
            this.EndAiTurn();
        }

        private void ExecuteAiControlLogic()
        {
            foreach (var unit in droneUnits)
            {
                var selectedUnit = unit.GetComponent<BasePiece>();
                var selectedUnitLogic = selectedUnit.UnitAiBehaviourLogic;

                if (selectedUnitLogic is null)
                {
                    Debug.LogError("Drone Logic script not found!");
                    return;
                }
                
                selectedUnitLogic.ExecuteUnitBehaviour(selectedUnit);
                this._gameBoard.ClearBoardColors();
            }
        }
        
        private void EndAiTurn()
        {
            TurnManager.Instance.SwitchTurn();
        }
    }
}