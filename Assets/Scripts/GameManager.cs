using Gameplay;
using UnityEngine;
using GameStates;
using Utility;
using Utility.Observable;

public class GameManager: MonoBehaviour
{
    /// <summary>
    /// This event occurs when the turn ends.
    /// </summary>
    public delegate void OnTurnEnd();
    public OnTurnEnd OnTurnEndEvent;
    
    public StateMachine StateMachine { get; private set; } = new StateMachine();
    public Observable<int> Turn { get; private set; } = new Observable<int>(0);
    public Calamity Calamity { get; private set; }

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
    public void RequestEndTurn()
    {
        StateMachine.ChangeState(new StateGeneration());
    }

    public void EndTurn()
    {
        OnTurnEndEvent?.Invoke();
        Turn.Value += 1;
    }

    public void GenerateCalamity()
    {
        // Assume at least 1 calamity
        var allCalamities = ServiceLocator.Instance.Schemas.Calamities;
        int randomIndex = Random.Range(0, allCalamities.Count);
        Calamity = new Calamity(allCalamities[randomIndex]);
    }
}
