using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu]
    public class ActionShowView : Action
    {
        [SerializeField] private ViewSchema viewConfig;

        public override void Invoke()
        {
            ServiceLocator.Instance.UIDisplayProcessor.TryShowView(viewConfig);
        }
    }
}
