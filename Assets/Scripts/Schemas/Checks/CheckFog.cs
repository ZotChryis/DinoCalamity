using Gameplay;
using UnityEngine;

namespace Schemas.Checks
{
    /// <summary>
    /// Use this check to see if selected tile is in Fog of War or not.
    /// </summary>
    [CreateAssetMenu(menuName = "Check/CheckFog")]
    public class CheckFog : TargettedCheck
    {
        // TODO: Try to bake in Negate to all checks without having to dupe the code
        public bool Negate;
        
        public override bool IsValid(Invoker.Context context)
        {
            var tile = ServiceLocator.Instance.Loadout.SelectedTile.Value;
            if (Location == Invoker.Location.Target)
            {
                tile = context.Target as Tile;
            }

            if (tile == null)
            {
                return Negate;
            }

            var inFog = tile.IsInFog();
            return Negate ? !inFog : inFog;
        }
    }
}
