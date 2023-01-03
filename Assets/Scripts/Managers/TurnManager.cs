using System;
using Enums;
using UnityEngine;

namespace Managers
{
    public class TurnManager : MonoBehaviour
    {
        public static event EventHandler<FactionEnum> OnTurnSwitch;

        private static TurnManager _instance;
        private FactionEnum _currentTurnFaction;
        public FactionEnum ActiveFaction => _currentTurnFaction;
        public int ActiveFactionInt => (int) _currentTurnFaction;
        public static TurnManager Instance => _instance;
        
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
            this._currentTurnFaction = FactionEnum.None;
        }

        private void Start()
        {
            this.SwitchTurn(); // Will make sure humans start first and rest are disabled.
        }
        
        public void SwitchTurn()
        {
            Debug.Log("TURN SWITCH CALLED!");
            var nextFaction = this._currentTurnFaction == FactionEnum.Humans ? FactionEnum.Drones : FactionEnum.Humans;

            this._currentTurnFaction = nextFaction;
            
            OnTurnSwitch?.Invoke(this, ActiveFaction);
        }
    }
}