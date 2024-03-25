using System.Collections.Generic;
using Gameplay.Cards;

namespace Gameplay
{
    /// <summary>
    /// STUB
    /// Should Deck/Hand/Discard just be lists within Player.cs?
    /// What do we lose/gain from separating them into bespoke classes?
    /// </summary>
    public class Discard
    {
        /// <summary>
        /// An ordered list of the cards in the discard pile.
        /// </summary>
        private List<Card> m_cards = new List<Card>();
    }
}
