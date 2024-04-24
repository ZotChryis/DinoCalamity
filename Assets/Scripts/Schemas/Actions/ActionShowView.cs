using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu(menuName = "Action/ActionShowView")]  
    public class ActionShowView : Action
    {
        [SerializeField] private ViewSchema viewConfig;

        public override void Invoke(Invoker.Context context)
        {
            ServiceLocator.Instance.UIDisplayProcessor.TryShowView(viewConfig);
        }
    }
}
