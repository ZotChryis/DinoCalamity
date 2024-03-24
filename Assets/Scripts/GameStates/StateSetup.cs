using Utility;

namespace GameStates
{
    public class StateSetup : IState
    {
        public void Enter()
        {
            ServiceLocator.Instance.MapGenerator.GenerateMap();
            ServiceLocator.Instance.StateMachine.ChangeState(new StateDraw());
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}
