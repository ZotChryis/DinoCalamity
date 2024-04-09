using Gameplay;
using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu]
    public class ActionShowView : Action
    {
        [SerializeField] private ViewSchema viewConfig;

        public override void Invoke(Invoker.Context context)
        {
            ServiceLocator.Instance.UIDisplayProcessor.TryShowView(viewConfig);
        }
    }
}
