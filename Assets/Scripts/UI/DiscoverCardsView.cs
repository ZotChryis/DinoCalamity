using System.Collections.Generic;
using Gameplay.Cards;
using Schemas.Actions;
using TMPro;
using UnityEngine;
using Utility;

namespace UI
{
    public class DiscoverCardsView : View
    {
        // Variables
        //
        [SerializeField]
        private TextMeshProUGUI m_titleText;

        [SerializeField]
        private BaseCardView m_cardPrefab;

        [SerializeField]
        private Transform m_cardsParentTransform;

        private List<BaseCardView> m_cardViews = new List<BaseCardView>();
        private DiscoverCardSchema m_discoverCardsData;

      
        // Methods
        //
        public override void Setup()
        {
            base.Setup();
        }

        public override void Teardown()
        {
            base.Teardown();
            ClearData();
        }

        public override void Activate()
        {
            base.Activate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public void SetData(DiscoverCardSchema discoverCardsData)
        {
            m_discoverCardsData = discoverCardsData;

            List<Card> cardsToDiscover = new List<Card>();
            switch (m_discoverCardsData.DiscoverType)
            {
                case DiscoverCardSchema.DiscoverTypeEnum.AddToDeck:
                case DiscoverCardSchema.DiscoverTypeEnum.AddToHand:
                    foreach (var cardSchema in discoverCardsData.CardOptions)
                    {
                        cardsToDiscover.Add(new Card(cardSchema));
                    }
                    break;
                case DiscoverCardSchema.DiscoverTypeEnum.RemoveFromDeck:
                    cardsToDiscover = new List<Card>(ServiceLocator.Instance.Loadout.Deck.Cards);
                    break;
                default:
                    break;
            }

            Algorithms.Shuffle(cardsToDiscover);
            int cardDiscoverCount = System.Math.Min(discoverCardsData.DiscoverCardCount, cardsToDiscover.Count);
            for(int i = 0; i < cardDiscoverCount; i++)
            {
                var cardView = Instantiate(m_cardPrefab, m_cardsParentTransform);
                cardView.SetData(cardsToDiscover[i]);
                cardView.OnCardViewPressedEvent += OnCardViewPressed;
                m_cardViews.Add(cardView);
            }

        }

        public void ClearData()
        {
            foreach(var cardView in m_cardViews)
            {
                cardView.OnCardViewPressedEvent -= OnCardViewPressed;
                Destroy(cardView);
            }
            m_cardViews.Clear();
            m_discoverCardsData = null;
        }

        private void OnCardViewPressed(BaseCardView cardView)
        {
            switch (m_discoverCardsData.DiscoverType)
            {
                case DiscoverCardSchema.DiscoverTypeEnum.AddToDeck:
                    ServiceLocator.Instance.Loadout.Deck.RemoveCard(cardView.SourceCard);
                    break;
                case DiscoverCardSchema.DiscoverTypeEnum.AddToHand:
                    ServiceLocator.Instance.Loadout.Hand.AddCard(cardView.SourceCard);
                    break;
                case DiscoverCardSchema.DiscoverTypeEnum.RemoveFromDeck:
                    ServiceLocator.Instance.Loadout.Deck.RemoveCard(cardView.SourceCard);
                    break;
                default:
                    break;
            }

            ServiceLocator.Instance.UIDisplayProcessor.PopView();
        }

    }
}
