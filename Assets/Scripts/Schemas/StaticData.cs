using System.Collections.Generic;

namespace Schemas
{
    /// <summary>
    /// A temporary solution to how we access all the static data for the game.
    /// For now, we can load in everything into memory. We will need a system for loading/off-loading assets
    /// once the project gets big. For now, this should be sufficient.
    /// </summary>
    public class StaticData
    {
        public IReadOnlyList<Card> Cards;
        public IReadOnlyList<Tile> Tiles;
        public IReadOnlyList<Resource> Resources;

        public void Initialize()
        {
            Cards = UnityEngine.Resources.FindObjectsOfTypeAll<Card>();
            Tiles = UnityEngine.Resources.FindObjectsOfTypeAll<Tile>();
            Resources = UnityEngine.Resources.FindObjectsOfTypeAll<Resource>();
        }
    }
}
