using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Gameplay
{
    /// <summary>
    /// STUB
    /// </summary>
    public class Tile : MonoBehaviour, IPointerClickHandler, IInvoker, ITooltipable
    {
        // STUB - We might want to have multiple places to spawn stuff on the tile. We'll denote the difference with
        // this. However, if we find we don't need it, we can rip it out. We could generalize this further and make it
        // a scriptable object. Could re-use this style of data anchoring in UI as well?
        public enum Anchor
        {
            Center
        }

        public Invoker Invoker { get; private set; } = new Invoker();
        
        [SerializeField] private Highlight m_highlight;
        [SerializeField] private GameObject m_fog;
        [SerializeField] private GameObject m_content;

        [SerializedDictionary("Anchor", "Transform")] 
        [SerializeField] private SerializedDictionary<Anchor, Transform> m_anchors;
        
        [HideInInspector] public Schemas.TileSchema Schema;
        
        public List<Structure> m_structures;

        private void Awake()
        {
            m_structures = new List<Structure>();
            ServiceLocator.Instance.Loadout.SelectedTile.OnChangedValues += OnSelectedTileChanged;
            ToggleFog(true);
        }

        private void OnDestroy()
        {
            ServiceLocator.Instance.Loadout.SelectedTile.OnChangedValues -= OnSelectedTileChanged;
        }

        private void OnSelectedTileChanged(Tile oldValue, Tile newValue)
        {
            m_highlight.ToggleHighlight(newValue == this);
        }

        // Do we want an interface for schema baked items?
        public void SetSchema(Schemas.TileSchema schema)
        {
            Schema = schema;
            Invoker.Initialize(this, Schema);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            // Only left clicks are counted for selecting tiles
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }
            
            ServiceLocator.Instance.Loadout.SelectedTile.Value = this;
        }
        
        public void AddStructure(Schemas.StructureSchema schema, Anchor anchor)
        {
            // If this tile is the Home, we can't place buildings here so we ignore the action (it gets consumed. we 
            // may want to add a generic "IsNotHome" check of some sort
            if (ServiceLocator.Instance.World.IsHome(this))
            {
                return;
            }
            
            // Currently, if you place a building on a tile that has one, we'll just replace it
            foreach (var structure in m_structures)
            {
                Destroy(structure.gameObject);
            }
            m_structures.Clear();
            
            // We spawn in world position and then zero out local position so we can retain the prefab author's
            // scale and rotation information, while manipulating the position
            Structure spawn = Instantiate(schema.Prefab, m_anchors[anchor], true);
            spawn.gameObject.transform.localPosition = Vector3.zero;
            spawn.Initialize(this, schema);
            m_structures.Add(spawn);
        }

        public void ToggleFog(bool on, bool broadcast = true)
        {
            bool wasInFog = IsInFog();

            m_fog.SetActive(on);
            m_content.SetActive(!on);

            if (!broadcast)
            {
                return;
            }
            
            if (wasInFog && !IsInFog())
            {
                ServiceLocator.Instance.World.TriggerTileReveal(this);
                if (!Invoker.AreConditionsMet(Invoker.EventType.TileOnReveal, Invoker.GetDefaultContext()))
                {
                    return;
                }
                
                Invoker.TryInvokeActions(Invoker.EventType.TileOnReveal, Invoker.GetDefaultContext());
            }
        }

        public bool IsInFog()
        {
            return m_fog.activeInHierarchy;
        }

        public int GetStructureCount()
        {
            return m_structures.Count;
        }
        
        // *********************
        // ITooltipable
        // *********************
        public void OpenTooltip()
        {
            throw new System.NotImplementedException();
        }
    }
}
