using System.Collections.Generic;
using Gameplay;
using Schemas;
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
        private TextMeshProUGUI m_descriptionText;

        [SerializeField]
        private BaseCardView m_cardPrefab;

        [SerializeField]
        private Transform m_cardsParentTransform;

        private List<BaseCardView> m_cardViews = new List<BaseCardView>();
        private ActionDiscoverCard m_actionDiscoverCard;
        
        // Methods
        //
        public override void Teardown()
        {
            base.Teardown();
            ClearData();
        }

        public void SetData(ActionDiscoverCard actionDiscoverCard)
        {
            m_actionDiscoverCard = actionDiscoverCard;

            m_titleText.SetText(m_actionDiscoverCard.DisplayName);
            m_descriptionText.SetText(m_actionDiscoverCard.DisplayDescription);
            
            var allCards = new List<CardSchema>(ServiceLocator.Instance.Schemas.Cards);
            Algorithms.Shuffle(allCards);
            bool isOverridingSet = m_actionDiscoverCard.OverrideCardOptions != null &&
                                m_actionDiscoverCard.OverrideCardOptions.Count > 0;

            List<Card> cardsToDiscover = new List<Card>();
            switch (m_actionDiscoverCard.DiscoverType)
            {
                case ActionDiscoverCard.DiscoverTypeEnum.AddToDeck:
                case ActionDiscoverCard.DiscoverTypeEnum.AddToHand:
                    if (isOverridingSet)
                    {
                        foreach (var cardSchema in m_actionDiscoverCard.OverrideCardOptions)
                        {
                            cardsToDiscover.Add(new Card(cardSchema));
                        }
                    }
                    else
                    {
                        foreach (var card in allCards)
                        {
                            foreach (var cardTypeToDiscoverFrom in m_actionDiscoverCard.CardSetsToDiscoverFrom)
                            {
                                if (cardTypeToDiscoverFrom == card.CardType)
                                {
                                    cardsToDiscover.Add(new Card(card));
                                    break;
                                }
                            }
                        }
                    }
                    break;
                case ActionDiscoverCard.DiscoverTypeEnum.RemoveFromDeck:
                    cardsToDiscover = new List<Card>(ServiceLocator.Instance.Loadout.Deck.Cards);
                    break;
                default:
                    break;
            }

            Algorithms.Shuffle(cardsToDiscover);
            int cardDiscoverCount = System.Math.Min(m_actionDiscoverCard.DiscoverCardCount, cardsToDiscover.Count);
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
            m_actionDiscoverCard = null;
            m_titleText.SetText(string.Empty);
        }

        private void OnCardViewPressed(BaseCardView cardView)
        {
            switch (m_actionDiscoverCard.DiscoverType)
            {
                case ActionDiscoverCard.DiscoverTypeEnum.AddToDeck:
                    ServiceLocator.Instance.Loadout.Deck.ShuffleCard(cardView.SourceCard);
                    break;
                case ActionDiscoverCard.DiscoverTypeEnum.AddToHand:
                    ServiceLocator.Instance.Loadout.Hand.AddCard(cardView.SourceCard);
                    break;
                case ActionDiscoverCard.DiscoverTypeEnum.RemoveFromDeck:
                    ServiceLocator.Instance.Loadout.Deck.RemoveCard(cardView.SourceCard);
                    break;
                default:
                    break;
            }

            ServiceLocator.Instance.UIDisplayProcessor.PopView();
        }

    }
}
