using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu(menuName = "Action/ActionFog")]    
    public class ActionFog : TargettedAction
    {
        [SerializeField] private bool m_enable;
        [SerializeField] private bool m_includeNeighbors;
        [SerializeField] private bool m_suppressEvent;

        public override void Invoke(Invoker.Context context)
        {
            var tile = ServiceLocator.Instance.Loadout.SelectedTile.Value;
            if (Location == Invoker.Location.Owner)
            {
                tile = context.Owner as Tile;
            }
            if (Location == Invoker.Location.Target)
            {
                tile = context.Target as Tile;
            }
            if (tile == null)
            {
                return;
            }
            
            ServiceLocator.Instance.World.ToggleFog(
                tile,
                false,
                m_includeNeighbors,
                !m_suppressEvent
            );
        }
    }
}
