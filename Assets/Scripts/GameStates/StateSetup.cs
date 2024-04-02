using Gameplay.Cards;
using UnityEngine;
using Utility;

namespace GameStates
{
    public class StateSetup : IState
    {
        public void Enter()
        {
            ServiceLocator.Instance.World.GenerateMap();
            
            // TEMP: Fill deck with 10 random cards
            for (int i = 0; i < 10; i++)
            {
                int randomCardIndex = Random.Range(0, ServiceLocator.Instance.Schemas.Cards.Count);
                Schemas.Card cardData = ServiceLocator.Instance.Schemas.Cards[randomCardIndex];
                ServiceLocator.Instance.Player.ShuffleCard(new Card(cardData));
            }
            
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
