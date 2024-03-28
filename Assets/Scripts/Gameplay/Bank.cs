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
        public delegate void OnResourceUpdate(Schemas.Resource resource, int currentTotal);
        public OnResourceUpdate OnResourceUpdateEvent;
        
        private Dictionary<Schemas.Resource, int> m_resources = new Dictionary<Resource, int>();
        
        public void Initialize()
        {
            // Dependencies: Schemas
            var resourceSchemas = ServiceLocator.Instance.Schemas.Resources;
            foreach (var resourceSchema in resourceSchemas)
            {
                m_resources.Add(resourceSchema, 0);
            }
        }

        public void DeltaResource(Schemas.Resource resource, int amount)
        {
            m_resources[resource] += amount;
            OnResourceUpdateEvent?.Invoke(resource, m_resources[resource]);
        }
    }
}
