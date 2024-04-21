using Gameplay;
using GameStates;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CheatWindow : EditorWindow {

        [MenuItem("Window/Cheats")]
        public static void Open()
        {
            GetWindow(typeof(CheatWindow));
        }
        
        public void OnGUI()
        {
            if (!HasGameStart())
            {
                GUILayout.Label ("Waiting for game start...", EditorStyles.boldLabel);
                return;
            }
            
            GUILayout.Label ("Map", EditorStyles.boldLabel);
            
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
            
            GUILayout.Label ("Bank", EditorStyles.boldLabel);
            var allResources = ServiceLocator.Instance.Schemas.Resources;
            foreach (var resource in allResources)
            {
                GUILayout.Label (resource.Name, EditorStyles.boldLabel);
                if (GUILayout.Button("+X"))
                {
                    ServiceLocator.Instance.Bank.DeltaMultiplier(resource, 1);
                }
                if (GUILayout.Button("+100"))
                {
                    ServiceLocator.Instance.Bank.DeltaResource(resource, 100);
                }
                if (GUILayout.Button("+10"))
                {
                    ServiceLocator.Instance.Bank.DeltaResource(resource, 10);
                }
                if (GUILayout.Button("+1"))
                {
                    ServiceLocator.Instance.Bank.DeltaResource(resource, 1);
                }

                if (GUILayout.Button( "-1"))
                {
                    ServiceLocator.Instance.Bank.DeltaResource(resource, -1);
                }
                if (GUILayout.Button("-10"))
                {
                    ServiceLocator.Instance.Bank.DeltaResource(resource, -10);
                }
                if (GUILayout.Button("-100"))
                {
                    ServiceLocator.Instance.Bank.DeltaResource(resource, -100);
                }
                if (GUILayout.Button("-X"))
                {
                    ServiceLocator.Instance.Bank.DeltaMultiplier(resource, 1);
                }
            }
        }

        private bool HasGameStart()
        {
            if (ServiceLocator.Instance == null)
            {
                return false;
            }

            if (ServiceLocator.Instance.GameManager == null)
            {
                return false;
            }

            var currState = ServiceLocator.Instance.GameManager.StateMachine.GetCurrentState();
            if (currState is StateWin or StateLoss)
            {
                return false;
            }

            return true;
        }
    }
}