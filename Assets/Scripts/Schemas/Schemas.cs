using System;
using System.Collections.Generic;
using System.Linq;

namespace Schemas
{
    /// <summary>
    /// A temporary solution to how we access all the static data for the game.
    /// For now, we can load in everything into memory. We will need a system for loading/off-loading assets
    /// once the project gets big. For now, this should be sufficient.
    /// This requires everything to live under the Assets/Resources/ directory.
    /// </summary>
    public class Schemas
    {
        private const string c_cardDirectory = "Data/Cards";
        private const string c_tileDirectory = "Data/Tiles";
        private const string c_resourceDirectory = "Data/Resources";
        
        public IReadOnlyList<CardSchema> Cards;
        public IReadOnlyList<TileSchema> Tiles;
        public IReadOnlyList<ResourceSchema> Resources;

        public void Initialize(Schema.ProductionStatus minimumStatus)
        {
            Cards = Array.FindAll(
                UnityEngine.Resources.LoadAll<CardSchema>(c_cardDirectory), 
                v => v.Status >= minimumStatus
            );
            Tiles = Array.FindAll(
                UnityEngine.Resources.LoadAll<TileSchema>(c_tileDirectory), 
                v => v.Status >= minimumStatus
            );
            Resources = Array.FindAll(
                UnityEngine.Resources.LoadAll<ResourceSchema>(c_resourceDirectory),
                v => v.Status >= minimumStatus
            );
        }
    }
}
