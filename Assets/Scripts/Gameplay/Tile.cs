using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    /// <summary>
    /// STUB
    /// </summary>
    public class Tile : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Highlight m_highlight;
        
        private Schemas.Tile m_schema;

        private void Start()
        {
            ServiceLocator.Instance.Player.SelectedTile.OnChangedValues += OnSelectedTileChanged;
        }

        private void OnSelectedTileChanged(Tile oldValue, Tile newValue)
        {
            m_highlight.ToggleHighlight(newValue == this);
        }

        // TODO: Associate schema to this world tile
        public void SetSchema(Schemas.Tile schema)
        {
            m_schema = schema;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            // Only left clicks are counted for selecting tiles
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }
            
            ServiceLocator.Instance.Player.SelectedTile.Value = this;
        }
    }
}
