using UnityEngine;

namespace UI
{
    public class LoadoutMenu : Menu
    {
        [SerializeField] private LoadoutButton m_prefab;
        [SerializeField] private Transform m_backItem;
        [SerializeField] private Transform m_root;
        
        private void Start()
        {
            foreach (var loadoutSchema in ServiceLocator.Instance.Schemas.Loadouts)
            {
                LoadoutButton loadoutButton = Instantiate(m_prefab, m_root);
                loadoutButton.SetSchema(loadoutSchema);
            }
            
            m_backItem.transform.SetAsLastSibling();
        }
    }
}
