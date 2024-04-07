using UnityEngine;

namespace Schemas.Checks
{
    /// <summary>
    /// Use this check to determine if the contextual Tile has a building on it.
    /// </summary>
    [CreateAssetMenu]
    public class CheckStructure : Check
    {
        public bool Negate;
        
        public override bool IsValid(Context context)
        {
            if (context.SelectedTile == null)
            {
                return Negate;
            }

            bool hasStructure = context.SelectedTile.GetStructureCount() > 0;
            return Negate ? !hasStructure : hasStructure;
        }
    }
}
