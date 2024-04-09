using System;
using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class PlayableCardView : BaseCardView, IPointerDownHandler
    {
        private void Awake()
        {
            ServiceLocator.Instance.Loadout.SelectedCard.OnChangedValues += OnSelectedCardChanged;
        }

        private void OnSelectedCardChanged(Card oldValue, Card newValue)
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
        
        public void OnPointerDown(PointerEventData eventData)
        {
            // Only left clicks are counted for selecting cards
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            ServiceLocator.Instance.Loadout.SelectedCard.Value = m_source;
        }

        protected override void OnDestroy()
        {
            ServiceLocator.Instance.Loadout.SelectedCard.OnChangedValues -= OnSelectedCardChanged;
            base.OnDestroy();
        }
    }
}
