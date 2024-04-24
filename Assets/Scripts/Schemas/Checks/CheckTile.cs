using Gameplay;
using UnityEngine;

namespace Schemas.Checks
{
    /// <summary>
    /// Use this check to ensure that the context has a selected tile.
    /// </summary>
    [CreateAssetMenu(menuName = "Check/CheckTile")]
    public class CheckTile : TargettedCheck
    {
        public override bool IsValid(Invoker.Context context)
        {
            // todo: we need a selected tile context location
            var tile = ServiceLocator.Instance.Loadout.SelectedTile.Value;

            switch (Location)
            {
                case Invoker.Location.Target:
                    tile = context.Target as Tile;
                    break;
                case Invoker.Location.Owner:
                    tile = context.Owner as Tile;
                    break;
            }

            return tile != null;
        }
    }
}
