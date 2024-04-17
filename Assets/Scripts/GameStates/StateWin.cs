
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;

namespace GameStates
{
    public class StateWin : IState
    {
        public void Enter()
        {
            // todo: add popup for this? for now just go back to main menu
            Debug.Log("WINNER WINNER CHICKEN DINNER!!!!!!!!!!!!");

            SceneManager.LoadScene("Scenes/MainMenu", LoadSceneMode.Single);
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}
