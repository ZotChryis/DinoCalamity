using System.Collections.Generic;
using Schemas;
using Utility.Observable;

namespace Gameplay
{
    /// <summary>
    /// STUB - Class to hold resources that the player owns.
    /// </summary>
    public class Bank : IResourceModifierContainer
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

        /// <summary>
        /// These are "global" multipliers applied to all non-faith resource types.
        /// </summary>
        private Dictionary<ResourceSchema.ResourceType, List<ResourceModifier>> m_resourceModifiers 
            = new Dictionary<ResourceSchema.ResourceType, List<ResourceModifier>>();
        
        
        /// <summary>
        /// The collection of all non-faith resources.
        /// </summary>
        private Dictionary<ResourceSchema.ResourceType, int> m_resources 
            = new Dictionary<ResourceSchema.ResourceType, int>();
        
        public void Initialize()
        {
            // Dependencies: Schemas
            var resourceSchemas = ServiceLocator.Instance.Schemas.Resources;
            foreach (var resourceSchema in resourceSchemas)
            {
                m_resources.Add(resourceSchema.Type, 0);
                m_resourceModifiers.Add(resourceSchema.Type, new List<ResourceModifier>());
            }
            
            // TODO: Clean this relationship up
            ServiceLocator.Instance.GameManager.OnTurnCleanupEvent += ((IResourceModifierContainer)this).HandTurnCleanup;
        }

        public void DeltaResource(
            ResourceSchema.ResourceType resourceType, 
            int amount,
            Dictionary<ResourceSchema.ResourceType, List<ResourceModifier>> externalMods = null
        ) {
            float flatBonus = 0f;
            float multBonus = 0f;

            List<ResourceModifier> mods = new List<ResourceModifier>();
            if (externalMods != null && externalMods.ContainsKey(resourceType))
            {
                mods.AddRange(externalMods[resourceType]);
            }
            mods.AddRange(m_resourceModifiers[resourceType]);
            
            foreach (var mod in mods)
            {
                switch (mod.Type)
                {
                    case ResourceModifier.ModifierType.Flat:
                        flatBonus += mod.Amount;
                        break;
                    case ResourceModifier.ModifierType.Multiplier:
                        multBonus += mod.Amount;
                        break;
                }
            }

            amount = (int) ((amount + flatBonus) * (1 + multBonus));
            
            m_resources[resourceType] += amount;
            OnResourceUpdateEvent?.Invoke(resourceType, m_resources[resourceType]);
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

        public Dictionary<ResourceSchema.ResourceType, List<ResourceModifier>> GetResourceModifiers()
        {
            return m_resourceModifiers;
        }
    }
}
