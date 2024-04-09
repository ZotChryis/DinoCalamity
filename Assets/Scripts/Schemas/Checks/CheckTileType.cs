using Gameplay;
using UnityEngine;

namespace Schemas.Checks
{
    /// <summary>
    /// Use this check to determine if selected tile type matches the provided type.
    /// </summary>
    [CreateAssetMenu]
    public class CheckTileType : Check
    {
        public TileSchema.TileType Type;
        
        // TODO: Try to bake in Negate to all checks without having to dupe the code
        public bool Negate;
        
        public override bool IsValid(Invoker.Context context)
        {
            if (context.SelectedTile == null)
            {
                return Negate;
            }

            return Negate ? context.SelectedTile.Schema.Type != Type : context.SelectedTile.Schema.Type == Type;
        }
    }
}
