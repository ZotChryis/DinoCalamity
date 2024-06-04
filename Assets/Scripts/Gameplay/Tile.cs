using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    /// <summary>
    /// STUB
    /// </summary>
    public class Tile : MonoBehaviour, IPointerClickHandler, IInvoker
    {
        public static int c_maxCapacity = 4;
        
        public Invoker Invoker { get; private set; } = new Invoker();
        
        [SerializeField] private Highlight m_highlight;
        [SerializeField] private GameObject m_fog;
        [SerializeField] private GameObject m_content;
        [SerializeField] private Transform[] m_buildingLocations = new Transform[c_maxCapacity];

        [HideInInspector] public Schemas.TileSchema Schema;
        
        private List<Structure> m_structures;
        private int m_capacity;

        // Public viewable list of structures. TODO: The structures in the list are still editable.
        public List<Structure> Structures => new List<Structure>(m_structures);

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
    }
}
