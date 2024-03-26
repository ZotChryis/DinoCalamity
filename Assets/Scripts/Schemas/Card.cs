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
        public string Name;
        public Sprite Icon;
        public Action[] Actions;
    }
}