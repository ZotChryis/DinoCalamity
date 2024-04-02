using Gameplay.Cards;
using UnityEngine;
using Utility;

namespace GameStates
{
    public class StateDraw : IState
    {
        public void Enter()
        {
            // TEMP: Draw 5 randomly
            for (int i = 0; i < 10; i++)
            {
                int randomCardIndex = Random.Range(0, ServiceLocator.Instance.Schemas.Cards.Count);
                Schemas.Card cardData = ServiceLocator.Instance.Schemas.Cards[randomCardIndex];
                ServiceLocator.Instance.Player.ShuffleCard(new Card(cardData));
            }

            // TEMP - Draw 5 of those
            ServiceLocator.Instance.Player.Draw();
            ServiceLocator.Instance.Player.Draw();
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
