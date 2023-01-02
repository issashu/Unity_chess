using System;
using Enums;
using UnityEngine;

namespace Managers
{
    public class TurnManager : MonoBehaviour
    {
        public static event EventHandler<FactionEnum> OnTurnSwitch;

        private FactionEnum _currentTurnFaction;
        public FactionEnum ActiveFactionEnum => _currentTurnFaction;
        public int ActiveFactionInt => (int) _currentTurnFaction;
        
        private void Awake()
        {
            this._currentTurnFaction = FactionEnum.None;
        }

        public void SwitchTurn(FactionEnum nextFaction)
        {
            if (nextFaction == FactionEnum.None)
            {
                Debug.LogError("Cannot set null faction for next turn!");
                return;
            }
            
            this._currentTurnFaction = nextFaction;
            
            OnTurnSwitch?.Invoke(this, ActiveFactionEnum);
        }
    }
}