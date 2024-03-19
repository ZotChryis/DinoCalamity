namespace Utility
{
    public interface IState
    {
        public void Enter();
        public void Update();
        public void Exit();
    }
 
    /// <summary>
    /// Basic implementation of a Finite State machine. 
    /// </summary>
    public class StateMachine
    {
        private IState m_currentState;
 
        public void ChangeState(IState newState)
        {
            m_currentState?.Exit();
            m_currentState = newState;
            m_currentState.Enter();
        }
 
        public void Update()
        {
            m_currentState?.Update();
        }
    }
    
}