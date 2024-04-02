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
        
        public IReadOnlyList<Card> Cards;
        public IReadOnlyList<Tile> Tiles;
        public IReadOnlyList<Resource> Resources;

        public void Initialize()
        {
            Cards = Array.FindAll(UnityEngine.Resources.LoadAll<Card>(c_cardDirectory), c => c.Status >= Schema.ProductionStatus.InDevelopment);
            Tiles = Array.FindAll(UnityEngine.Resources.LoadAll<Tile>(c_cardDirectory), c => c.Status >= Schema.ProductionStatus.InDevelopment);
            Resources = Array.FindAll(UnityEngine.Resources.LoadAll<Resource>(c_cardDirectory), c => c.Status >= Schema.ProductionStatus.InDevelopment);
        }
    }
}
