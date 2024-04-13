namespace Gameplay
{
    /// <summary>
    /// Base class for all cards.
    /// </summary>
    public class Card : IInvoker
    {
        /// <summary>
        /// The schema for this card.
        /// </summary>
        public readonly Schemas.CardSchema Schema;

        public Invoker Invoker { get; private set; } = new Invoker();
        
        public Card(Schemas.CardSchema schema)
        {
            Schema = schema;
            Invoker.Initialize(this, Schema);
        }
    }
}
