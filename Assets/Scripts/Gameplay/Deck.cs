using System.Collections.Generic;
using Gameplay.Cards;

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
    }
}
