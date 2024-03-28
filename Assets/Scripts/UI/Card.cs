using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_name;
        [SerializeField] private Image m_icon;
        [SerializeField] private Button m_button;

        private Gameplay.Cards.Card  m_source;

        public void SetSource(Gameplay.Cards.Card source)
        {
            m_source = source;
            m_name.SetText(source.Data.Name);
            m_icon.sprite = source.Data.Icon;
        }
        
        private void Start()
        {
            m_button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            // TEMP - for now execute all onplay actions when clicking the card in hand
            m_source.InvokeActions(Schemas.Card.EventType.OnPlay);
        }
    }
}
