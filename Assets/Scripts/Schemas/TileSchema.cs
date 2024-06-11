using System.Collections.Generic;
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
            Home = 0,
            Empty = 1,
            Lava = 2,
            Dino = 3,
            Fossil = 4,
            Amber = 5,
            Desert = 6,
            CalamityMeteorCrater = 7,
            EventDiscover = 8,
            EventPillagers = 9
        }
        
        public string Name;
        public Gameplay.Tile Prefab;
        public TileType Type;
        
        /// <summary>
        /// How many buildings this tile can hold.
        /// This may be altered at runtime.
        /// </summary>
        [Range(0, 4)]
        public int Capacity;

        // Tooltip info.
        public string tooltipMessage;
        public Sprite tooltipIcon;
        public List<Action> tooltipActions;
    }
}
