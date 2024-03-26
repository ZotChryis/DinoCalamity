using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_name;
        [SerializeField] private Image m_icon;
        [SerializeField] private Button m_button;

        private Schemas.Card m_data;

        public void SetData(Schemas.Card data)
        {
            m_data = data;
            m_name.SetText(data.Name);
            m_icon.sprite = data.Icon;
        }
        
        private void Start()
        {
            m_button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            for (var i = 0; i < m_data.Actions.Length; i++)
            {
                // TEMP AS SHIT
                m_data.Actions[i].Invoke(gameObject);
            }
        }
    }
}
