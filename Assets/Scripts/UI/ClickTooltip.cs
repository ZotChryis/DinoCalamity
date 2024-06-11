using UnityEngine;
using Gameplay;

namespace UI
{
    public class ClickTooltip : MonoBehaviour
    {
        [HideInInspector] public Tile m_tile;

        public Vector3 offset = new Vector3(1.75f, 1.0f, 0); // World point offset to place the tooltip.

        // TODO: May need to listen to an event from UIDisplayProcessor closing a view in case something else pops this.
        private bool isOpen = false;

        public void Start()
        {
            m_tile = GetComponent<Tile>();

            // Subscribe to events.
            ServiceLocator.Instance.Loadout.SelectedTile.OnChangedValues += OnSelectedTileChanged;
        }

        private void OnDestroy()
        {
            ServiceLocator.Instance.Loadout.SelectedTile.OnChangedValues -= OnSelectedTileChanged;
        }

        private void OnSelectedTileChanged(Tile oldValue, Tile newValue)
        {
            ToggleTooltip(newValue == m_tile);
        }

        /// <summary>
        /// Opens and closes the tooltip icon.
        /// </summary>
        /// <param name="open"></param>
        private void ToggleTooltip(bool open)
        {
            if (open)
            {
                if (m_tile?.m_structures == null)
                {
                    return;
                }

                // Get screen position.
                var worldPos = new Vector3(m_tile.transform.position.x + offset.x, m_tile.transform.position.y + offset.y, m_tile.transform.position.z + offset.z);

                // TODO: Make for loop for multiple structures. Make each one offset to not stack.
                if (m_tile.m_structures.Count > 0)
                {
                    // Open tooltip
                    var view = ServiceLocator.Instance.UIDisplayProcessor.TryShowView(Schemas.ViewMapSchema.ViewType.Tooltip);
                    var tooltipView = view as TooltipView;
                    // tooltipView?.SetData(m_tile.m_structures[0].Schema.tooltipInfo, worldPos);

                    isOpen = true;
                }
            }
            else
            {
                if (isOpen)
                {
                    ServiceLocator.Instance.UIDisplayProcessor.PopView();
                    isOpen = false;
                }
            }
        }
    }
}