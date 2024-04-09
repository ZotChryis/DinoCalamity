using Utility;

namespace GameStates
{
    public class StateSetup : IState
    {
        public void Enter()
        {
            ServiceLocator.Instance.World.GenerateMap();
            ServiceLocator.Instance.GameManager.GenerateCalamity();
        }

        public void Update()
        {
            // Moved this to Update from Enter because it seemed to get skipped sometimes.
            ServiceLocator.Instance.StateMachine.ChangeState(new StateDraw());
        }

        public void Exit()
        {
        }
    }
}
