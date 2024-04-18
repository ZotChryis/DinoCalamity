using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu]
    public class ActionVision : Action
    {
        public override void Invoke(Invoker.Context context)
        {
            ServiceLocator.Instance.GameManager.Calamity.TryTriggerVision();
        }
    }
}
