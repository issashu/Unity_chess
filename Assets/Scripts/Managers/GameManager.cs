using System;
using AI;
using Defines;
using GameBoard;
using GamePieces.Drones;
using UI;

using UnityEngine;


namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        /*-----------MEMBERS-------------------*/
        private static GameManager _instance;
        public static GameManager Instance => _instance;
        
        private int _gameDifficulty;
        private GameObject _gameBoard;
        private GameObject _gamePieces;
        private GameObject _turnSystem;
        private GameObject _difficultySelector;
        private GameObject _aiPlayer;
        
        public static event EventHandler OnDifficultySwitchWipe;
        public static event EventHandler<int> OnDifficultySwitchSpawn;
        
        /*-----------METHODS-------------------*/
        private void Awake()
        {
            // Mostly sanity check, for clones
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            _instance = this;

            this._gameDifficulty = (int) Enums.GameDifficulty.Easy;
            SpawnMatrix.InitialiseSpawnMatrix();
            SpawnMatrix.SetSpawns();
            
            this._gameBoard = new GameObject("GameBoard");
            this._gameBoard.AddComponent<GameBoard.GameBoard>();

            this._gamePieces = new GameObject("GamePieces");
            this._gamePieces.AddComponent<PieceManager>();

            this._turnSystem = new GameObject("TurnSystem");
            this._turnSystem.AddComponent<TurnManager>();
            
            // Individual components in order to add or remove easier at any moment
            this._aiPlayer = new GameObject("AI Controller");
            this._aiPlayer.AddComponent<AiController>();

            this._gameDifficulty = 1;
        }

        private void Start()
        {
            PieceManager.OnHumansWipe += DroneVictoryAchieved;
            PieceManager.OnAICommandUnitsWipe += HumanVictoryAchieved;
            DronePiece.OnReachingRowZero += DroneVictoryAchieved;
            DifficultyDropdown.OnDifficultySwitch += ChangeDifficulty;
            BoardSpawner.easyBoardSpawner(this._gameDifficulty);
        }
        
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

        private void ChangeDifficulty(object sender, int newDifficulty)
        {
            this._gameDifficulty = newDifficulty;
            OnDifficultySwitchWipe?.Invoke(this, EventArgs.Empty);
            OnDifficultySwitchSpawn?.Invoke(this,this._gameDifficulty);
        }
        
        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}

