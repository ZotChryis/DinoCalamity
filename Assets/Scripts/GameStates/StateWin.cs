
using UnityEngine;
using Utility;

namespace GameStates
{
    public class StateWin : IState
    {
        public void Enter()
        {
            Debug.Log("WINNER WINNER CHICKEN DINNER!!!!!!!!!!!!");
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}
