using System;
using Enums;
using UnityEngine;

namespace Managers
{
    public class TurnManager : MonoBehaviour
    {
        /*-----------MEMBERS-------------------*/
        public static event EventHandler<FactionEnum> OnTurnSwitch;
        private static TurnManager _instance;
        
        private FactionEnum _currentTurnFaction;
        
        public FactionEnum ActiveFaction => _currentTurnFaction;
        public static TurnManager Instance => _instance;
        
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
            this._currentTurnFaction = FactionEnum.None;
        }

        private void Start()
        {
            this.SwitchTurn(); // Will make sure humans start first and rest are disabled.
        }
        
        public void SwitchTurn()
        {
            var nextFaction = this._currentTurnFaction == FactionEnum.Humans ? FactionEnum.Drones : FactionEnum.Humans;

            this._currentTurnFaction = nextFaction;
            
            OnTurnSwitch?.Invoke(this, ActiveFaction);
        }
    }
}