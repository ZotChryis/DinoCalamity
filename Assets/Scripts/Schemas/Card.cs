using AYellowpaper.SerializedCollections;
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
        public enum EventType
        {
            OnDraw,
            OnDiscard,
            OnShuffle,
            OnPlay
        }
        
        public string Name;
        public Sprite Icon;
        
        [SerializedDictionary("Event Type", "Actions")]
        public SerializedDictionary<EventType, Action[]> Actions;
    }
}