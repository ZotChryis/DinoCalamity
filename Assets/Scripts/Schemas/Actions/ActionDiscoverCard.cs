using UI;
using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu]
    public class ActionDiscoverCard : Action
    {
        [SerializeField] private ViewSchema viewConfig;
        [SerializeField] private DiscoverCardSchema CardDiscoverOptions;

        public override void Invoke()
        {
            var view = ServiceLocator.Instance.UIDisplayProcessor.TryShowView(viewConfig);
            var discoverCardView = view as DiscoverCardsView;
            if (discoverCardView == null)
            {
                return;
            }

            discoverCardView.SetData(CardDiscoverOptions);
        }
    }
}
