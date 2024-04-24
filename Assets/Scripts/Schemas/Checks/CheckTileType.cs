using Gameplay;
using UnityEngine;

namespace Schemas.Checks
{
    /// <summary>
    /// Use this check to determine if selected tile type matches the provided type.
    /// </summary>
    [CreateAssetMenu(menuName = "Check/CheckTileType")]
    public class CheckTileType : TargettedCheck
    {
        public TileSchema.TileType Type;

        // TODO: Try to bake in Negate to all checks without having to dupe the code
        public bool Negate;
        
        public override bool IsValid(Invoker.Context context)
        {
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

            if (tile == null)
            {
                return Negate;
            }

            return Negate ? tile.Schema.Type != Type : tile.Schema.Type == Type;
        }
    }
}
