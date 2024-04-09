using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu]
    public class ActionStateInt : Action
    {
        public string Key;
        public int Delta;
        
        public override void Invoke(Invoker.Context context)
        {
            if (!context.Invoker.State.Ints.TryAdd(Key, Delta))
            {
                context.Invoker.State.Ints[Key] += Delta;
            }
        }
    }
}
