using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] protected Button m_button;
        [SerializeField] private string m_scene;
        [SerializeField] private LoadSceneMode m_mode;


        private void Start()
        {
            m_button.onClick.AddListener(OnButtonClicked);
        }

        protected virtual void OnButtonClicked()
        {
            SceneManager.LoadScene(m_scene, m_mode);
        }
    }
}
