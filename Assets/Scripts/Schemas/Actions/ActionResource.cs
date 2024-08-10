using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu(menuName = "Action/ActionResource")]   
    public class ActionResource : Action
    {
        [SerializeField] private ResourceSchema m_resource;
        [SerializeField] private int m_amount;
        
        public override void Invoke(Invoker.Context context)
        {
            ServiceLocator.Instance.Bank.DeltaResource(
                m_resource.Type, 
                m_amount, 
                context.Invoker.GetResourceModifiers()
            );
        }
    }
}
