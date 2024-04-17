using Schemas;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public void OnPlayPressed()
        {
            // todo: load the scene behind a loading bar...
            SceneManager.LoadScene("Scenes/Game", LoadSceneMode.Single);
        }

        public void OnPatchNotesPressed()
        {
            ServiceLocator.Instance.UIDisplayProcessor.TryShowView(ViewMapSchema.ViewType.PatchNotes);
        }
    }
}
