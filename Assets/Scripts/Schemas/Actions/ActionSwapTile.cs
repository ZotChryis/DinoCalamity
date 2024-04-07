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
        
        public override void Invoke()
        {
            // TODO: We should move to using ActionContext
            // This action requires a tile
            if (ServiceLocator.Instance.Loadout.SelectedTile.Value == null)
            {
                return;
            }
            
            ServiceLocator.Instance.World.SwapTile(
                ServiceLocator.Instance.Loadout.SelectedTile.Value,
                Tile
            );
        }
    }
}
