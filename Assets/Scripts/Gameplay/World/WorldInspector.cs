using UnityEditor;
using UnityEngine;

namespace Gameplay.MapGeneration
{
    [CustomEditor(typeof(World))]
    public class WorldInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            World world = (World) target;
            if (GUILayout.Button("Generate Map"))
            {
                world.GenerateMap();
            }
            
            if (GUILayout.Button("Clear"))
            {
                world.Clear();
            }
        }
    }
}
