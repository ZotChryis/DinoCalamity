using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    /// <summary>
    /// Use this action to swap all neighbor tiles to the currently selected tile.
    /// </summary>
    [CreateAssetMenu]
    public class ActionSwapTileNeighbors : Action
    {
        public TileSchema Tile;
        public override void Invoke(Invoker.Context context)
        {
            // This action requires a tile
            if (context.SelectedTile == null)
            {
                return;
            }

            // Swap selected tile's neighbors
            var neighbors = ServiceLocator.Instance.World.GetNeighbors(context.SelectedTile);
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
