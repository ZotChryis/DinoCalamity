using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] private Button m_button;
        [SerializeField] private string m_scene;
        [SerializeField] private LoadSceneMode m_mode;


        private void Start()
        {
            m_button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            SceneManager.LoadScene(m_scene, m_mode);
        }
    }
}
