using Utility;

namespace GameStates
{
    public class StateSetup : IState
    {
        public void Enter()
        {
            Game.Instance.MapGenerator.GenerateMap();
            Game.Instance.StateMachine.ChangeState(new StateDraw());
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}
