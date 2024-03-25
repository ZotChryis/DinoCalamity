using MapGeneration;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public MapGeneratorSettings Settings;

    private int m_totalTileProbability;
    private GameObject[] m_tileProbability;

    public void GenerateMap()
    {
        GenerateProbabilityTable();
        
        // TODO: It is possible to separate the data generation and the Unity object instantiation. Do we want that?
        // Kill all children objects (for when regenerating)
        Clear();
        
        float zOffsetEven = Mathf.Sqrt(3) * Settings.HexSize;
        for (int row = 0; row < Settings.Width; row++)
        {
            for (int col = 0; col < Settings.Height; col++)
            {
                // We check null because it's valid that a tile rolls empty (for now?)
                GameObject tile = GetRandomTile();
                if (tile == null)
                {
                    continue;
                }
                
                float zPos = col * (zOffsetEven + Settings.Gap);
                if (row % 2 == 1)
                {
                    zPos += (zOffsetEven) / 2f;
                }

                float xPos = row * 1.5f * (Settings.HexSize + Settings.Gap);
                
                Vector3 position = new Vector3(xPos, 0, zPos);

                //  TODO: How do we store the grid? Right now we don't. But we'll need a way to determine neighbors, etc.
                Instantiate(tile, position, Quaternion.identity, transform);
            }
        }
    }

    public void Clear()
    {
        // For some reason, in editor, using DestroyImmediate only kills half of the children. So we have to wrap 
        // this function in a while loop to do it until it's empty. Super odd
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

    public void GenerateProbabilityTable()
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
