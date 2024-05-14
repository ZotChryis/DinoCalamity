using Gameplay;
using Schemas.Checks;
using UnityEngine;

namespace Schemas.Actions
{
    /// <summary>
    /// Use this action to forcibly change the selected tile to a random neighbor that passes the checks.
    /// /// </summary>
    [CreateAssetMenu(menuName = "Action/ActionSelectRandomTileNeighbor")]  
    public class ActionSelectRandomTileNeighbor : Action
    {
        [SerializeField] private Invoker.Location m_location;
        [SerializeField] private Check[] m_checks;
        
        public override void Invoke(Invoker.Context context)
        {
            var tile = ServiceLocator.Instance.Loadout.SelectedTile.Value;
            if (m_location == Invoker.Location.Owner)
            {
                tile = context.Owner as Tile;
            }
            if (m_location == Invoker.Location.Target)
            {
                tile = context.Target as Tile;
            }
            if (tile == null)
            {
                return;
            }
            
            // Aggregate all possible tiles
            var possibleTiles = ServiceLocator.Instance.World.GetNeighbors(tile);

            // Try to find a valid tile
            while (possibleTiles.Count > 0)
            {
                // First, get a random tile
                int rIndex = Random.Range(0, possibleTiles.Count);
                Tile rTile = possibleTiles[rIndex];
                
                // Second, overwrite the context selected tile for the check logic
                context.Target = rTile;

                // If there are no checks, then its valid
                if (m_checks == null)
                {
                    ServiceLocator.Instance.Loadout.SelectedTile.Value = rTile;
                    return;
                }
                
                // Otherwise, check if all checks pass
                bool passes = true;
                foreach (var check in m_checks)
                {
                    if (!check.IsValid(context))
                    {
                        passes = false;
                        break;
                    }
                }

                // All passed, so this is a good tile!
                if (passes)
                {
                    ServiceLocator.Instance.Loadout.SelectedTile.Value = rTile;
                    return;
                }
                
                // If something didnt pass, we can just remove this and try again
                possibleTiles.RemoveAt(rIndex);
            }
        }
    }
}
