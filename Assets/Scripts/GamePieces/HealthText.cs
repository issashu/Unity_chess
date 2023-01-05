using TMPro;
using UnityEngine;

namespace GamePieces
{
    public class HealthText : MonoBehaviour
    {
        private TextMeshPro _healthText;

        public TMP_Text CurrentHealthDisplay => _healthText;
        
        private void Awake()
        {
            this._healthText = gameObject.AddComponent<TextMeshPro>();
            this._healthText.text = "10";
            this._healthText.fontSize = 8;
            this._healthText.alignment = TextAlignmentOptions.Justified;
            this._healthText.verticalAlignment = VerticalAlignmentOptions.Middle;
            this._healthText.horizontalAlignment = HorizontalAlignmentOptions.Center;
            this._healthText.color = Color.white;

            BasePiece.OnDamageTaken += this.UpdateTextValue;;
        }

        public void UpdateTextValue(object sender, string newTextValue)
        {
            this._healthText.text = newTextValue;
        }
        
        
    }
}