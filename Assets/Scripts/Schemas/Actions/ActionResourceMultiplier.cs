using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu]
    public class ActionResourceMultiplier : Action
    {
        public ResourceSchema Resource;
        public float Multiplier;
        
        public override void Invoke(Invoker.Context context)
        {
            ServiceLocator.Instance.Bank.DeltaMultiplier(Resource, Multiplier);
        }
    }
}
