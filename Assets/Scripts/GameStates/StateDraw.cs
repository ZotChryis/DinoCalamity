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
                switch (cardData.Type)
                {
                    case Schemas.Card.CardType.Structure:
                        ServiceLocator.Instance.Player.ShuffleCard(new Structure(cardData));
                        break;
                    case Schemas.Card.CardType.Action:
                        ServiceLocator.Instance.Player.ShuffleCard(new Action(cardData));
                        break;
                }
            }

            // TEMP - Draw 3 of those
            ServiceLocator.Instance.Player.Draw();
            ServiceLocator.Instance.Player.Draw();
            ServiceLocator.Instance.Player.Draw();
        }

        public void Update()
        {
        }

        public void Exit()
        {
        }
    }
}
