using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    /// <summary>
    /// Use this action to swap all neighbor tiles to the currently selected tile.
    /// </summary>
    [CreateAssetMenu]
    public class ActionSwapTileNeighbors : TargettedAction
    {
        public TileSchema Tile;
        public override void Invoke(Invoker.Context context)
        {
            var tile = ServiceLocator.Instance.Loadout.SelectedTile.Value;
            if (Location == Invoker.Location.Target)
            {
                tile = context.Target as Tile;
            }
            
            // This action requires a tile
            if (tile == null)
            {
                return;
            }

            // Swap selected tile's neighbors
            var neighbors = ServiceLocator.Instance.World.GetNeighbors(tile);
            foreach (var neighbor in neighbors)
            {
                ServiceLocator.Instance.World.SwapTile(
                    neighbor,
                    Tile
                );
            }
        }
    }
}
