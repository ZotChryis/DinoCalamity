using UnityEngine;

namespace UI
{
    public class BankView : MonoBehaviour
    {
        [SerializeField] private ResourceView m_prefab;
        [SerializeField] private Transform m_refined;
        [SerializeField] private Transform m_unrefined;

        public void Start()
        {
            // TEMP: Show that we can pull the data to create the views
            foreach (var resourceSchema in ServiceLocator.Instance.Schemas.Resources)
            {
                ResourceView resource = Instantiate(m_prefab, resourceSchema.IsRefined() ? m_refined : m_unrefined);
                resource.SetData(resourceSchema);
            }
        }
    }
}
