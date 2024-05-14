using System.Collections.Generic;
using GameStates;
using Schemas;
using UnityEngine.SceneManagement;
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
            public object Target;
            
            // todo: fully support this concept. currently only TILES do this, but in theory everything should have
            // something valid for this.
            public object Owner;
        }
        
        public enum Location
        {
            Selected,
            Owner,
            Target
        }
        
        public enum EventType
        {
            // These events happen when an Selected is created and a schema is applied. Happens once!
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

            if (Schema.ActionsByType.ContainsKey(EventType.GlobalOnEndTurn))
            {
                ServiceLocator.Instance.GameManager.OnTurnEndEvent += OnTurnEnded;
            }
            if (Schema.ActionsByType.ContainsKey(EventType.GlobalOnStartTurn))
            {
                ServiceLocator.Instance.GameManager.OnTurnStartEvent += OnTurnStarted;
            }
            if (Schema.ActionsByType.ContainsKey(EventType.GlobalOnGeneration))
            {
                ServiceLocator.Instance.StateMachine.OnStateChangedEvent += OnStateChanged;
            }
            if (Schema.ActionsByType.ContainsKey(EventType.GlobalOnTileReveal))
            {
                ServiceLocator.Instance.World.OnTileRevealEvent += OnTileRevealed;
            }
            
            TryInvokeActions(EventType.OnApply, GetDefaultContext());
        }

        public void Destroy()
        {
            ServiceLocator.Instance.GameManager.OnTurnEndEvent -= OnTurnEnded;
            ServiceLocator.Instance.GameManager.OnTurnStartEvent -= OnTurnStarted;
            ServiceLocator.Instance.StateMachine.OnStateChangedEvent -= OnStateChanged;
            ServiceLocator.Instance.World.OnTileRevealEvent -= OnTileRevealed;
        }
        
        private void OnStateChanged(IState state)
        {
            // Currently, structures only support invoking the GlobalOnGeneration actions
            if (state is StateGeneration)
            {
                if (AreConditionsMet(Invoker.EventType.GlobalOnGeneration, GetDefaultContext()))
                {
                    TryInvokeActions(Invoker.EventType.GlobalOnGeneration, GetDefaultContext());
                }
            }
        }

        private void OnTurnEnded()
        {
            if (!AreConditionsMet(EventType.GlobalOnEndTurn, GetDefaultContext()))
            {
                return;
            }
            
            TryInvokeActions(EventType.GlobalOnEndTurn, GetDefaultContext());
        }
        
        private void OnTurnStarted()
        {
            if (!AreConditionsMet(EventType.GlobalOnStartTurn, GetDefaultContext()))
            {
                return;
            }
            
            TryInvokeActions(EventType.GlobalOnStartTurn, GetDefaultContext());
        }
        
        private void OnTileRevealed(Tile tile)
        {
            var context = GetDefaultContext();
            context.Target = tile;
            
            if (!AreConditionsMet(EventType.GlobalOnTileReveal, context))
            {
                return;
            }
            
            TryInvokeActions(EventType.GlobalOnTileReveal, context);
        }

        public void TryInvokeActions(EventType eventType, Context context)
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
                    if (!checks[checkIndex].IsValid(context))
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
                    actions[actionIndex].Invoke(context);
                }
            }
        }
        
        /// <summary>
        /// Returns whether or not the card's conditions for the event are met. Only one passing set is needed to be true.
        /// For example, some cards require targetting an empty World tile, or a tile with a building, etc.
        /// </summary>
        public bool AreConditionsMet(EventType eventType, Context context)
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
                    if (!checks[checkIndex].IsValid(context))
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

        public Context GetDefaultContext()
        {
            return new Context()
            {
                Owner = Owner,
                Invoker = this
            };
        }
    }
}
