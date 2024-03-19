using UnityEditor;
using UnityEngine;

namespace MapGeneration
{
    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            MapGenerator mapGenerator = (MapGenerator) target;
            if (GUILayout.Button("Generate Map"))
            {
                mapGenerator.GenerateMap();
            }
            if (GUILayout.Button("Generate Probabilities"))
            {
                mapGenerator.GenerateProbabilityTable();
            }
        }
    }
}
