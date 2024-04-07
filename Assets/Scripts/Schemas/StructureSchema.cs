using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Schemas
{
    [CreateAssetMenu]
    public class StructureSchema : Schema
    {
        public Gameplay.Structure Prefab;
        
        [SerializedDictionary("Event Type", "ActionEvent")]
        public SerializedDictionary<Action.EventType, ActionEvent> ActionByType;
    }
}
