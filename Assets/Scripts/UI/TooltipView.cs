using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Schemas.Actions;

namespace UI
{
    public class TooltipView : View
    {
        /// <summary>
        /// Holds all information that would be displayed in a tooltip.
        /// </summary>
        [Serializable]
        public class TooltipDescription
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

        public delegate void TooltipButtonDelegate();

        public void SetData(TooltipDescription tooltip)
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

            // TODO: Create action buttons
            foreach (Schemas.Action action in tooltip.actions)
            {
                var button = Instantiate(m_buttonPrefab, m_buttonParent.transform);
                // TODO: Assign actions to the button.
                // TODO: Make a cleanup for the buttons?
                //button.GetComponent<TooltipActionButton>().;
            }
        }
    }
}