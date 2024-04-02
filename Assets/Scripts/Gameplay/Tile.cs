using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using GameStates;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace Gameplay
{
    /// <summary>
    /// STUB
    /// </summary>
    public class Tile : MonoBehaviour, IPointerClickHandler
    {
        // STUB - We might want to have multiple places to spawn stuff on the tile. We'll denote the difference with
        // this. However, if we find we don't need it, we can rip it out. We could generalize this further and make it
        // a scriptable object. Could re-use this style of data anchoring in UI as well?
        public enum Anchor
        {
            Center
        }
        
        [SerializeField] private Highlight m_highlight;

        [SerializedDictionary("Anchor", "Transform")] 
        [SerializeField] private SerializedDictionary<Anchor, Transform> Anchors;
            
        private Schemas.Tile m_schema;
        private List<Structure> m_structures;

        private void Awake()
        {
            m_structures = new List<Structure>();
            ServiceLocator.Instance.Player.SelectedTile.OnChangedValues += OnSelectedTileChanged;
        }

        private void OnSelectedTileChanged(Tile oldValue, Tile newValue)
        {
            m_highlight.ToggleHighlight(newValue == this);
        }

        // Do we want an interface for schema baked items?
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
        
        public void AddStructure(Schemas.Structure schema, Anchor anchor)
        {
            // We spawn in world position and then zero out local position so we can retain the prefab author's
            // scale and rotation information, while manipulating the position
            Structure spawn = Instantiate(schema.Prefab, Anchors[anchor], true);
            spawn.gameObject.transform.localPosition = Vector3.zero;
            spawn.SetSchema(schema);
            m_structures.Add(spawn);
        }
    }
}
