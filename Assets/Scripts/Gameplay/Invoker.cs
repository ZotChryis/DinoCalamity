using System.Collections.Generic;
using Schemas;
using UnityEditor.Build;

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
        }
        
        public enum EventType
        {
            // These events pertain to the particular cards. They are only invoked on the card in which this 
            // event occurs on. For example, a card can trigger CardOnDraw effects when it itself is drawn, but 
            // cannot use this event for other cards being drawn. For that, use the OnDrawGlobal event
            CardOnDraw,
            CardOnDiscard,
            CardOnShuffle,
            CardOnPlay,
            
            // These events only work on buildings
            OnGeneration,       //  Need to migrate to Global
            
            // These events pertain to any other context.
            GlobalOnEndTurn,
        }
        
        public InvokerState State { get; private set; } = new InvokerState();
        protected InvokerSchema Schema;

        public void Initialize(InvokerSchema schema)
        {
            Schema = schema;

            ServiceLocator.Instance.GameManager.OnTurnEndEvent += OnTurnEnded;
        }

        public void Destroy()
        {
            ServiceLocator.Instance.GameManager.OnTurnEndEvent -= OnTurnEnded;
        }

        private void OnTurnEnded()
        {
            if (!AreConditionsMet(EventType.GlobalOnEndTurn))
            {
                return;
            }
            
            TryInvokeActions(EventType.GlobalOnEndTurn);
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
                Invoker = this,
                SelectedTile = ServiceLocator.Instance.Loadout.SelectedTile.Value
            };
        }
    }
}
