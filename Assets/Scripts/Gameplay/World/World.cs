using System.Collections.Generic;
using Schemas;
using UnityEngine;

namespace Gameplay.World
{
    public class World : MonoBehaviour
    {
        private int m_totalTileProbability;
        private Schemas.TileSchema[] m_tileProbability;

        private Tile[,] m_grid;
        private Tile m_home;
        private WorldSettings m_schema;

        public void Initialize(WorldSettings schema)
        {
            m_schema = schema;
        }
        
        /// <summary>
        /// Generates a map in World space given the current WorldSettings.
        /// </summary>
        public void GenerateMap()
        {
            GenerateProbabilityTable();
            Clear();

            m_grid = new Tile[m_schema.Width, m_schema.Height];
            float zOffsetEven = Mathf.Sqrt(3) * m_schema.HexSize;
            for (int row = 0; row < m_schema.Width; row++)
            {
                for (int col = 0; col < m_schema.Height; col++)
                {
                    float zPos = col * (zOffsetEven + m_schema.Gap);
                    if (row % 2 == 1)
                    {
                        zPos += (zOffsetEven) / 2f;
                    }
                    float xPos = row * 1.5f * (m_schema.HexSize + m_schema.Gap);
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
            switch (m_schema.Location)
            {
                case WorldSettings.HomeLocation.Random:
                    row = Random.Range(0, m_schema.Width);
                    col = Random.Range(0, m_schema.Height);
                    break;
                case WorldSettings.HomeLocation.Center:
                default:
                    row = m_schema.Width / 2;
                    col = m_schema.Height / 2;
                    break;
            }
            
            var position = m_grid[row, col].transform.position; 
            Destroy(m_grid[row, col].gameObject);
            
            m_home = Instantiate(m_schema.Home.Prefab, position, Quaternion.identity, transform);
            m_home.SetSchema(m_schema.Home);
            m_grid[row, col] = m_home;

            // Home and its neighbors should be set to have fog off 
            ToggleFog(m_home, false, true);
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
            foreach (var settingsTileProbability in m_schema.MapProbabilities)
            {
                m_totalTileProbability += settingsTileProbability.Amount;
            }

            m_tileProbability = new Schemas.TileSchema[m_totalTileProbability];
            int tilesPlaced = 0;
            foreach (var settingsTileProbability in m_schema.MapProbabilities)
            {
                for (int i = 0; i < settingsTileProbability.Amount; i++)
                {
                    m_tileProbability[tilesPlaced] = settingsTileProbability.Tile;
                    tilesPlaced++;
                }
            }
        }

        private Schemas.TileSchema GetRandomTileSchema()
        {
            //  TODO: Introduce a good way to randomize the tiles
            int randomIndex = Random.Range(0, m_totalTileProbability);
            return m_tileProbability[randomIndex];
        }

        private List<Tile> GetNeighbors(Tile tile)
        {
            for (var i = 0; i < m_schema.Width; i++)
            {
                for (int j = 0; j < m_schema.Height; j++)
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
            if (x < 0 || x >= m_schema.Width)
            {
                return false;
            }
            if (y < 0 || y >= m_schema.Height)
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

        public bool IsHome(Tile tile)
        {
            return tile == m_home;
        }
    }
}
