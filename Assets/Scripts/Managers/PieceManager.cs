using System;
using System.Collections.Generic;
using Enums;
using GamePieces;
using UnityEngine;
using Utils;


namespace Managers
{
    public class PieceManager: MonoBehaviour
    {
        /*-----------MEMBERS-------------------*/
        private char[,] _spawnMatrix;
        private static PieceManager _instance;
        public static PieceManager Instance => _instance;
        public static event EventHandler OnHumansWipe;
        public static event EventHandler OnDronesWipe;
        public static event EventHandler OnAICommandUnitsWipe;
        
        private List<GameObject> _spawnedHumanPieces;
        private List<GameObject> _spawnedAIPieces;
        private List<GameObject> _spawnedAICommandUnits;
        
        public List<GameObject> AiPieces => this._spawnedAIPieces;
        public List<GameObject> HumanPieces => this._spawnedHumanPieces;
        public List<GameObject> DroneCommandUnits => this._spawnedAICommandUnits;
        
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

            this._spawnedHumanPieces = new List<GameObject>();
            this._spawnedAIPieces = new List<GameObject>();
            this._spawnedAICommandUnits = new List<GameObject>();
            
        }
        
        private void Start()
        {
            
            BasePiece.OnHealthZero += DestroyPiece;
            GameBoard.GameBoard.OnTileWipe += DestroyPiece;
            TurnManager.OnTurnSwitch += SwitchActiveFaction;
        }

        private void Update()
        {
          
            if (this._spawnedHumanPieces.Count == 0)
            {
                OnHumansWipe?.Invoke(this, EventArgs.Empty);
            }
            
            if (this._spawnedAIPieces.Count == 0)
            {
                OnDronesWipe?.Invoke(this, EventArgs.Empty);
            }

            if (this._spawnedAICommandUnits.Count == 0)
            {
                OnAICommandUnitsWipe?.Invoke(this, EventArgs.Empty);
            }
        }

        public static bool arePiecesSameTeam(BasePiece one, BasePiece other)
        {
            return one.PieceFaction == other.PieceFaction;
        }

        private void DestroyPiece(object sender, BasePiece deadPiece)
        {
            var faction = deadPiece.PieceFaction;
            switch (faction)
            {
                case (int)FactionEnum.Drones:
                    this._spawnedAIPieces.Remove(deadPiece.gameObject);
                    this._spawnedAICommandUnits.Remove(deadPiece.gameObject);
                    break;
                
                case (int)FactionEnum.Humans:
                    this._spawnedHumanPieces.Remove(deadPiece.gameObject);
                    break;
            }

            ConversionUtils.GetTileAtPoint(deadPiece.CurrentPieceCoordinates).ClearOccupant();
            deadPiece.OnDestroy();
            Destroy(deadPiece.gameObject);
        }
        
        private void SwitchActiveFaction(object sender, FactionEnum newFaction)
        {
            switch (newFaction)
            {
                case FactionEnum.Humans:
                    foreach (var unit in this._spawnedHumanPieces)
                    {
                        var unitScript = unit.GetComponent<BasePiece>();
                        unitScript.ActivatePiece();
                        unitScript.ResetPieceActions();
                    }
                    
                    foreach (var unit in this._spawnedAIPieces)
                    {
                        var unitScript = unit.GetComponent<BasePiece>();
                        unitScript.DeactivatePiece();
                    }
                    
                    foreach (var unit in this._spawnedAICommandUnits)
                    {
                        var unitScript = unit.GetComponent<BasePiece>();
                        unitScript.DeactivatePiece();
                    }
                    
                    break;
                
                case FactionEnum.Drones:
                    foreach (var unit in this._spawnedAIPieces)
                    {
                        var unitScript = unit.GetComponent<BasePiece>();
                        unitScript.ActivatePiece();
                        unitScript.ResetPieceActions();
                    }
                    
                    foreach (var unit in this._spawnedAICommandUnits)
                    {
                        var unitScript = unit.GetComponent<BasePiece>();
                        unitScript.ActivatePiece();
                        unitScript.ResetPieceActions();
                    }
                    
                    foreach (var unit in this._spawnedHumanPieces)
                    {
                        var unitScript = unit.GetComponent<BasePiece>();
                        unitScript.DeactivatePiece();
                    }

                    break;
                
                case FactionEnum.None:
                default:
                    Debug.LogError("Incorrect faction passed!");
                    break;
            }
        }
    }
}