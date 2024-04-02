using UnityEngine;

namespace Schemas
{
    /// <summary>
    /// The data definition for a Resource.
    /// Create new entries via the asset create menu.
    /// </summary>
    [CreateAssetMenu]
    public class Resource : Schema
    {
        public string Name;
        public Sprite Icon;
    }
}
