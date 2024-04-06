using System.Collections.Generic;
using Gameplay.Cards;
using UnityEngine;
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
        /// Attempts to add a card to back of the deck. Returns whether or not the addition was successful.
        /// </summary>
        public bool AddCard(Card card)
        {
            m_cards.Add(card);
            CardCount.Value = m_cards.Count;
            return true;
        }
        
        /// <summary>
        /// Attempts to add a card to the front of the deck. Returns whether or not the addition was successful.
        /// </summary>
        public bool AddCardFront(Card card)
        {
            m_cards.Insert(0, card);
            CardCount.Value = m_cards.Count;
            return true;
        }
        
        /// <summary>
        /// Attempts to add a card to the deck in a random location. Returns whether or not the addition was successful.
        /// </summary>
        public bool ShuffleCard(Card card)
        {
            m_cards.Insert(UnityEngine.Random.Range(0, m_cards.Count), card);
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

        /// <summary>
        /// Returns a random card from the deck.
        /// </summary>
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

        /// <summary>
        /// Removes the specified card from the deck. Returns if one was removed.
        /// </summary>
        public bool RemoveCard(Card card)
        {
            for (var i = m_cards.Count - 1; i >= 0; i--)
            {
                if (m_cards[i] == card)
                {
                    m_cards.RemoveAt(i);
                    CardCount.Value = m_cards.Count;
                    return true;
                }
            }

            return false;
        }
    }
}
