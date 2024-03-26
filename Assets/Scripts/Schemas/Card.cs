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
        public enum ActionTiming
        {
            OnPlay
        }
        
        public string Name;
        public Sprite Icon;

        // todo: split these up for onplay, ondiscard, etc
        public Action[] Actions;
    }
}