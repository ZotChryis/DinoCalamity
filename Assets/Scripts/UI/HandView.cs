using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace UI
{
    public class HandView : MonoBehaviour
    {
        [SerializeField] private PlayableCardView m_card;
        [SerializeField] private Transform m_content;

        private Dictionary<Card, PlayableCardView> m_cards = new();

        private void Awake()
        {
            ServiceLocator.Instance.Loadout.Hand.CardCount.OnChanged += OnHandUpdated;
        }

        private void Update()
        {
            // TEMP: Whenever the user right clicks, we'll deselect whatever card 
            //       This should probably live somewhere else in some input manager.
            if (Input.GetMouseButtonDown(1))
            {
                ServiceLocator.Instance.Loadout.SelectedCard.Value = null;
                ServiceLocator.Instance.Loadout.SelectedTile.Value = null;
            }
        }

        private void OnHandUpdated()
        {
            ClearViews();
            var hand = ServiceLocator.Instance.Loadout.Hand;
            foreach(var card in hand.Cards)
            {
                AddCardToHand(card);
            }
        }

        private void ClearViews()
        {
            foreach(var view in m_cards)
            {
                Destroy(view.Value.gameObject);
            }
            m_cards.Clear();
        }

        private void AddCardToHand(Card card)
        {
            var uiCard = Instantiate(m_card, m_content);
            uiCard.SetData(card);

            m_cards.Add(card, uiCard);
        }
    }
}
