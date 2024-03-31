using Gameplay.Cards;
using Utility.Observable;

namespace Gameplay
{
    /// <summary>
    /// STUB
    /// </summary>
    public class Player
    {
        /// <summary>
        /// This event occurs when the card is drawn from the player's deck to the player's hand.
        /// </summary>
        public delegate void OnDraw(Card card);
        public OnDraw OnDrawEvent;
    
        /// <summary>
        /// This event occurs when the player commits playing the card from their hand to the map.
        /// </summary>
        public delegate void OnPlay(Card card);
        public OnPlay OnPlayEvent;
        
        /// <summary>
        /// This event occurs when the card enters the discard pile in any way.
        /// </summary>
        public delegate void OnDiscard(Card card);
        public OnDiscard OnDiscardEvent;
        
        /// <summary>
        /// This event occurs when the card is goes from anywhere back into the player's deck.
        /// </summary>
        public delegate void OnShuffle(Card card);
        public OnShuffle OnShuffleEvent;

        /// <summary>
        /// Central method of communicating what is the currently selected card. Anyone can change this, beware.
        /// </summary>
        public Observable<Card> SelectedCard = new Observable<Card>();
        
        /// <summary>
        /// Central method of communicating what is the currently selected tile. Anyone can change this, beware.
        /// TODO: Support selecting multiple tiles
        /// </summary>
        public Observable<Tile> SelectedTile = new Observable<Tile>();
        
        public readonly Deck Deck = new Deck();
        public readonly Hand Hand = new Hand();
        public readonly Discard Discard = new Discard();
        
        private Card m_selectedCard;
        
        /// <summary>
        /// Adds a card to the deck. Returns whether the addition was successful.
        /// </summary>
        public bool ShuffleCard(Card card)
        {
            if (!Deck.AddCard(card))
            {
                return false;
            }

            OnShuffleEvent?.Invoke(card);
            return true;
        }

        /// <summary>
        /// Draws a card from the deck. Returns whether a draw was made.
        /// </summary>
        public bool Draw()
        {
            if (Deck.IsEmpty())
            {
                return false;
            }

            Card card = Deck.Pop();
            Hand.AddCard(card);
            card.InvokeActions(Schemas.Card.EventType.OnDraw);
            OnDrawEvent?.Invoke(card);
            return true;
        }

        /// <summary>
        /// Discards a random card from the hand. Returns whether a draw was made.
        /// </summary>
        public bool DiscardRandomCard()
        {
            Card card = Hand.ChooseRandomCard();
            if (card == null)
            {
                return false;
            }

            Hand.Discard(card);
            card.InvokeActions(Schemas.Card.EventType.OnDiscard);
            OnDiscardEvent?.Invoke(card);
            return true;
        }
        
        // TEMP
        public void PlaySelectedCard()
        {
            // Protect against a rogue call.
            if (SelectedCard.Value == null)
            {
                return;
            }
            
            Hand.Discard(SelectedCard.Value);
            OnDiscardEvent?.Invoke(SelectedCard.Value);
            
            SelectedCard.Value.InvokeActions(Schemas.Card.EventType.OnPlay);
            OnPlayEvent?.Invoke(SelectedCard.Value);
            
            SelectedCard.Value = null;
            SelectedTile.Value = null;
        }
    }
}
