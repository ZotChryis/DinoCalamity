using System.Collections.Generic;
using PlayableCardView = UI.PlayableCardView;

namespace Gameplay
{
    /// <summary>
    /// STUB
    /// </summary>
    public class Discard
    {
        public int CardCount => m_cards.Count;
        
        /// <summary>
        /// An ordered list of the cards in the discard pile.
        /// </summary>
        private List<PlayableCardView> m_cards = new List<PlayableCardView>();
    }
}
