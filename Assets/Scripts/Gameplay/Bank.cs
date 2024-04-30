using System.Collections.Generic;
using Schemas;
using Utility.Observable;

namespace Gameplay
{
    /// <summary>
    /// STUB - Class to hold resources that the player owns.
    /// </summary>
    public class Bank
    {
        /// <summary>
        /// This event occurs when the amount of a resourceType changes.
        /// </summary>
        public delegate void OnResourceUpdate(ResourceSchema.ResourceType resourceType, int currentTotal);
        public OnResourceUpdate OnResourceUpdateEvent;

        /// <summary>
        /// The current amount of maximum faith.
        /// </summary>
        public Observable<int> FaithMax = new Observable<int>(0);

        private Dictionary<ResourceSchema.ResourceType, float> m_resourceMultipliers 
            = new Dictionary<ResourceSchema.ResourceType, float>();
        private Dictionary<ResourceSchema.ResourceType, int> m_resources 
            = new Dictionary<ResourceSchema.ResourceType, int>();
        
        public void Initialize()
        {
            // Dependencies: Schemas
            var resourceSchemas = ServiceLocator.Instance.Schemas.Resources;
            foreach (var resourceSchema in resourceSchemas)
            {
                m_resources.Add(resourceSchema.Type, 0);
                m_resourceMultipliers.Add(resourceSchema.Type, 0f);
            }
        }

        public void DeltaResource(Schemas.ResourceSchema resource, int amount)
        {
            DeltaResource(resource.Type, amount);
        }

        public void DeltaResource(ResourceSchema.ResourceType resourceType, int amount)
        {
            float multiplier = m_resourceMultipliers[resourceType];
            amount += (int)(multiplier * amount);
            
            m_resources[resourceType] += amount;
            OnResourceUpdateEvent?.Invoke(resourceType, m_resources[resourceType]);
        }

        public void DeltaMultiplier(Schemas.ResourceSchema resource, float multiplier)
        {
            DeltaMultiplier(resource.Type, multiplier);
        }
        
        public void DeltaMultiplier(ResourceSchema.ResourceType resourceType, float multiplier)
        {
            m_resourceMultipliers[resourceType] += multiplier;
        }

        public int GetAmount(ResourceSchema resource)
        {
            return GetAmount(resource.Type);
        }

        public int GetAmount(ResourceSchema.ResourceType resourceType)
        {
            m_resources.TryGetValue(resourceType, out int value);
            return value;
        }

        public void ReplinishFaith()
        {
            m_resources[ResourceSchema.ResourceType.Faith] = FaithMax.Value;
            OnResourceUpdateEvent?.Invoke(
                ResourceSchema.ResourceType.Faith, 
                m_resources[ResourceSchema.ResourceType.Faith]
            );
        }
    }
}
