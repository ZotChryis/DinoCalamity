using Gameplay;
using UnityEngine;

namespace Schemas.Checks
{
    /// <summary>
    /// Use this check to forcibly check the home tile to see if it's in an ok state.
    /// </summary>
    [CreateAssetMenu]
    public class CheckHome : Check
    {
        public bool Negate = false;
        
        public override bool IsValid(Invoker.Context context)
        {
            // If the home was destroyed for some reason, not valid!
            Tile home = ServiceLocator.Instance.World.Home;
            if (home == null)
            {
                return Negate;
            }

            // If the home was changed to a different type, not valid!
            if (home.Schema.Type != TileSchema.TileType.Home)
            {
                return Negate;
            }

            return !Negate;
        }
    }
}
