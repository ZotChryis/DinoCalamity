using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Schemas
{
    [CreateAssetMenu]
    public class ViewMap : ScriptableObject
    {
        public enum ViewType
        {
            DiscoverCardsView
        }

        [SerializedDictionary("View Type", "ViewConfigs")]
        public SerializedDictionary<ViewType, ViewConfig> ViewConfigs;
    }
}
