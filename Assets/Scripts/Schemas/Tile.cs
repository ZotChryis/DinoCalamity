using UnityEngine;

namespace Schemas
{
    /// <summary>
    /// The data definition for a Tile.
    /// Create new entries via the asset create menu.
    /// </summary>
    [CreateAssetMenu]
    public class Tile : Schema
    {
        public string Name;
        public Gameplay.Tile Prefab;
    }
}
