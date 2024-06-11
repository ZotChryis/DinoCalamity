using UnityEngine;
using Gameplay;

namespace UI
{
    public class TooltipHandler: MonoBehaviour
    {
        public Vector3 offset = new Vector3(1.75f, 1.0f, 0); // World point offset to place the tooltip.

        // private Tile currValue;
        private View currTooltipView = null;
        
        public void Start()
        {
            // Subscribe to events.
            ServiceLocator.Instance.Loadout.SelectedTile.OnChangedValues += OnSelectedTileChanged;
        }

        private void OnDestroy()
        {
            ServiceLocator.Instance.Loadout.SelectedTile.OnChangedValues -= OnSelectedTileChanged;
        }
        
        private void OnSelectedTileChanged(Tile oldValue, Tile newValue)
        {
            ToggleTooltip(newValue);
        }

        private void ToggleTooltip(Tile newValue)
        {
            // currValue?.CloseTooltip();
            if (currTooltipView != null)
            {
                ServiceLocator.Instance.UIDisplayProcessor.CloseView(currTooltipView);
            }

            // currValue = newValue;

            currTooltipView = newValue?.OpenTooltip(offset);
        }
    }
}