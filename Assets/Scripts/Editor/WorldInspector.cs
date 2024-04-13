using Gameplay;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(World))]
    class WorldInspector : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            if (GUILayout.Button("Remove All Fog"))
            {
                if (ServiceLocator.Instance == null)
                {
                    return;
                }

                foreach (var tile in ServiceLocator.Instance.World.Grid)
                {
                    ServiceLocator.Instance.World.ToggleFog(tile, false, false);
                }
            }
            
            if (GUILayout.Button("Remove 50% Fog"))
            {
                if (ServiceLocator.Instance == null)
                {
                    return;
                }

                foreach (var tile in ServiceLocator.Instance.World.Grid)
                {
                    var random = Random.Range(0, 2);
                    ServiceLocator.Instance.World.ToggleFog(tile, random == 0, false);
                }
            }
        }
    }
}