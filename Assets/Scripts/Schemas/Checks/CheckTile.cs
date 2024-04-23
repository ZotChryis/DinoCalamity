using Gameplay;
using UnityEngine;

namespace Schemas.Checks
{
    /// <summary>
    /// Use this check to ensure that the context has a selected tile.
    /// </summary>
    [CreateAssetMenu]
    public class CheckTile : TargettedCheck
    {
        public override bool IsValid(Invoker.Context context)
        {
            var tile = ServiceLocator.Instance.Loadout.SelectedTile.Value;
            if (Location == Invoker.Location.Target)
            {
                tile = context.Target as Tile;
            }
            
            return tile != null;
        }
    }
}
