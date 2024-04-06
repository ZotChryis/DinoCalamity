using Utility;

namespace GameStates
{
    public class StateDraw : IState
    {
        public void Enter()
        {
            // TEMP - Draw 3 cards
            ServiceLocator.Instance.Player.Draw();
            ServiceLocator.Instance.Player.Draw();
            ServiceLocator.Instance.Player.Draw();
        }

        public void Update()
        {
            // On first update, go into StatePlay
            ServiceLocator.Instance.StateMachine.ChangeState(new StatePlay());
        }

        public void Exit()
        {
        }
    }
}
