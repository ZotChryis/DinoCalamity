using UnityEngine;

namespace Schemas
{
    /// <summary>
    /// The data definition for a Loadout.
    /// Create new entries via the asset create menu.
    /// </summary>
    [CreateAssetMenu]
    public class Loadout : ScriptableObject
    {
        public string Name;
        public int HandSize;
        public Card[] Deck;
    }
}
