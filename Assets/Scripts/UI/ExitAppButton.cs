using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ExitAppButton : MonoBehaviour
    {
        private Button _buttonObject;

        private void Awake()
        {
            this._buttonObject = GameObject.Find("QuitApplicationButton").GetComponent<Button>();
        }

        private void Start()
        {
            this._buttonObject.onClick.AddListener(()=>{GameManager.Instance.QuitApplication();});
        }
    }
}