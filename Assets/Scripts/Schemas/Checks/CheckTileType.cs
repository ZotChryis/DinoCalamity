using Gameplay;
using UnityEngine;

namespace Schemas.Checks
{
    /// <summary>
    /// Use this check to determine if selected tile type matches the provided type.
    /// </summary>
    [CreateAssetMenu]
    public class CheckTileType : TargettedCheck
    {
        public TileSchema Schema;

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

            return Negate ? tile.Schema != Schema : tile.Schema == Schema;
        }
    }
}
