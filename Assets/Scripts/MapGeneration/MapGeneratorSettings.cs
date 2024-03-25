using Schemas;
using System;
using UnityEngine;

namespace MapGeneration
{
    [CreateAssetMenu]
    public class MapGeneratorSettings : ScriptableObject
    {
        //  TODO: This is placeholder randomization.
        [Serializable]
        public struct MapProbability
        {
            public Schemas.Tile Tile;
            public int Amount;
        }

        /// <summary>
        /// Tiles are spawned at these probabilities. All probabilities are added together and then drawn from that total.
        /// See MapGenerator.GenerateMap for execution.
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