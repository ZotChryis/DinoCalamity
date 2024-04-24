using Gameplay;
using UnityEngine;

namespace Schemas.Checks
{
    /// <summary>
    /// Use this check to determine the relationship with the current turn counter.
    /// </summary>
    [CreateAssetMenu(menuName = "Check/CheckTurn")]
    public class CheckTurn : Check
    {
        public int Turn;
        public NumericComparison Comparison;
        
        public override bool IsValid(Invoker.Context context)
        {
            int turn = ServiceLocator.Instance.GameManager.Turn.Value;
            switch (Comparison)
            {
                case NumericComparison.Less:
                    return turn < Turn;
                case NumericComparison.LessOrEqual:
                    return turn <= Turn;
                case NumericComparison.Equal:
                    return turn == Turn;
                case NumericComparison.GreaterOrEqual:
                    return turn >= Turn;
                case NumericComparison.Greater:
                    return turn > Turn;
            }

            return false;
        }
    }
}
