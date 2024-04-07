using Schemas.Checks;

namespace Gameplay.Cards
{
    /// <summary>
    /// STUB
    /// Base class for all cards.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// The schema for this card.
        /// </summary>
        public readonly Schemas.CardSchema Schema;
        
        public Card(Schemas.CardSchema schema)
        {
            Schema = schema;
        }

        /// <summary>
        /// Invokes all the card's actions, in order, for the given event type.
        /// </summary>
        public void InvokeActions(Schemas.Action.EventType eventType)
        {
            if (!Schema.ActionByType.TryGetValue(eventType, out var cardEvents))
            {
                return;
            }
            
            for (var i = 0; i < cardEvents.Actions.Length; i++)
            {
                cardEvents.Actions[i].Invoke();
            }
        }

        /// <summary>
        /// Returns whether or not the card's conditions for the event are met.
        /// For example, some cards require targetting an empty World tile, or a tile with a building, etc.
        /// </summary>
        public bool AreConditionsMet(Schemas.Action.EventType eventType)
        {
            if (!Schema.ActionByType.TryGetValue(eventType, out var cardEvents))
            {
                return true;
            }

            Check.Context context = new Check.Context()
            {
                SelectedTile = ServiceLocator.Instance.Loadout.SelectedTile.Value
            };

            for (var i = 0; i < cardEvents.Checks.Length; i++)
            {
                if (!cardEvents.Checks[i].IsValid(context))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
