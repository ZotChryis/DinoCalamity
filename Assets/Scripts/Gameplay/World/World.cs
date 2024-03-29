using UnityEngine;

namespace Gameplay.MapGeneration
{
    public class World : MonoBehaviour
    {
        // TEMP - We should supply the settings we want on creation so we can make levels
        //        but for now, we can serialize it for ease of access.
        public WorldSettings Settings;

        private int m_totalTileProbability;
        private GameObject[] m_tileProbability;

        private GameObject[,] m_grid;
        
        /// <summary>
        /// Generates a map in world space given the current Settings.
        /// </summary>
        public void GenerateMap()
        {
            GenerateProbabilityTable();
            Clear();

            m_grid = new GameObject[Settings.Width, Settings.Height];
            float zOffsetEven = Mathf.Sqrt(3) * Settings.HexSize;
            for (int row = 0; row < Settings.Width; row++)
            {
                for (int col = 0; col < Settings.Height; col++)
                {
                    float zPos = col * (zOffsetEven + Settings.Gap);
                    if (row % 2 == 1)
                    {
                        zPos += (zOffsetEven) / 2f;
                    }

                    float xPos = row * 1.5f * (Settings.HexSize + Settings.Gap);
                
                    Vector3 position = new Vector3(xPos, 0, zPos);

                    m_grid[row, col] = Instantiate(GetRandomTile(), position, Quaternion.identity, transform);
                }
            }
        }

        /// <summary>
        /// Eliminates all gameobjects that make up the world.
        /// </summary>
        public void Clear()
        {
            // For some reason, in editor, using DestroyImmediate only kills half of the children. So we have to wrap 
            // this function in a while loop to do it until it's empty. Super odd. Probably why the game can hang
            // at times. Should look into this
            while (transform.childCount > 0)
            {
                foreach(Transform child in transform)
                {
                    if (Application.isPlaying)
                    {
                        Destroy(child.gameObject);
                    }
                    else
                    {
                        DestroyImmediate(child.gameObject);
                    }
                }
            }
        }

        private void GenerateProbabilityTable()
        {
            // O(2N) because we have to find the total first to properly initialize the array.
            m_totalTileProbability = 0;
            foreach (var settingsTileProbability in Settings.MapProbabilities)
            {
                m_totalTileProbability += settingsTileProbability.Amount;
            }

            m_tileProbability = new GameObject[m_totalTileProbability];
            int tilesPlaced = 0;
            foreach (var settingsTileProbability in Settings.MapProbabilities)
            {
                for (int i = 0; i < settingsTileProbability.Amount; i++)
                {
                    m_tileProbability[tilesPlaced] = settingsTileProbability.Tile.Prefab;
                    tilesPlaced++;
                }
            }
        }

        private GameObject GetRandomTile()
        {
            //  TODO: Introduce a good way to randomize the tiles
            int randomIndex = Random.Range(0, m_totalTileProbability);
            return m_tileProbability[randomIndex];
        }
    }
}
