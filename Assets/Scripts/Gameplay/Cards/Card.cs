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
        
        /// <summary>
        /// Invokes all the card's actions, in order, for the given event type.
        /// </summary>
        public void InvokeActions(Schemas.Card.EventType eventType)
        {
            if (!Data.Actions.TryGetValue(eventType, out var actions))
            {
                return;
            }
            
            for (var i = 0; i < actions.Length; i++)
            {
                actions[i].Invoke();
            }
        }
    }
}
