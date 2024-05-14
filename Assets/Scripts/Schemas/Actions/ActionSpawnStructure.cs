using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu(menuName = "Action/ActionSpawnStructure")]  
    public class ActionSpawnStructure : Action
    {
        [SerializeField] private StructureSchema m_schema;
        
        public override void Invoke(Invoker.Context context)
        {
            // This action requires a target tile
            var tile = ServiceLocator.Instance.Loadout.SelectedTile.Value;
            if (tile == null)
            {
                return;
            }
            
            tile.AddStructure(m_schema);
        }
    }
}
