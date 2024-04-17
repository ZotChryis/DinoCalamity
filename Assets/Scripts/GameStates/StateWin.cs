
using Schemas;
using Utility;

namespace GameStates
{
    public class StateWin : IState
    {
        public void Enter()
        {
            ServiceLocator.Instance.UIDisplayProcessor.TryShowView(ViewMapSchema.ViewType.GameOver);
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}
