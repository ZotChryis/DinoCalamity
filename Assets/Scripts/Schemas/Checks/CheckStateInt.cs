using Gameplay;
using UnityEngine;

namespace Schemas.Checks
{
    [CreateAssetMenu]
    public class CheckStateInt : Check
    {
        public string Key;
        public int Value;
        public NumericComparison Comparison;
    
        public override bool IsValid(Invoker.Context context)
        {
            // Value defaults to 0 if not found
            context.Invoker.State.Ints.TryGetValue(Key, out int value);

            switch (Comparison)
            {
                case NumericComparison.Less:
                    return value < Value;
                case NumericComparison.LessOrEqual:
                    return value <= Value;
                case NumericComparison.Equal:
                    return value == Value;
                case NumericComparison.GreaterOrEqual:
                    return value >= Value;
                case NumericComparison.Greater:
                    return value > Value;
            }

            return false;
        }
    }
}
