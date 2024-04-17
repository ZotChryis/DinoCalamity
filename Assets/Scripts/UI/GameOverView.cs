using GameStates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameOverView : View
    {
        [SerializeField] private TMP_Text m_title;
        
        public void Start()
        {
            string title = ServiceLocator.Instance.GameManager.StateMachine.GetCurrentState() is StateWin
                ? "Victory!"
                : "Defeat!";

            m_title.SetText(title);
        }

        public void OnMainMenuButtonPressed()
        {
            SceneManager.LoadScene("Scenes/MainMenu", LoadSceneMode.Single);
        }
    }
}
