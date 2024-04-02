using UnityEngine;

namespace Schemas
{
    public class Schema : ScriptableObject
    {
        public enum ProductionStatus
        {
            Debug,
            InDevelopment,
            Shippable
        }

        public ProductionStatus Status;
    }
}
