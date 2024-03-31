using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_amount;
        [SerializeField] private TextMeshProUGUI m_name;
        [SerializeField] private Image m_icon;

        private Schemas.Resource m_data;
        
        public void SetData(Schemas.Resource data)
        {
            m_data = data;
            m_name.SetText(data.Name);
            m_amount.SetText("0");
            m_icon.sprite = data.Icon;
            
            ServiceLocator.Instance.Bank.OnResourceUpdateEvent += OnResourceUpdateEvent;
        }
        
        private void OnResourceUpdateEvent(Schemas.Resource resource, int currentTotal)
        {
            if (resource != m_data)
            {
                return;
            }
            
            m_amount.SetText(currentTotal.ToString());
        }
    }
}
