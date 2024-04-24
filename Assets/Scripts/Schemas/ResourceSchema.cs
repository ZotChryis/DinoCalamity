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
        } 
        
        public string Name;
        public Sprite Icon;
        public ResourceType Type;

        public bool IsRefined()
        {
            return Type == ResourceType.Might ||
                   Type == ResourceType.Prosperity ||
                   Type == ResourceType.Culture ||
                   Type == ResourceType.Technology;
        }
        
        public bool IsUnrefined()
        {
            return Type == ResourceType.Amber ||
                   Type == ResourceType.Dino ||
                   Type == ResourceType.Lava ||
                   Type == ResourceType.Fossil;
        }
    }
}
