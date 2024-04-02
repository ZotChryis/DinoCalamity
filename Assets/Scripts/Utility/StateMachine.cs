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
        
        /// <summary>
        /// STUB - This should be outside of the state machine?
        /// This event occurs when the state changes. 
        /// </summary>
        public delegate void OnStateChanged(IState state);
        public OnStateChanged OnStateChangedEvent;
 
        public void ChangeState(IState newState)
        {
            m_currentState?.Exit();
            m_currentState = newState;
            m_currentState.Enter();
            OnStateChangedEvent?.Invoke(m_currentState);
        }

        public IState GetCurrentState()
        {
            return m_currentState;
        }
 
        public void Update()
        {
            m_currentState?.Update();
        }
    }
    
}