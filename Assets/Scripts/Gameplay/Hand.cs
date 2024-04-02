using System.Collections.Generic;
using Gameplay.Cards;
using UnityEngine;

namespace Gameplay
{
    public class Hand
    {
        public int CardCount => m_cards.Count;
        
        private List<Card> m_cards = new List<Card>();

        /// <summary>
        /// Attempts to add the card to the hand. Returns if this was successful or not.
        /// </summary>
        public bool AddCard(Card card)
        {
            m_cards.Add(card);
            return true;
        }

        public Card ChooseRandomCard()
        {
            if (m_cards.Count == 0)
            {
                return null;
            }

            int cardIndex = Random.Range(0, m_cards.Count);
            Card card = m_cards[cardIndex];
            return card;
        }

        public bool RemoveCard(Card card)
        {
            for (var i = m_cards.Count - 1; i >= 0; i--)
            {
                if (m_cards[i] == card)
                {
                    m_cards.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }
    }
}
