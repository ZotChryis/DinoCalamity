using Gameplay;
using UnityEngine;

namespace Schemas.Checks
{
    /// <summary>
    /// Use this check to compare the total resource count for the given resource.
    /// TODO: Merge this and CheckStateInt with comparisons?
    /// </summary>
    [CreateAssetMenu(menuName = "Check/CheckBank")]
    public class CheckBank : Check
    {
        public global::Schemas.ResourceSchema Resource;
        public int Value;
        public NumericComparison Comparison;
    
        public override bool IsValid(Invoker.Context context)
        {
            int value = ServiceLocator.Instance.Bank.GetAmount(Resource);

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
