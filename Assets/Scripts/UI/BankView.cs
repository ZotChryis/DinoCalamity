using UnityEngine;

namespace UI
{
    public class BankView : MonoBehaviour
    {
        [SerializeField] private ResourceView m_prefab;
        [SerializeField] private Transform m_content;

        public void Awake()
        {
            // TEMP: Show that we can pull the data to create the views
            foreach (var resourceSchema in ServiceLocator.Instance.Schemas.Resources)
            {
                ResourceView resource = Instantiate(m_prefab, m_content);
                resource.SetData(resourceSchema);
            }
        }
    }
}
