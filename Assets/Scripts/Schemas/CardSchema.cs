using UnityEngine;

namespace Schemas
{
    /// <summary>
    /// The data definition for a card.
    /// Create new entries via the asset create menu.
    /// </summary>
    [CreateAssetMenu]
    public class CardSchema : InvokerSchema
    {
        public string Name;
        public string Description;
        public Sprite Icon;
    }
}