using UnityEngine;
using GameStates;
using Utility;

public class GameManager: MonoBehaviour
{
    public StateMachine StateMachine = new StateMachine();

    [SerializeField] private int turn = 1;
    public int Turn => turn;

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
        turn++;
    }
}
