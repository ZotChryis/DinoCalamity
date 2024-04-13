using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    /// <summary>
    /// Use this action to forcibly select the home tile.
    /// </summary>
    [CreateAssetMenu]
    public class ActionSelectTileHome : Action
    {
        public override void Invoke(Invoker.Context context)
        {
            ServiceLocator.Instance.Loadout.SelectedTile.Value = ServiceLocator.Instance.World.Home;
        }
    }
}
