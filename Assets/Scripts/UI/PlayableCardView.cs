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
        private void Start()
        {
            ServiceLocator.Instance.Loadout.SelectedCard.OnChangedValues += OnSelectedCardChanged;
        }

        private void OnSelectedCardChanged(Card oldValue, Card newValue)
        {
            // TEMP - Need better animation support
            if (newValue != SourceCard)
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

            ServiceLocator.Instance.Loadout.SelectedCard.Value = SourceCard;
        }

        protected override void OnDestroy()
        {
            ServiceLocator.Instance.Loadout.SelectedCard.OnChangedValues -= OnSelectedCardChanged;
            base.OnDestroy();
        }
    }
}
