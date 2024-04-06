using Schemas;
using Utility.Observable;
using Card = Gameplay.Cards.Card;

namespace Gameplay
{
    /// <summary>
    /// STUB
    /// </summary>
    public class Loadout
    {
        /// <summary>
        /// This event occurs when the card is drawn from the loadout's deck to the player's hand.
        /// </summary>
        public delegate void OnDraw(Card card);
        public OnDraw OnDrawEvent;
    
        /// <summary>
        /// This event occurs when the loadout commits playing the card from their hand to the map.
        /// </summary>
        public delegate void OnPlay(Card card);
        public OnPlay OnPlayEvent;
        
        /// <summary>
        /// This event occurs when the card enters the discard pile in any way.
        /// </summary>
        public delegate void OnDiscard(Card card);
        public OnDiscard OnDiscardEvent;
        
        /// <summary>
        /// This event occurs when the card is goes from anywhere back into the loadout's deck.
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
        public readonly Deck Discard = new Deck();

        private Schemas.Loadout m_schema; 
        private Card m_selectedCard;

        public Loadout()
        {
            Deck.CardCount.OnChanged += OnDeckCountChanged;
        }
        
        public void Initialize(Schemas.Loadout loadout)
        {
            // Start the user's deck with the pre-selected kit
            m_schema = loadout;
            foreach (Schemas.Card card in m_schema.Deck)
            {
                ShuffleCard(new Card(card));
            }
        }

        /// <summary>
        /// Adds a card to the deck. Returns whether the addition was successful.
        /// Shuffle in our game means "add to deck". Open to other names...
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
            card.InvokeActions(Schemas.Action.EventType.OnDraw);
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

            Hand.RemoveCard(card);
            card.InvokeActions(Schemas.Action.EventType.OnDiscard);
            Discard.AddCard(card);
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
            
            Hand.RemoveCard(SelectedCard.Value);
            Discard.AddCard(SelectedCard.Value);
            OnDiscardEvent?.Invoke(SelectedCard.Value);
            
            SelectedCard.Value.InvokeActions(Schemas.Action.EventType.OnPlay);
            OnPlayEvent?.Invoke(SelectedCard.Value);
            
            SelectedCard.Value = null;
            SelectedTile.Value = null;
        }
        
        
        // STUB - This should live in a game state manager of some sort?
        private void OnDeckCountChanged()
        {
            if (Deck.CardCount.Value != 0)
            {
                return;
            }
            
            Discard.Shuffle();
            while (!Discard.IsEmpty())
            {
                Deck.AddCard(Discard.Pop());
            }
        }

        public void DrawUntilFull()
        {
            while (Hand.CardCount < m_schema.HandSize && !Deck.IsEmpty())
            {
                Draw();
            }
        }
    }
}
