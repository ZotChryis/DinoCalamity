using Gameplay;
using UnityEngine;

namespace Schemas.Checks
{
    /// <summary>
    /// Use this check to see if selected tile is in Fog of War or not.
    /// </summary>
    [CreateAssetMenu]
    public class CheckFog : Check
    {
        // TODO: Try to bake in Negate to all checks without having to dupe the code
        public bool Negate;
        
        public override bool IsValid(Invoker.Context context)
        {
            if (context.SelectedTile == null)
            {
                return Negate;
            }

            var inFog = context.SelectedTile.IsInFog();
            return Negate ? !inFog : inFog;
        }
    }
}
