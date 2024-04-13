using UnityEngine;
using Utility;

namespace GameStates
{
    public class StateLoss : IState
    {
        public void Enter()
        {
            // TODO: Go to a game over screen
            Debug.Log("GAME OVER!!!");
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
        }
    }
}
