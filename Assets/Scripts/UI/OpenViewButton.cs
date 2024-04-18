using Schemas;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OpenViewButton : MonoBehaviour
    {
        [SerializeField] private Button m_button;
        [SerializeField] private ViewMapSchema.ViewType m_view;

        private void Start()
        {
            m_button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            ServiceLocator.Instance.UIDisplayProcessor.TryShowView(m_view);
        }
    }
}
