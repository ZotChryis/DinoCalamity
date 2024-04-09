using Utility;

namespace GameStates
{
    public class StateEnd : IState
    {
        public void Enter()
        {
            // Increment turn counter
            ServiceLocator.Instance.GameManager.EndTurn();
        }

        public void Update()
        {
            // First tick, we can move on. We should check for max turn counter?
            ServiceLocator.Instance.StateMachine.ChangeState(new StateDraw());
        }

        public void Exit()
        {
        }
    }
}
