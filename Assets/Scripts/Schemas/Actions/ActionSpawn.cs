using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu]
    public class ActionSpawn : Action
    {
        [SerializeField] private GameObject m_prefab;
        [SerializeField] private Gameplay.Tile.Anchor m_anchor;

        public override void Invoke()
        {
            // This action requires a target tile
            var tile = ServiceLocator.Instance.Player.SelectedTile.Value;
            if (tile == null)
            {
                return;
            }
            
            tile.AddSpawn(m_prefab, m_anchor);
        }
    }
}
