using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu]
    public class ActionSpawnStructure : Action
    {
        [SerializeField] private StructureSchema m_schema;
        [SerializeField] private Gameplay.Tile.Anchor m_anchor;

        public override void Invoke(Invoker.Context context)
        {
            // This action requires a target tile
            var tile = ServiceLocator.Instance.Loadout.SelectedTile.Value;
            if (tile == null)
            {
                return;
            }
            
            tile.AddStructure(m_schema, m_anchor);
        }
    }
}
