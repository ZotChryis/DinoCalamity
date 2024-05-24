using UnityEngine;
using Gameplay;

namespace UI
{
    public class ClickTooltip : MonoBehaviour
    {
        [HideInInspector] public Tile m_tile;

        [HideInInspector] private bool isOpen = false;

        /*
         * TODO: Steps:
         * 3) OnClick -> Open a tooltip for each structure.
         * 4) ClickOff -> Close tooltips.
         */

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

        public void ToggleTooltip(bool open)
        {
            if (open)
            {
                if (m_tile?.Structures == null)
                {
                    return;
                }

                // TODO: Open tooltip for each structure on the tile.
                //for (int i = 0; i < m_tile.Structures.Count; i++)
                //{
                //    // Open tooltip
                //    var view = ServiceLocator.Instance.UIDisplayProcessor.TryShowView(Schemas.ViewMapSchema.ViewType.Tooltip);
                //    var tooltipView = view as TooltipView;
                //    tooltipView?.SetData(m_tile.Structures[i].Schema.TooltipInfo);
                //}

                if (m_tile.Structures.Count > 0)
                {
                    // Open tooltip
                    var view = ServiceLocator.Instance.UIDisplayProcessor.TryShowView(Schemas.ViewMapSchema.ViewType.Tooltip);
                    var tooltipView = view as TooltipView;
                    tooltipView?.SetData(m_tile.Structures[0].Schema.TooltipInfo);
                    Debug.Log($"ClickTooltip: Opening.");
                    isOpen = true;
                }
            } else
            {
                if (isOpen)
                {
                    ServiceLocator.Instance.UIDisplayProcessor.PopView();
                    Debug.Log($"ClickTooltip: Closing.");
                    isOpen = false;
                }
            }
        }


        //private void OnMouseEnter()
        //{
        //    if (Schema == null) return;

        //    Debug.Log($"Structure: Mouse Enter");
        //var view = ServiceLocator.Instance.UIDisplayProcessor.TryShowView(Schemas.ViewMapSchema.ViewType.Tooltip);
        //var tooltipView = view as TooltipView;
        //tooltipView?.SetData(Schema.TooltipInfo);
        //}

        //private void OnMouseExit()
        //{
        //    if (Schema == null) return;

        //    ServiceLocator.Instance.UIDisplayProcessor.PopView();
        //}
    }
}