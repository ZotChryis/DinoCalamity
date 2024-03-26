using System.Collections.Generic;
using UnityEngine;

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
        }

        private void OnDraw(Gameplay.Cards.Card card)
        {
            var uiCard = Instantiate(m_card, m_content);
            uiCard.SetData(card.Data);

            m_cards.Add(card, uiCard);
        }
    }
}
