using Gameplay;
using UnityEngine;

namespace Schemas.Checks
{
    /// <summary>
    /// Use this check to determine if the contextual Tile has a building on it.
    /// </summary>
    [CreateAssetMenu(menuName = "Check/CheckTileHasCapacity")]
    public class CheckTileHasCapacity : TargettedCheck
    {
        public bool Negate;
        
        public override bool IsValid(Invoker.Context context)
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

            if (tile == null)
            {
                return Negate;
            }

            bool hasCapacity = tile.HasCapacity();
            return Negate ? !hasCapacity : hasCapacity;
        }
    }
}