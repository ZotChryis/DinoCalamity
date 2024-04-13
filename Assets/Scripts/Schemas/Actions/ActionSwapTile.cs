using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    /// <summary>
    /// Use this action to swap a tile to another type of tile.
    /// </summary>
    [CreateAssetMenu]
    public class ActionSwapTile : Action
    {
        public TileSchema Tile;
        public override void Invoke(Invoker.Context context)
        {
            // This action requires a tile
            if (context.SelectedTile == null)
            {
                return;
            }
            
            
            // Swap selected tile
            ServiceLocator.Instance.World.SwapTile(
                context.SelectedTile,
                Tile
            );
        }
    }
}
