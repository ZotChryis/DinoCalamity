using System.Collections.Generic;
using Gameplay;
using Schemas.Checks;
using UnityEngine;

namespace Schemas.Actions
{
    /// <summary>
    /// Use this action to forcibly change the selected tile to a random one that passes the checks.
    /// /// </summary>
    [CreateAssetMenu(menuName = "Action/ActionSelectRandomTile")]  
    public class ActionSelectRandomTile : Action
    {
        [SerializeField] private Check[] m_checks;
        
        public override void Invoke(Invoker.Context context)
        {
            // Aggregate all tiles
            var allTiles = new List<Tile>();
            foreach (var tile in ServiceLocator.Instance.World.Grid)
            {
                allTiles.Add(tile);
            }

            // Internal context because we'll swap selected tile for checks
            Invoker.Context internalContext = new Invoker.Context()
            {
                Invoker = context.Invoker
            };

            // Try to find a valid tile
            while (allTiles.Count > 0)
            {
                // First, get a random tile
                int rIndex = Random.Range(0, allTiles.Count);
                Tile rTile = allTiles[rIndex];
                
                // Second, overwrite the context selected tile for the check logic
                internalContext.Target = rTile;

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
                    if (!check.IsValid(internalContext))
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
                allTiles.RemoveAt(rIndex);
            }
        }
    }
}
