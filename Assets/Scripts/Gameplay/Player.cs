using Gameplay.Cards;

namespace Gameplay
{
    /// <summary>
    /// STUB
    /// </summary>
    public class Player
    {
        /// <summary>
        /// This event occurs when the card is drawn from the player's deck to the player's hand.
        /// </summary>
        public delegate void OnDraw(Card card);
        public OnDraw OnDrawEvent;
    
        /// <summary>
        /// This event occurs when the player commits playing the card from their hand to the map.
        /// </summary>
        public delegate void OnPlay(Card card);
        public OnPlay OnPlayEvent;
        
        /// <summary>
        /// This event occurs when the card enters the discard pile in any way.
        /// </summary>
        public delegate void OnDiscard(Card card);
        public OnDiscard OnDiscardEvent;
        
        /// <summary>
        /// This event occurs when the card is goes from anywhere back into the player's deck.
        /// </summary>
        public delegate void OnShuffle(Card card);
        public OnShuffle OnShuffleEvent;

        
        /// Should Deck/Hand/Discard just be lists within Player.cs?
        /// What do we lose/gain from separating them into bespoke classes?
        private Deck m_deck = new Deck();
        private Hand m_hand = new Hand();

        /// <summary>
        /// Adds a card to the deck. Returns whether the addition was successful.
        /// </summary>
        public bool ShuffleCard(Card card)
        {
            if (!m_deck.AddCard(card))
            {
                return false;
            }

            OnShuffleEvent?.Invoke(card);
            return true;
        }

        /// <summary>
        /// Draws a card from the deck. Returns whether a draw was made.
        /// </summary>
        public bool Draw()
        {
            if (m_deck.IsEmpty())
            {
                return false;
            }

            Card card = m_deck.Pop();
            m_hand.AddCard(card);
            OnDrawEvent?.Invoke(card);
            return true;
        }
    }
}
