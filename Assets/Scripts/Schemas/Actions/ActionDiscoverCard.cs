using Gameplay;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu(menuName = "Action/ActionDiscoverCard")]
    public class ActionDiscoverCard : Action
    {
        public enum DiscoverTypeEnum
        {
            AddToDeck = 1,
            AddToHand = 2,
            RemoveFromDeck = 3
        }
        
        [SerializeField] private ViewSchema viewConfig;
        [SerializeField] public DiscoverTypeEnum DiscoverType;
        [SerializeField] public List<CardTypeEnum> CardSetsToDiscoverFrom;
        [SerializeField] public int DiscoverCardCount;
        [SerializeField] public List<CardSchema> OverrideCardOptions;

        public override void Invoke(Invoker.Context context)
        {
            var view = ServiceLocator.Instance.UIDisplayProcessor.TryShowView(viewConfig);
            var discoverCardView = view as DiscoverCardsView;
            if (discoverCardView == null)
            {
                return;
            }

            discoverCardView.SetData(this);
        }
    }
}
