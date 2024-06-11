using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    /// <summary>
    /// STUB
    /// </summary>
    public class Tile : MonoBehaviour, IPointerClickHandler, IInvoker, ITooltipable
    {
        public static int c_maxCapacity = 4;
        
        public Invoker Invoker { get; private set; } = new Invoker();
        
        [SerializeField] private Highlight m_highlight;
        [SerializeField] private GameObject m_fog;
        [SerializeField] private GameObject m_content;
        [SerializeField] private Transform[] m_buildingLocations = new Transform[c_maxCapacity];

        [HideInInspector] public Schemas.TileSchema Schema;
        
        public List<Structure> m_structures;
        private int m_capacity;

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

            m_capacity = schema.Capacity;
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
        
        public void AddStructure(Schemas.StructureSchema schema)
        {
            // This tile is full!
            if (m_structures.Count >= m_capacity)
            {
                return;
            }
            
            // We spawn in world position and then zero out local position so we can retain the prefab author's
            // scale and rotation information, while manipulating the position
            Structure spawn = Instantiate(schema.Prefab, m_buildingLocations[m_structures.Count], true);
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

        public bool HasCapacity()
        {
            return m_structures.Count < m_capacity;
        }
        
        public int GetStructureCount()
        {
            return m_structures.Count;
        }
        
        // *********************
        // ITooltipable
        // *********************

        public View OpenTooltip()
        {
            return OpenTooltip(Vector3.zero);
        }

        public View OpenTooltip(Vector3 offset)
        {
            // Using a list to create individual tooltip items for the tile and each structure on it.
            List<TooltipView.TooltipInfo> tooltips = new List<TooltipView.TooltipInfo>();
            
            // Add tile info
            tooltips.Add(new TooltipView.TooltipInfo(Schema.Name, Schema.tooltipMessage, Schema.tooltipIcon, Schema.tooltipActions));
            
            // Add info for each structure. TODO: Make this a for loop if more than one structure on the tile.
            if (m_structures.Count > 0)
            {
                // Open tooltip
                var structure = m_structures[0]; // TODO: Make this a for loop with [i];
                tooltips.Add(new TooltipView.TooltipInfo(structure.Schema.tooltipName, structure.Schema.tooltipMessage, structure.Schema.tooltipIcon, structure.Schema.tooltipActions));
            }
            
            // Open tooltip
            var view = ServiceLocator.Instance.UIDisplayProcessor.TryShowView(Schemas.ViewMapSchema.ViewType.TooltipStack);
            var tooltipStackView = view as TooltipStackView;

            var pos = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z + offset.z);
            tooltipStackView?.SetData(tooltips, pos);

            return tooltipStackView;
        }

        public void CloseTooltip()
        {
            ServiceLocator.Instance.UIDisplayProcessor.PopView();
        }
    }
}
