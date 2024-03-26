namespace Gameplay.Cards
{
    /// <summary>
    /// STUB
    /// Base class for all cards.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// The data for this card.
        /// </summary>
        public readonly Schemas.Card Data;
        
        public Card(Schemas.Card data)
        {
            Data = data;
        }
    }
}
