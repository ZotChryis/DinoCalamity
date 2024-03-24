using UnityEngine;

namespace Schemas
{
    /// <summary>
    /// The data definition for a Tile.
    /// Create new entries via the asset create menu.
    /// </summary>
    [CreateAssetMenu]
    public class Tile : ScriptableObject
    {
        public string Name;
        public GameObject Prefab;
    }
}
