using System.Collections.Generic;
using Gameplay.Cards;
using UnityEngine.Assertions;
using Utility;
using Utility.Observable;

namespace Gameplay
{
    /// <summary>
    /// STUB
    /// </summary>
    public class Deck
    {
        public Observable<int> CardCount = new Observable<int>(0);
        
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
            CardCount.Value = m_cards.Count;
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
            CardCount.Value = m_cards.Count;
            return card;
        }

        /// <summary>
        /// Shuffles all current cards randomly.
        /// </summary>
        public void Shuffle()
        {
            Algorithms.Shuffle(m_cards);
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
