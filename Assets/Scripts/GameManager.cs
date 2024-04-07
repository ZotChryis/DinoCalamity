using UnityEngine;
using GameStates;
using Utility;

public class GameManager: MonoBehaviour
{
    public StateMachine StateMachine = new StateMachine();
    
    public int Turn { get; private set; } = 1;

    public void Start()
    {
        StateMachine.ChangeState(new StateSetup());
    }

    public void Update()
    {
        StateMachine.Update();
    }

    /// <summary>
    /// Ends the turn by cycling to the next state.
    /// </summary>
    public void EndTurn()
    {
        StateMachine.ChangeState(new StateGeneration());
    }

    public void IncrementTurnCount()
    {
        Turn++;
    }
}
