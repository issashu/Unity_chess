using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class WinAnnouncementText : MonoBehaviour
    {
       /*-----------MEMBERS-------------------*/
       private TextMeshPro _endGameText;

       public TMP_Text WinnerAnnouncement => _endGameText;
    
        
       /*-----------METHODS-------------------*/
       private void Awake()
       { 
           this._endGameText = gameObject.AddComponent<TextMeshPro>();
           this._endGameText.text = "Player wins!";
           this._endGameText.fontSize = 15;
           this._endGameText.alignment = TextAlignmentOptions.Justified;
           this._endGameText.verticalAlignment = VerticalAlignmentOptions.Middle;
           this._endGameText.horizontalAlignment = HorizontalAlignmentOptions.Center;
           this._endGameText.color = Color.white;
       }

       public void UpdateTextValue(string newTextValue)
       {
                this._endGameText.text = newTextValue;
       }

       public void SwitchActive()
       {
           this.GameObject().SetActive(!this.GameObject().activeSelf);
       }
    }
}
