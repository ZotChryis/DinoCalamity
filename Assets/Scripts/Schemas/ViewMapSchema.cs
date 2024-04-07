using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Schemas
{
    [CreateAssetMenu]
    public class ViewMapSchema : Schema
    {
        public enum ViewType
        {
            DiscoverCardsView
        }

        [SerializedDictionary("View Type", "ViewConfigs")]
        public SerializedDictionary<ViewType, ViewSchema> ViewConfigs;
    }
}
