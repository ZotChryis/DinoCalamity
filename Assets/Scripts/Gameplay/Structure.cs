using GameStates;
using Schemas;
using UnityEngine;
using Utility;

namespace Gameplay
{
    public class Structure : MonoBehaviour
    {
        private Schemas.Structure m_schema;

        private void Awake()
        {
            // STUB - This needs to probably happen in some game manager
            ServiceLocator.Instance.StateMachine.OnStateChangedEvent += OnStateChanged;
        }

        private void OnStateChanged(IState state)
        {
            // Currently, structures only support invoking the OnGeneration actions
            if (state is not StateGeneration)
            {
                return;
            }

            InvokeActions(Action.EventType.OnGeneration);
        }

        public void SetSchema(Schemas.Structure schema)
        {
            m_schema = schema;
        }
        
        /// <summary>
        /// Invokes all the card's actions, in order, for the given event type.
        /// TODO: I think we can generalize this with Card?
        /// </summary>
        public void InvokeActions(Schemas.Action.EventType eventType)
        {
            if (!m_schema.Actions.TryGetValue(eventType, out var actions))
            {
                return;
            }
            
            for (var i = 0; i < actions.Length; i++)
            {
                actions[i].Invoke();
            }
        }
    }
}
