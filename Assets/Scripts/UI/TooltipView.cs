using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Schemas;

namespace UI
{
    public class TooltipView : View
    {
        /// <summary>
        /// Holds all information that would be displayed in a tooltip.
        /// </summary>
        [Serializable]
        public class TooltipInfo
        {
            public string title;
            public string description;
            public Sprite sprite;
            public List<Schemas.Action> actions;
        }

        [SerializeField] private TextMeshProUGUI m_titleText;
        [SerializeField] private TextMeshProUGUI m_descriptionText;
        [SerializeField] private Image m_image;
        [SerializeField] private GameObject m_buttonParent;
        [SerializeField] private GameObject m_buttonPrefab;

        // For keeping track of action buttons. Currently unused but may be required to unsub actions from onClick listener.
        private List<TooltipActionButton> actionButtons = new List<TooltipActionButton>();

        private Vector3 m_WorldPos;

        public delegate void TooltipButtonDelegate();

        public override void Teardown()
        {
            base.Teardown();
            // TODO: Unsub action buttons from addListener?
        }

        public void SetData(TooltipInfo tooltip, Vector3 worldPos)
        {
            // Set title text
            if (m_titleText != null && tooltip.title != null)
            {
                m_titleText.text = tooltip.title;
            } else
            {
                m_titleText.gameObject.SetActive(false);
            }

            // Set description/message text
            if (m_descriptionText != null && tooltip.description != null)
            {
                m_descriptionText.text = tooltip.description;
            }
            else
            {
                m_descriptionText.gameObject.SetActive(false);
            }

            // Set image
            if (m_image != null && tooltip.sprite != null)
            {
                m_image.sprite = tooltip.sprite;
            }
            else
            {
                m_image.gameObject.SetActive(false);
            }

            // Create action buttons -> Set actions to buttons.
            foreach (Schemas.Action action in tooltip.actions)
            {
                var button = Instantiate(m_buttonPrefab, m_buttonParent.transform).GetComponent<TooltipActionButton>();
                button.SetAction(action);
                actionButtons.Add(button);
            }

            m_WorldPos = worldPos;
            UpdateScreenPosition();
        }

        public void LateUpdate()
        {
            // Move the screen position to match the world position.
            // Will be able to move offscreen. TODO: If offscreen, align to an edge?
            UpdateScreenPosition();
        }

        private void UpdateScreenPosition()
        {
            var screenPos = Camera.main.WorldToScreenPoint(m_WorldPos);
            transform.position = screenPos;
        }
    }
}