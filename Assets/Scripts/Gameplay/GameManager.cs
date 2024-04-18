using GameStates;
using UnityEngine;
using Utility;
using Utility.Observable;

namespace Gameplay
{
    public class GameManager: MonoBehaviour
    {
        /// <summary>
        /// This event occurs when the turn ends.
        /// </summary>
        public delegate void OnTurnEnd();
        public OnTurnEnd OnTurnEndEvent;
    
        /// <summary>
        /// This event occurs when the turn starts.
        /// </summary>
        public delegate void OnTurnStart();
        public OnTurnEnd OnTurnStartEvent;
    
        public StateMachine StateMachine { get; private set; } = new StateMachine();
        public Observable<int> Turn { get; private set; } = new Observable<int>(0);
        public Calamity Calamity { get; private set; }

        public void Awake()
        {
            ServiceLocator.Instance.Register(this);
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

        public void StartTurn()
        {
            Turn.Value += 1;
            OnTurnStartEvent?.Invoke();
        }

        public void EndTurn()
        {
            OnTurnEndEvent?.Invoke();
        }

        public void GenerateCalamity()
        {
            // Assume at least 1 calamity
            var allCalamities = ServiceLocator.Instance.Schemas.Calamities;
            int randomIndex = Random.Range(0, allCalamities.Count);
            Calamity = new Calamity(allCalamities[randomIndex]);
        }
    }
}
