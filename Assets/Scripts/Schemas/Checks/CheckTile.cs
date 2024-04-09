using Gameplay;
using UnityEngine;

namespace Schemas.Checks
{
    /// <summary>
    /// Use this check to ensure that the context has a selected tile.
    /// </summary>
    [CreateAssetMenu]
    public class CheckTile : Check
    {
        public override bool IsValid(Invoker.Context context)
        {
            return context.SelectedTile != null;
        }
    }
}
