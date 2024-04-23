using UnityEngine;

namespace Schemas
{
    /// <summary>
    /// The data definition for a LoadoutSchema.
    /// Create new entries via the asset create menu.
    /// </summary>
    [CreateAssetMenu]
    public class LoadoutSchema : Schema
    {
        public string Name;
        public int HandSize;
        public CardSchema[] Deck;
    }
}
