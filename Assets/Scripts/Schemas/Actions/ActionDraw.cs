using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu]
    public class ActionDraw : Action
    {
        [SerializeField] private int m_amount;

        public override void Invoke()
        {
            for (int i = 0; i < m_amount; i++)
            {
                ServiceLocator.Instance.Player.Draw();
            }
        }
    }
}
