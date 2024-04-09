using AYellowpaper.SerializedCollections;
using Gameplay;

namespace Schemas
{
    public class InvokerSchema : Schema
    {
        [SerializedDictionary("Event Type", "ActionEvent")]
        public SerializedDictionary<Invoker.EventType, ActionEvent[]> ActionsByType;
    }
}
