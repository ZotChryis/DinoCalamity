using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.World
{
    public class World : MonoBehaviour
    {
        // TEMP - We should supply the settings we want on creation so we can make levels
        //        but for now, we can serialize it for ease of access.
        public WorldSettings Settings;

        private int m_totalTileProbability;
        private Schemas.Tile[] m_tileProbability;

        private Tile[,] m_grid;
        
        /// <summary>
        /// Generates a map in World space given the current Settings.
        /// </summary>
        public void GenerateMap()
        {
            GenerateProbabilityTable();
            Clear();

            m_grid = new Tile[Settings.Width, Settings.Height];
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

                    var schema = GetRandomTileSchema();
                    var instance = Instantiate(schema.Prefab, position, Quaternion.identity, transform);
                    instance.SetSchema(schema);
                    m_grid[row, col] = instance;
                }
            }

            PlaceHome();
        }

        /// <summary>
        /// Places the home at the appropriate location. This function assumes the grid has been populated.
        /// </summary>
        private void PlaceHome()
        {
            int row, col;
            switch (Settings.Location)
            {
                case WorldSettings.HomeLocation.Random:
                    row = Random.Range(0, Settings.Width);
                    col = Random.Range(0, Settings.Height);
                    break;
                case WorldSettings.HomeLocation.Center:
                default:
                    row = Settings.Width / 2;
                    col = Settings.Height / 2;
                    break;
            }
            
            var position = m_grid[row, col].transform.position; 
            Destroy(m_grid[row, col].gameObject);
            
            var instance = Instantiate(Settings.Home.Prefab, position, Quaternion.identity, transform);
            instance.SetSchema(Settings.Home);
            m_grid[row, col] = instance;

            // Home and its neighbors should be set to have fog off 
            ToggleFog(instance, false, true);
        }

        /// <summary>
        /// Eliminates all gameobjects that make up the World.
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

            m_tileProbability = new Schemas.Tile[m_totalTileProbability];
            int tilesPlaced = 0;
            foreach (var settingsTileProbability in Settings.MapProbabilities)
            {
                for (int i = 0; i < settingsTileProbability.Amount; i++)
                {
                    m_tileProbability[tilesPlaced] = settingsTileProbability.Tile;
                    tilesPlaced++;
                }
            }
        }

        private Schemas.Tile GetRandomTileSchema()
        {
            //  TODO: Introduce a good way to randomize the tiles
            int randomIndex = Random.Range(0, m_totalTileProbability);
            return m_tileProbability[randomIndex];
        }

        private List<Tile> GetNeighbors(Tile tile)
        {
            for (var i = 0; i < Settings.Width; i++)
            {
                for (int j = 0; j < Settings.Height; j++)
                {
                    if (m_grid[i, j] == tile)
                    {
                        return GetNeighbors(i, j);
                    }
                }
            }

            return new List<Tile>();
        }
        
        private List<Tile> GetNeighbors(int x, int y)
        {
            void TryAdd(ref List<Tile> tiles, int r, int c)
            {
                if (IsValidLocation(r, c ))
                {
                    tiles.Add(m_grid[r, c]);
                }
            }
            
            List<Tile> neighbors = new List<Tile>();
            if (x % 2 == 0)
            {
                TryAdd(ref neighbors, x - 1, y - 1);
                TryAdd(ref neighbors, x, y - 1);
                TryAdd(ref neighbors, x + 1, y - 1);
                TryAdd(ref neighbors, x - 1, y);
                TryAdd(ref neighbors, x, y + 1);
                TryAdd(ref neighbors, x + 1, y);
            }
            else
            {
                TryAdd(ref neighbors, x - 1, y);
                TryAdd(ref neighbors, x, y - 1);
                TryAdd(ref neighbors, x + 1, y);
                TryAdd(ref neighbors, x - 1, y + 1);
                TryAdd(ref neighbors, x, y + 1);
                TryAdd(ref neighbors, x + 1, y + 1);
            }

            return neighbors;
        }

        private bool IsValidLocation(int x, int y)
        {
            if (x < 0 || x >= Settings.Width)
            {
                return false;
            }
            if (y < 0 || y >= Settings.Height)
            {
                return false;
            }

            return true;
        }

        public void ToggleFog(Tile tile, bool on, bool includeNeightbors)
        {
            tile.ToggleFog(on);

            if (!includeNeightbors)
            {
                return;
            }
            
            var homeNeighbors = GetNeighbors(tile);
            foreach (var homeNeighbor in homeNeighbors)
            {
                homeNeighbor.ToggleFog(on);
            }
        }
    }
}
