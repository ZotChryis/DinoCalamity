namespace Gameplay.Cards
{
    /// <summary>
    /// STUB
    /// Base class for all cards.
    /// </summary>
    public abstract class Card
    {
        /// <summary>
        /// This event occurs when the card is drawn from the player's deck to the player's hand.
        /// </summary>
        public delegate void OnDraw();
    
        /// <summary>
        /// This event occurs when the player commits playing the card from their hand to the map.
        /// </summary>
        public delegate void OnPlay();
    
        /// <summary>
        /// This event occurs when the card enters the discard pile in any way.
        /// </summary>
        public delegate void OnDiscard();
    
        /// <summary>
        /// This event occurs when the card is goes from anywhere back into the player's deck.
        /// </summary>
        public delegate void OnShuffle();
    }
}
