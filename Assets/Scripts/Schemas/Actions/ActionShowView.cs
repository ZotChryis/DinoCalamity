using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu]
    public class ActionShowView : Action
    {
        [SerializeField] private ViewConfig viewConfig;

        public override void Invoke()
        {
            ServiceLocator.Instance.UIDisplayProcessor.TryShowView(viewConfig);
        }
    }
}
