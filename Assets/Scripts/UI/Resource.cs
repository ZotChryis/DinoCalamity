using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_amount;
        [SerializeField] private TextMeshProUGUI m_name;
        [SerializeField] private Image m_icon;

        public void SetData(Schemas.Resource data)
        {
            m_name.SetText(data.Name);
            m_icon.sprite = data.Icon;
        }
    }
}
