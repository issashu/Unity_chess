using System;
using System.Collections.Generic;
using System.Drawing;
using AI;
using Defines;
using GameBoard;
using GamePieces.Drones;
using GamePieces.Humans;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Color = UnityEngine.Color;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance;
        
        private int _gameDifficulty;
        private GameObject _gameBoard;
        private GameObject _gamePieces;
        private GameObject _turnSystem;
        private GameObject _aiPlayer;

        private void Awake()
        {
            // Mostly sanity check, for clones
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
            
            // TODO: Hunt and replace hard coded strings and magic numbers.
            this._gameBoard = new GameObject("GameBoard");
            this._gameBoard.AddComponent<GameBoard.GameBoard>();

            this._gamePieces = new GameObject("GamePieces");
            this._gamePieces.AddComponent<PieceManager>();

            this._turnSystem = new GameObject("TurnSystem");
            this._turnSystem.AddComponent<TurnManager>();
            
            // Individual components in order to add or remove easier at any moment
            this._aiPlayer = new GameObject("AI Controller");
            this._aiPlayer.AddComponent<AiController>();
            this._aiPlayer.AddComponent<DroneDecisionLogic>();
            this._aiPlayer.AddComponent<DreadnoughtDecisionLogic>();
            this._aiPlayer.AddComponent<CommandUnitDecisionLogic>();
        }

        private void Start()
        {
            PieceManager.OnHumansWipe += DroneVictoryAchieved;
            PieceManager.OnAICommandUnitsWipe += HumanVictoryAchieved;
            DronePiece.OnReachingRowZero += DroneVictoryAchieved;
        }
        
        // TODO MAke argst Event.Empty, if nothing will be passed
      private static void DroneVictoryAchieved(object eventSender, EventArgs args)
        {
            // TODO Add some endgame pop-up logic here and stop app
            Debug.Log("Drone team won!");
        }
        
        private static void HumanVictoryAchieved(object eventSender, EventArgs args)
        {
            // TODO Add some endgame pop-up logic here and stop app
            Debug.Log("Humans team won!");
        }
    }
}

