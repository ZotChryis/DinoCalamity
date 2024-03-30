using System;
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
        [SerializeField] private Animation m_animation;

        private Gameplay.Cards.Card  m_source;

        private void Start()
        {
            ServiceLocator.Instance.Player.SelectedCard.OnChangedValues += OnSelectedCardChanged;
        }

        private void OnSelectedCardChanged(Gameplay.Cards.Card oldValue, Gameplay.Cards.Card newValue)
        {
            // TEMP - Need better animation support
            if (newValue != m_source)
            {
                m_animation.Play("Card_Normal");
            }
            else
            {
                m_animation.Play("Card_Selected");
            }
        }

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

        public void Dispose()
        {
            ServiceLocator.Instance.Player.SelectedCard.OnChangedValues -= OnSelectedCardChanged;
            Destroy(gameObject);
        }
    }
}
