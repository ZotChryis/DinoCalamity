using UnityEngine;
using Gameplay;

namespace UI
{
    public class ClickTooltip : MonoBehaviour
    {
        [HideInInspector] public Tile m_tile;

        // TODO: May need to listen to an event from UIDisplayProcessor closing a view in case something else pops this.
        [HideInInspector] private bool isOpen = false;

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
        public void ToggleTooltip(bool open)
        {
            if (open)
            {
                if (m_tile?.Structures == null)
                {
                    return;
                }

                var tilePos = Camera.main.WorldToScreenPoint(m_tile.transform.position);

                // TODO: Make for loop for multiple structures. Make each one offset to not stack.
                if (m_tile.Structures.Count > 0)
                {
                    // Open tooltip
                    var view = ServiceLocator.Instance.UIDisplayProcessor.TryShowView(Schemas.ViewMapSchema.ViewType.Tooltip);
                    var tooltipView = view as TooltipView;
                    tooltipView?.SetData(m_tile.Structures[0].Schema.TooltipInfo);

                    // Set position.
                    tooltipView.transform.position = tilePos;

                    Debug.Log($"ClickTooltip: Opening. world={m_tile.transform.position}. screen={tilePos}");
                    isOpen = true;
                }
            }
            else
            {
                if (isOpen)
                {
                    ServiceLocator.Instance.UIDisplayProcessor.PopView();
                    Debug.Log($"ClickTooltip: Closing.");
                    isOpen = false;
                }
            }
        }
    }
}