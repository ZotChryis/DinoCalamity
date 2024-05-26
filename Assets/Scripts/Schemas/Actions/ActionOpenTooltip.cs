using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UI;

namespace Schemas.Actions
{
    [CreateAssetMenu(menuName = "Action/OpenTooltip")]
    public class ActionOpenTooltip : Action
    {
        [SerializeField] private ViewSchema viewConfig;
        [SerializeField] private TooltipView.TooltipDescription Tooltip;

        public override void Invoke(Invoker.Context context)
        {
            var view = ServiceLocator.Instance.UIDisplayProcessor.TryShowView(viewConfig);
            var tooltipView = view as TooltipView;
            tooltipView?.SetData(Tooltip, Vector3.zero);
        }
    }
}