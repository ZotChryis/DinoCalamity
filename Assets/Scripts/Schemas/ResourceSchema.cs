using UnityEngine;

namespace Schemas
{
    /// <summary>
    /// The data definition for a Resource.
    /// Create new entries via the asset create menu.
    /// </summary>
    [CreateAssetMenu]
    public class ResourceSchema : Schema
    {
        public enum ResourceType
        {
            // Unrefined
            Amber = 0,
            Dino = 1,
            Lava = 2,
            Fossil = 3,
            
            // Refined
            Might = 4,
            Prosperity = 5,
            Culture = 6,
            Technology = 7,
            
            // Currency
            Faith = 8,
        } 
        
        public string Name;
        public Sprite Icon;
        public ResourceType Type;

        public bool IsCurrency()
        {
            return Type == ResourceType.Faith;
        }
        
        public bool IsRefined()
        {
            if (IsCurrency())
            {
                return false;
            }
            
            return Type == ResourceType.Might ||
                   Type == ResourceType.Prosperity ||
                   Type == ResourceType.Culture ||
                   Type == ResourceType.Technology;
        }
        
        public bool IsUnrefined()
        {
            if (IsCurrency())
            {
                return false;
            }
            
            return Type == ResourceType.Amber ||
                   Type == ResourceType.Dino ||
                   Type == ResourceType.Lava ||
                   Type == ResourceType.Fossil;
        }
    }
}
