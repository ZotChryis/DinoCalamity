using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Schemas
{
    /// <summary>
    /// The data definition for a card.
    /// Create new entries via the asset create menu.
    /// </summary>
    [CreateAssetMenu]
    public class Card : Schema
    {
        public enum EventType
        {
            OnDraw,
            OnDiscard,
            OnShuffle,
            OnPlay
        }
        
        public string Name;
        public Sprite Icon;
        
        // STUB - I'd like to expand this concept further
        public bool PlayRequiresTile;
        
        [SerializedDictionary("Event Type", "Actions")]
        public SerializedDictionary<EventType, Action[]> Actions;
    }
}