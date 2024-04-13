using Gameplay;
using GameStates;
using UnityEngine;

namespace Schemas.Actions
{
    /// <summary>
    /// Use this action to force a loss.
    /// </summary>
    [CreateAssetMenu]
    public class ActionLose : Action
    {
        public override void Invoke(Invoker.Context context)
        {
            ServiceLocator.Instance.GameManager.StateMachine.ChangeState(new StateLoss());
        }
    }
}
