using System;
using Gameplay;

namespace Schemas.Checks
{
    [Serializable]
    public abstract class Check : Schema
    {
        public enum NumericComparison
        {
            Less,
            LessOrEqual,
            Equal,
            GreaterOrEqual,
            Greater
        }
        
        public abstract bool IsValid(Invoker.Context context);
    }
}
