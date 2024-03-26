using System.Collections.Generic;
using Gameplay.Cards;
using UnityEngine.Assertions;

namespace Gameplay
{
    /// <summary>
    /// STUB
    /// </summary>
    public class Deck
    {
        /// <summary>
        /// An ordered list of the cards in the deck. This can be thought of as a Queue, but we use a list
        /// because we will routinely augment the order through actions.
        /// </summary>
        private List<Card> m_cards = new List<Card>();

        /// <summary>
        /// Attempts to add a card to the deck. Returns whether or not the addition was successful.
        /// </summary>
        public bool AddCard(Card card)
        {
            m_cards.Add(card);
            return true;
        }

        /// <summary>
        /// Returns the card at the very top of the deck. It is removed from the deck.
        /// </summary>
        /// <returns></returns>
        public Card Pop()
        {
            Assert.IsFalse(IsEmpty());
            Card card = m_cards[0];
            m_cards.RemoveAt(0);
            return card;
        }

        /// <summary>
        /// Returns whether the deck is empty or not.
        /// </summary>
        public bool IsEmpty()
        {
            return m_cards.Count == 0;
        }
    }
}
