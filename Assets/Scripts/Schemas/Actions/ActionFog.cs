using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu(menuName = "Action/ActionFog")]    
    public class ActionFog : Action
    {
        [SerializeField] private bool m_enable;
        [SerializeField] private bool m_includeNeighbors;

        public override void Invoke(Invoker.Context context)
        {
            // TODO: We should move to using ActionContext
            // This action requires a tile
            if (ServiceLocator.Instance.Loadout.SelectedTile.Value == null)
            {
                return;
            }

            ServiceLocator.Instance.World.ToggleFog(
                ServiceLocator.Instance.Loadout.SelectedTile.Value,
                false,
                m_includeNeighbors
            );
        }
    }
}
