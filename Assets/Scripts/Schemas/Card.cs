using UnityEngine;

namespace Schemas
{
    /// <summary>
    /// The data definition for a card.
    /// Create new entries via the asset create menu.
    /// </summary>
    [CreateAssetMenu]
    public class Card : ScriptableObject
    {
        // @Ryan: How do we differentiate between card types? Is an enum enough?
        // We'll need to map this somehow to a script
        public enum Type
        {
            Structure,
            Action
        }
        
        public string Name;
        public Sprite Icon;
    }
}