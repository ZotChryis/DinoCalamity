using UnityEngine;

namespace Schemas
{
    /// <summary>
    /// The data definition for a Tile.
    /// Create new entries via the asset create menu.
    /// </summary>
    [CreateAssetMenu]
    public class TileSchema : InvokerSchema
    {
        public enum TileType
        {
            Home,
            Empty,
            Lava,
            Dino,
            Fossil,
            Amber,
            Desert,
            CalamityMeteorCrater,
        }
        
        public string Name;
        public Gameplay.Tile Prefab;
        public TileType Type;
    }
}
