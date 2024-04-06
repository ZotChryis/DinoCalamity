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
        public string Name;
        public Sprite Icon;
        
        // STUB - I'd like to expand this concept further
        public bool PlayRequiresTile;
        public bool PlayRequiresTileVision;
        
        [SerializedDictionary("Event Type", "Actions")]
        public SerializedDictionary<Action.EventType, Action[]> Actions;
    }
}