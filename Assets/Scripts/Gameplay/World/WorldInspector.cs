using UnityEditor;
using UnityEngine;

namespace Gameplay.World
{
    [CustomEditor(typeof(Gameplay.World.World))]
    public class WorldInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Gameplay.World.World world = (Gameplay.World.World) target;
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
