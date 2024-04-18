using System.Collections.Generic;
using Schemas;

namespace Gameplay
{
    /// <summary>
    /// STUB - Class to hold resources that the player owns.
    /// </summary>
    public class Bank
    {
        /// <summary>
        /// This event occurs when the amount of a resource changes.
        /// </summary>
        public delegate void OnResourceUpdate(Schemas.ResourceSchema resource, int currentTotal);
        public OnResourceUpdate OnResourceUpdateEvent;

        private Dictionary<Schemas.ResourceSchema, float> m_resourceMultipliers = new Dictionary<ResourceSchema, float>();
        private Dictionary<Schemas.ResourceSchema, int> m_resources = new Dictionary<ResourceSchema, int>();
        
        public void Initialize()
        {
            // Dependencies: Schemas
            var resourceSchemas = ServiceLocator.Instance.Schemas.Resources;
            foreach (var resourceSchema in resourceSchemas)
            {
                m_resources.Add(resourceSchema, 0);
                m_resourceMultipliers.Add(resourceSchema, 0f);
            }
        }

        public void DeltaResource(Schemas.ResourceSchema resource, int amount)
        {
            float multiplier = m_resourceMultipliers[resource];
            amount += (int)(multiplier * amount);
            
            m_resources[resource] += amount;
            OnResourceUpdateEvent?.Invoke(resource, m_resources[resource]);
        }

        public void DeltaMultiplier(Schemas.ResourceSchema resource, float multiplier)
        {
            m_resourceMultipliers[resource] += multiplier;
        }

        public int GetAmount(ResourceSchema resource)
        {
            m_resources.TryGetValue(resource, out int value);
            return value;
        }
    }
}
