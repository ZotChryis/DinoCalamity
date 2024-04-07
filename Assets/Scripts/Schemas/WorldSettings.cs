using System;
using UnityEngine;

namespace Schemas
{
    [CreateAssetMenu]
    public class WorldSettings : ScriptableObject
    {
        public enum HomeLocation
        {
            Center,
            Random
        }
        
        //  TODO: This is placeholder randomization.
        [Serializable]
        public struct MapProbability
        {
            public Schemas.TileSchema Tile;
            public int Amount;
        }

        /// <summary>
        /// The player's home tile.
        /// </summary>
        public Schemas.TileSchema Home;

        /// <summary>
        /// The location where to place the home tile.
        /// </summary>
        public HomeLocation Location;

        /// <summary>
        /// Tiles are spawned at these probabilities. All probabilities are added together and then drawn from that total.
        /// See World.GenerateMap for execution.
        /// </summary>
        public MapProbability[] MapProbabilities;

        /// <summary>
        /// A map is generated by creating a grid of Width by Height, with 0,0 being at the bottom right.
        /// </summary>
        /// </summary>
        public int Width;

        /// <summary>
        /// A map is generated by creating a grid of Width by Height, with 0,0 being at the bottom right.
        /// </summary>
        public int Height;

        /// <summary>
        /// The size of the hexagons that the map will be using. 
        /// </summary>
        public float HexSize;
    
        /// <summary>
        /// The gap between the hexagons.
        /// </summary>
        public float Gap;
    }
}
