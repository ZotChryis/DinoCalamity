using System.Collections.Generic;
using Gameplay.Cards;

namespace Gameplay
{
    public class Hand
    {
        private List<Card> m_cards = new List<Card>();

        /// <summary>
        /// Attempts to add the card to the hand. Returns if this was successful or not.
        /// </summary>
        public bool AddCard(Card card)
        {
            m_cards.Add(card);
            return true;
        }
    }
}
