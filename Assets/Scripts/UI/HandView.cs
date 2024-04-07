using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class HandView : MonoBehaviour
    {
        [SerializeField] private PlayableCardView m_card;
        [SerializeField] private Transform m_content;

        private Dictionary<Gameplay.Cards.Card, PlayableCardView> m_cards = new();

        private void Start()
        {
            ServiceLocator.Instance.Loadout.OnDrawEvent += OnDraw;
            ServiceLocator.Instance.Loadout.OnDiscardEvent += OnDiscard;
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

        private void OnDraw(Gameplay.Cards.Card card)
        {
            var uiCard = Instantiate(m_card, m_content);
            uiCard.SetData(card);

            m_cards.Add(card, uiCard);
        }
        
        private void OnDiscard(Gameplay.Cards.Card card)
        {
            if (!m_cards.ContainsKey(card))
            {
                return;
            }
            
            PlayableCardView uiCard = m_cards[card];
            uiCard.Dispose();
            m_cards.Remove(card);
        }
    }
}
