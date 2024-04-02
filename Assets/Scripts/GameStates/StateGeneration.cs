using Utility;

namespace GameStates
{
    public class StateGeneration : IState
    {
        public void Enter()
        {
        }

        public void Update()
        {
            ServiceLocator.Instance.StateMachine.ChangeState(new StateEnd());
        }

        public void Exit()
        {
        }
    }
}
