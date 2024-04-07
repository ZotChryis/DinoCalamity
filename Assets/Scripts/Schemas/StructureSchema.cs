using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Schemas
{
    [CreateAssetMenu]
    public class StructureSchema : Schema
    {
        public Gameplay.Structure Prefab;
        
        [SerializedDictionary("Event Type", "Actions")]
        public SerializedDictionary<Action.EventType, Action[]> Actions;
    }
}
