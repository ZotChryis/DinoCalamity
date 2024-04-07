using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu]
    public class ActionResource : Action
    {
        [SerializeField] private global::Schemas.ResourceSchema m_resource;
        [SerializeField] private int m_amount;
        
        public override void Invoke()
        {
            ServiceLocator.Instance.Bank.DeltaResource(m_resource, m_amount);
        }
    }
}
