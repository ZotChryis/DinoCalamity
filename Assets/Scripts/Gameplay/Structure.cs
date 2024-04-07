using GameStates;
using Schemas.Checks;
using UnityEngine;
using Utility;
using Action = Schemas.Action;

namespace Gameplay
{
    // TODO: I think we can generalize this with Card?
    public class Structure : MonoBehaviour
    {
        [HideInInspector] public Schemas.StructureSchema Schema;

        private void Awake()
        {
            ServiceLocator.Instance.StateMachine.OnStateChangedEvent += OnStateChanged;
        }

        private void OnDestroy()
        {
            ServiceLocator.Instance.StateMachine.OnStateChangedEvent -= OnStateChanged;
        }

        private void OnStateChanged(IState state)
        {
            // Currently, structures only support invoking the OnGeneration actions
            if (state is not StateGeneration)
            {
                return;
            }

            if (!AreConditionsMet(Action.EventType.OnGeneration))
            {
                return;
            }
            
            InvokeActions(Action.EventType.OnGeneration);
        }

        public void SetSchema(Schemas.StructureSchema schema)
        {
            Schema = schema;
        }

        /// <summary>
        /// Invokes all the card's actions, in order, for the given event type.
        /// </summary>
        public void InvokeActions(Schemas.Action.EventType eventType)
        {
            if (!Schema.ActionByType.TryGetValue(eventType, out var cardEvents))
            {
                return;
            }
            
            for (var i = 0; i < cardEvents.Actions.Length; i++)
            {
                cardEvents.Actions[i].Invoke();
            }
        }

        /// <summary>
        /// Returns whether or not the card's conditions for the event are met.
        /// For example, some cards require targetting an empty World tile, or a tile with a building, etc.
        /// </summary>
        public bool AreConditionsMet(Schemas.Action.EventType eventType)
        {
            if (!Schema.ActionByType.TryGetValue(eventType, out var cardEvents))
            {
                return true;
            }

            Check.Context context = new Check.Context()
            {
                SelectedTile = ServiceLocator.Instance.Loadout.SelectedTile.Value
            };

            for (var i = 0; i < cardEvents.Checks.Length; i++)
            {
                if (!cardEvents.Checks[i].IsValid(context))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
