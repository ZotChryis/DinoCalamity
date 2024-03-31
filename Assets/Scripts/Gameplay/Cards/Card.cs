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

        /// <summary>
        /// Returns whether or not the card's play conditions are met. For example, some cards require targetting
        /// an empty World tile, or a tile with a building, or nothing at all.
        /// </summary>
        public bool ArePlayConditionsMet()
        {
            // STUB - I'd like to expand this concept further
            if (Data.PlayRequiresTile)
            {
                return ServiceLocator.Instance.Player.SelectedTile.Value != null;
            }
            
            return true;
        }
    }
}
