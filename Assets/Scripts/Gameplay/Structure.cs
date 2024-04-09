using GameStates;
using UnityEngine;
using Utility;
using Action = Schemas.Action;

namespace Gameplay
{
    public class Structure : MonoBehaviour, IInvoker
    {
        [HideInInspector] public Schemas.StructureSchema Schema;
        
        public Invoker Invoker { get; private set; } = new Invoker();

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

            if (!Invoker.AreConditionsMet(Invoker.EventType.OnGeneration))
            {
                return;
            }
            
            Invoker.TryInvokeActions(Invoker.EventType.OnGeneration);
        }

        public void SetSchema(Schemas.StructureSchema schema)
        {
            Schema = schema;
            Invoker.Initialize(Schema);
        }
    }
}
