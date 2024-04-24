using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu(menuName = "Action/ActionSpawnStructure")]  
    public class ActionSpawnStructure : Action
    {
        public StructureSchema Structure => m_schema;
        
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
