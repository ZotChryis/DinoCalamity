using UnityEngine;

namespace UI
{
    public class Bank : MonoBehaviour
    {
        [SerializeField] private Resource m_prefab;
        [SerializeField] private Transform m_content;

        public void Start()
        {
            // TEMP: Show that we can pull the data to create the views
            foreach (var resourceSchema in ServiceLocator.Instance.Schemas.Resources)
            {
                Resource resource = Instantiate(m_prefab, m_content);
                resource.SetData(resourceSchema);
            }
        }
    }
}
