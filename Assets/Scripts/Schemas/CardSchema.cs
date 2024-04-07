using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Schemas
{
    /// <summary>
    /// The data definition for a card.
    /// Create new entries via the asset create menu.
    /// </summary>
    [CreateAssetMenu]
    public class CardSchema : Schema
    {
        public string Name;
        public Sprite Icon;

        [SerializedDictionary("Event Type", "ActionEvent")]
        public SerializedDictionary<Action.EventType, ActionEvent> ActionByType;
    }
}