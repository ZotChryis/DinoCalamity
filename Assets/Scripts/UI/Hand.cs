using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class Hand : MonoBehaviour
    {
        [SerializeField] private Card m_card;
        [SerializeField] private Transform m_content;

        private Dictionary<Gameplay.Cards.Card, Card> m_cards = new();

        private void Start()
        {
            ServiceLocator.Instance.Player.OnDrawEvent += OnDraw;
            ServiceLocator.Instance.Player.OnDiscardEvent += OnDiscard;
        }

        private void Update()
        {
            // TEMP: Whenever the user right clicks, we'll deselect whatever card 
            //       This should probably live somewhere else in some input manager.
            if (Input.GetMouseButtonDown(1))
            {
                ServiceLocator.Instance.Player.SelectedCard.Value = null;
                ServiceLocator.Instance.Player.SelectedTile.Value = null;
            }
        }

        private void OnDraw(Gameplay.Cards.Card card)
        {
            var uiCard = Instantiate(m_card, m_content);
            uiCard.SetSource(card);

            m_cards.Add(card, uiCard);
        }
        
        private void OnDiscard(Gameplay.Cards.Card card)
        {
            if (!m_cards.ContainsKey(card))
            {
                return;
            }
            
            Card uiCard = m_cards[card];
            uiCard.Dispose();
            m_cards.Remove(card);
        }
    }
}
