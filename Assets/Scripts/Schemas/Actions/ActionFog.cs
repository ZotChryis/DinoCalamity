using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu]
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

            ServiceLocator.Instance.Loadout.SelectedTile.Value.ToggleFog(m_enable);
            if (m_includeNeighbors)
            {
                ServiceLocator.Instance.World.ToggleFog(
                    ServiceLocator.Instance.Loadout.SelectedTile.Value,
                    false,
                    true
                );
            }
        }
    }
}
