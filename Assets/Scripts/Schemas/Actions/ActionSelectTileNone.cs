using Gameplay;

namespace Schemas.Actions
{
    /// <summary>
    /// Use this action if you wish to deselect all tiles.
    /// </summary>
    public class ActionSelectTileNone : Action
    {
        public override void Invoke(Invoker.Context context)
        {
            ServiceLocator.Instance.Loadout.SelectedTile.Value = null;
        }
    }
}
