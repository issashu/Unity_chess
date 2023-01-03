using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EndTurnButton : MonoBehaviour
    {
        private Button _buttonObject;

        private void Awake()
        {
            this._buttonObject = GameObject.Find("EndTurnButton").GetComponent<Button>();
        }

        private void Start()
        {
            // Button logic in Start, so all awakes are done and everything is set up. Lambda so we don't make a whole new method
            this._buttonObject.onClick.AddListener(()=>{TurnManager.Instance.SwitchTurn();});
        }
    }
}