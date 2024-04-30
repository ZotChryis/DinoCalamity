using System;
using Schemas;
using TMPro;
using UnityEngine;

namespace UI
{
    public class FaithView : MonoBehaviour
    {
        private const string c_format = "{0} / {1}";

        [SerializeField] private TMP_Text m_label;

        private void Start()
        {
            UpdateLabel();
            ServiceLocator.Instance.Bank.OnResourceUpdateEvent += OnResourcesUpdated;
            ServiceLocator.Instance.Bank.FaithMax.OnChanged += UpdateLabel;
        }

        private void OnDestroy()
        {
            ServiceLocator.Instance.Bank.OnResourceUpdateEvent -= OnResourcesUpdated;
            ServiceLocator.Instance.Bank.FaithMax.OnChanged -= UpdateLabel;
        }

        private void OnResourcesUpdated(ResourceSchema.ResourceType resourceType, int value)
        {
            if (resourceType != ResourceSchema.ResourceType.Faith)
            {
                return;
            }
            
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            string text = string.Format(
                c_format,
                ServiceLocator.Instance.Bank.GetAmount(ResourceSchema.ResourceType.Faith),
                ServiceLocator.Instance.Bank.FaithMax.Value
            );

            m_label.SetText(text);
        }
    }
}

