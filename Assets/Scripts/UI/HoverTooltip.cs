using UnityEngine;
using Gameplay;

namespace UI
{
    public class HoverTooltip : MonoBehaviour
    {
        // TODO: Make this more generic.
        [HideInInspector] public Schemas.StructureSchema Schema;

        public void Start()
        {
            // TODO: Make a better way to get the data.
            Schema = GetComponent<Structure>()?.Schema;
        }

        private void OnMouseEnter()
        {
            if (Schema == null) return;

            Debug.Log($"Structure: Mouse Enter");
            var view = ServiceLocator.Instance.UIDisplayProcessor.TryShowView(Schemas.ViewMapSchema.ViewType.Tooltip);
            var tooltipView = view as TooltipView;
            tooltipView?.SetData(Schema.TooltipInfo);
        }

        private void OnMouseExit()
        {
            if (Schema == null) return;

            ServiceLocator.Instance.UIDisplayProcessor.PopView();
        }
    }
}