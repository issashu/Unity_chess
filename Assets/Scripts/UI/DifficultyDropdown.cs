using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

namespace UI
{
    public class DifficultyDropdown : MonoBehaviour
    {
        public static event EventHandler<int> OnDifficultySwitch;
        
        public void HandleDropdownSelection(int selection)
        {
            switch (selection)
            {
                case (int)Enums.GameDifficulty.Easy:
                    OnDifficultySwitch?.Invoke(this, (int)Enums.GameDifficulty.Easy);
                    break;
                
                case (int)Enums.GameDifficulty.Normal:
                    OnDifficultySwitch?.Invoke(this, (int)Enums.GameDifficulty.Normal);
                    break;
                
                case (int)Enums.GameDifficulty.Hard:
                    OnDifficultySwitch?.Invoke(this, (int)Enums.GameDifficulty.Hard);
                    break;
            }
            
        }


    }
}