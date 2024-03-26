using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu]
    public class ActionDiscard : Action
    {
        [SerializeField] private int m_amount;

        public override void Invoke(GameObject source)
        {
            for (int i = 0; i < m_amount; i++)
            {
                ServiceLocator.Instance.Player.DiscardRandomCard();
            }
        }
    }
}