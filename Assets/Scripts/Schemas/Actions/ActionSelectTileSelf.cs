using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    /// <summary>
    /// Use this action to forcibly change the tile that invoked this.
    /// </summary>
    public class ActionSelectTileSelf : Action
    {
        public override void Invoke(Invoker.Context context)
        {
            // This should only be called by invokers who are owned by Tiles
            Tile tile = context.Owner as Tile;
            if (tile == null)
            {
                return;
            }
            
            // Overload the context
            ServiceLocator.Instance.Loadout.SelectedTile.Value = tile;
        }
    }
}
