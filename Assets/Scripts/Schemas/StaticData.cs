using System.Collections.Generic;

namespace Schemas
{
    /// <summary>
    /// A temporary solution to how we access all the static data for the game.
    /// For now, we can load in everything into memory. We will need a system for loading/off-loading assets
    /// once the project gets big. For now, this should be sufficient.
    /// This requires everything to live under the Assets/Resources/ directory.
    /// </summary>
    public class StaticData
    {
        private const string c_cardDirectory = "Data/Cards";
        private const string c_tileDirectory = "Data/Tiles";
        private const string c_resourceDirectory = "Data/Resources";
        
        public IReadOnlyList<Card> Cards;
        public IReadOnlyList<Tile> Tiles;
        public IReadOnlyList<Resource> Resources;

        public void Initialize()
        {
            Cards = UnityEngine.Resources.LoadAll<Card>(c_cardDirectory);
            Tiles = UnityEngine.Resources.LoadAll<Tile>(c_tileDirectory);
            Resources = UnityEngine.Resources.LoadAll<Resource>(c_resourceDirectory);
        }
    }
}
