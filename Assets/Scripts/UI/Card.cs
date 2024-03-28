using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class Card : MonoBehaviour, IPointerDownHandler
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
        
        public void OnPointerDown(PointerEventData eventData)
        {
            // Only left clicks are counted for selecting cards
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            ServiceLocator.Instance.Player.SelectedCard.Value = m_source;
        }
    }
}
