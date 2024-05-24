using UnityEngine;
using UnityEngine.EventSystems;
using Gameplay;

namespace UI
{
    public class ClickTooltip : MonoBehaviour, IPointerClickHandler
    {
        [HideInInspector] public Tile m_tile;

        /*
         * TODO: Steps:
         * 1) Get Tile class
         * 2) Get all structures on the Tile
         * 3) OnClick -> Open a tooltip for each structure.
         * 4) ClickOff -> Close tooltips.
         */

        public void Start()
        {
            // TODO: Make a better way to get the data.
            m_tile = GetComponent<Tile>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // Open tooltip on right click.
            if (eventData.button != PointerEventData.InputButton.Right)
            {
                return;
            }
            string str = "";
            if (m_tile?.Structures != null)
            {
                str += "{\n";
                for (int i = 0; i < m_tile.Structures.Count; i++)
                {
                    str += $"{m_tile.Structures[i].Schema.name},\n";
                }
                str += "}";
            }
            Debug.Log($"{gameObject.name}: Opening Tooltip. Structures: {str}");
            //ServiceLocator.Instance.Loadout.SelectedTile.Value = this;
        }

        //private void OnMouseEnter()
        //{
        //    if (Schema == null) return;

        //    Debug.Log($"Structure: Mouse Enter");
        //    var view = ServiceLocator.Instance.UIDisplayProcessor.TryShowView(Schemas.ViewMapSchema.ViewType.Tooltip);
        //    var tooltipView = view as TooltipView;
        //    tooltipView?.SetData(Schema.TooltipInfo);
        //}

        //private void OnMouseExit()
        //{
        //    if (Schema == null) return;

        //    ServiceLocator.Instance.UIDisplayProcessor.PopView();
        //}
    }
}