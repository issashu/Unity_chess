using System;
using System.Collections.Generic;
using System.Drawing;
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
        private int _gameDifficulty;
        private GameObject _gameBoard;
        private GameObject _gamePieces;

        private void Awake()
        {
            // Mostly sanity check, for clones
            if (_instance != null && _instance != this)
            {
                // TODO Make the script spawn the other Managers and attach them to himself or make gatekeeper Singleton managers object group
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
            
            // TODO: Hunt and replace hard coded strings and magic numbers.
            this._gameBoard = new GameObject("GameBoard");
            this._gameBoard.AddComponent<GameBoard.GameBoard>();

            this._gamePieces = new GameObject("GamePieces");
            this._gamePieces.AddComponent<Managers.PieceManager>();
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

