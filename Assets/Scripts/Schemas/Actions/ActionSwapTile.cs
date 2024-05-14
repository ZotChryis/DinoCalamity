using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    /// <summary>
    /// Use this action to swap a tile to another type of tile.
    /// </summary>
    [CreateAssetMenu(menuName = "Action/ActionSwapTile")]
    public class ActionSwapTile : TargettedAction
    {
        public TileSchema Tile;
        public override void Invoke(Invoker.Context context)
        {
            var tile = ServiceLocator.Instance.Loadout.SelectedTile.Value;
            if (Location == Invoker.Location.Target)
            {
                tile = context.Target as Tile;
            }
            if (Location == Invoker.Location.Owner)
            {
                tile = context.Owner as Tile;
            }
            
            // This action requires a tile
            if (tile == null)
            {
                return;
            }
            
            // Swap selected tile
            ServiceLocator.Instance.World.SwapTile(
                tile,
                Tile
            );
        }
    }
}
