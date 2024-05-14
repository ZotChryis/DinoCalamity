using System.Collections.Generic;
using Schemas;
using UnityEngine;

namespace Gameplay
{
    public class World : MonoBehaviour
    {
        /// <summary>
        /// This event occurs when a tile is revealed from the fog of war.
        /// </summary>
        public delegate void OnTileReveal(Tile tile);
        public OnTileReveal OnTileRevealEvent;
        
        public Tile[,] Grid { get; private set; }
        public Tile Home { get; private set; }
        
        private int m_totalTileProbability;
        private Schemas.TileSchema[] m_tileProbability;

        private WorldSettings m_schema;

        private void Awake()
        {
            ServiceLocator.Instance.Register(this);
        }
        
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

            Grid = new Tile[m_schema.Width, m_schema.Height];
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
                    Grid[row, col] = instance;
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

            // Now replace the home where the settings tell us to...
            SwapTile(Grid[row, col], m_schema.Home);
            
            // ...and toggle the fog on it and surrounding tiles.
            ToggleFog(Grid[row, col], false, true);

            Home = Grid[row, col];
        }

        /// <summary>
        /// Eliminates all gameobjects that make up the World.
        /// </summary>
        private void Clear()
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

        public List<Tile> GetNeighbors(Tile tile)
        {
            for (var i = 0; i < m_schema.Width; i++)
            {
                for (int j = 0; j < m_schema.Height; j++)
                {
                    if (Grid[i, j] == tile)
                    {
                        return GetNeighbors(i, j);
                    }
                }
            }

            return new List<Tile>();
        }
        
        public List<Tile> GetNeighbors(int x, int y)
        {
            void TryAdd(ref List<Tile> tiles, int r, int c)
            {
                if (IsValidLocation(r, c ))
                {
                    tiles.Add(Grid[r, c]);
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

        /// <summary>
        /// Toggles the fog on the given tile to the given value.
        /// </summary>
        public void ToggleFog(Tile tile, bool on, bool includeNeightbors, bool broadcast = true)
        {
            tile.ToggleFog(on, broadcast);

            if (!includeNeightbors)
            {
                return;
            }
            
            var neighbors = GetNeighbors(tile);
            foreach (var neighbor in neighbors)
            {
                neighbor.ToggleFog(on, broadcast);
            }
        }

        /// <summary>
        /// Returns whether the provided tile is the Home tile.
        /// </summary>
        public bool IsHome(Tile tile)
        {
            return tile.Schema.Type == TileSchema.TileType.Home;
        }

        /// <summary>
        /// Attempts to recreate the given tile in the world into the given tile schema.
        /// Returns whether or not it was successful.
        /// </summary>
        public bool SwapTile(Tile tile, TileSchema schema)
        {
            // Do nothing if it's the same one
            if (tile.Schema == schema)
            {
                return false;
            }
            
            for (var row = 0; row < m_schema.Width; row++)
            {
                for (int col = 0; col < m_schema.Height; col++)
                {
                    if (Grid[row, col] == tile)
                    {
                        var oldTile = Grid[row, col];
                        var position = oldTile.transform.position;
                        var newTile = Instantiate(schema.Prefab, position, Quaternion.identity, transform);
                        newTile.SetSchema(schema);
                        newTile.ToggleFog(oldTile.IsInFog(), false);
                        Grid[row, col] = newTile;

                        if (IsHome(oldTile))
                        {
                            Home = newTile;
                        }
                        
                        Destroy(oldTile.gameObject);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// A tile will inform the world that it has been revealed. THis is the entry point for all global
        /// invokers to handle this event.
        /// </summary>
        public void TriggerTileReveal(Tile tile)
        {
            OnTileRevealEvent?.Invoke(tile);
        }
    }
}
