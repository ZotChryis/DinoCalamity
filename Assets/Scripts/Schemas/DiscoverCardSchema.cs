using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu]
    public class DiscoverCardSchema : Schema
    {
        public enum DiscoverTypeEnum
        {
            AddToDeck = 1,
            AddToHand = 2,
            RemoveFromDeck = 3
        }

        public DiscoverTypeEnum DiscoverType;
        public List<CardSchema> CardOptions;
        public int DiscoverCardCount;
    }
}