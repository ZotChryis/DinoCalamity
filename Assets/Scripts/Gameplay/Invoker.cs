using System.Collections.Generic;
using GameStates;
using Schemas;
using Utility;

namespace Gameplay
{
    public interface IInvoker
    {
        Invoker Invoker { get; }
    }

    public class Invoker
    {
        public class InvokerState
        {
            public Dictionary<string, int> Ints = new Dictionary<string, int>();
        }

        public class Context
        {
            public Invoker Invoker;
            public Gameplay.Tile SelectedTile;
            public object Owner;
            
            // TODO: Support this
            public Gameplay.Card SelectedCard;
        }
        
        public enum EventType
        {
            // These events happen when an Invoker is created and a schema is applied. Happens once!
            OnApply = 7,
            
            // These events are only invoked on card invokers.
            CardOnDraw = 0,
            CardOnDiscard = 1,
            CardOnShuffle = 2,
            CardOnPlay = 3,

            // These events occur only on tile invokers
            TileOnReveal = 8,
            
            // These events pertain to any other context.
            GlobalOnGeneration = 4,     
            GlobalOnEndTurn = 5,
            GlobalOnStartTurn = 6,
            GlobalOnTileReveal = 9,
        }
        
        public InvokerState State { get; private set; } = new InvokerState();
        protected InvokerSchema Schema;
        protected object Owner;

        public void Initialize(object owner, InvokerSchema schema)
        {
            Owner = owner;
            Schema = schema;

            ServiceLocator.Instance.GameManager.OnTurnEndEvent += OnTurnEnded;
            ServiceLocator.Instance.GameManager.OnTurnStartEvent += OnTurnStarted;
            ServiceLocator.Instance.StateMachine.OnStateChangedEvent += OnStateChanged;
            ServiceLocator.Instance.World.OnTileRevealEvent += OnTileReveald;
            
            TryInvokeActions(EventType.OnApply);
        }

        public void Destroy()
        {
            ServiceLocator.Instance.GameManager.OnTurnEndEvent -= OnTurnEnded;
            ServiceLocator.Instance.GameManager.OnTurnStartEvent -= OnTurnStarted;
            ServiceLocator.Instance.StateMachine.OnStateChangedEvent -= OnStateChanged;
            ServiceLocator.Instance.World.OnTileRevealEvent -= OnTileReveald;
        }
        
        private void OnStateChanged(IState state)
        {
            // Currently, structures only support invoking the GlobalOnGeneration actions
            if (state is StateGeneration)
            {
                if (AreConditionsMet(Invoker.EventType.GlobalOnGeneration))
                {
                    TryInvokeActions(Invoker.EventType.GlobalOnGeneration);
                }
            }
        }

        private void OnTurnEnded()
        {
            if (!AreConditionsMet(EventType.GlobalOnEndTurn))
            {
                return;
            }
            
            TryInvokeActions(EventType.GlobalOnEndTurn);
        }
        
        private void OnTurnStarted()
        {
            if (!AreConditionsMet(EventType.GlobalOnStartTurn))
            {
                return;
            }
            
            TryInvokeActions(EventType.GlobalOnStartTurn);
        }
        
        private void OnTileReveald(Tile tile)
        {
            
        }

        public void TryInvokeActions(EventType eventType)
        {
            if (!Schema.ActionsByType.TryGetValue(eventType, out var cardEvents))
            {
                return;
            }

            for (var i = 0; i < cardEvents.Length; i++)
            {
                bool checksPassed = true;
                var checks = cardEvents[i].Checks;
                for (var checkIndex = 0; checkIndex < checks.Length; checkIndex++)
                {
                    if (!checks[checkIndex].IsValid(GetContext()))
                    {
                        checksPassed = false;
                        break;
                    }
                }

                if (!checksPassed)
                {
                    continue;
                }

                var actions = cardEvents[i].Actions;
                for (var actionIndex = 0; actionIndex < actions.Length; actionIndex++)
                {
                    actions[actionIndex].Invoke(GetContext());
                }
            }
        }
        
        /// <summary>
        /// Returns whether or not the card's conditions for the event are met. Only one passing set is needed to be true.
        /// For example, some cards require targetting an empty World tile, or a tile with a building, etc.
        /// </summary>
        public bool AreConditionsMet(EventType eventType)
        {
            // If we have no actions for this type of event, we can assume say false.
            if (!Schema.ActionsByType.TryGetValue(eventType, out var cardEvents))
            {
                return false;
            }

            // If any card event is able to fire off of this event, then we can return true
            for (var i = 0; i < cardEvents.Length; i++)
            {
                bool isValid = true;
                var checks = cardEvents[i].Checks;
                for (var checkIndex = 0; checkIndex < checks.Length; checkIndex++)
                {
                    if (!checks[checkIndex].IsValid(GetContext()))
                    {
                        isValid = false;
                    }
                }

                if (isValid)
                {
                    return true;
                }
            }

            // No card events were valid
            return false;
        }

        private Context GetContext()
        {
            return new Context()
            {
                Owner = Owner,
                Invoker = this,
                SelectedTile = ServiceLocator.Instance.Loadout.SelectedTile.Value,
            };
        }
    }
}
