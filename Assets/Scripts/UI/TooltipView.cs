using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class TooltipView : View
    {
        public struct TooltipInfo
        {
            public string Title;
            public string Message;
            public Sprite Icon;
            public List<Schemas.Action> Actions;

            /// <summary>
            /// Wraps all info needed to create a tooltip popup.
            /// Inputting a name and message are required.
            /// Inputting an image icon and list of actions is optional.
            /// </summary>
            /// <param name="title">Required. Tooltip name/title.</param>
            /// <param name="message">Required. Main tooltip message.</param>
            /// <param name="icon">Optional. Tooltip image.</param>
            /// <param name="actions">Optional. A list of actions to be displayed as buttons in the tooltip.</param>
            public TooltipInfo(string title, string message, Sprite icon = null, List<Schemas.Action> actions = null)
            {
                Title = title;
                Message = message;
                Icon = icon;
                Actions = actions;
            }
        }

        [SerializeField] private TextMeshProUGUI m_titleText;
        [SerializeField] private TextMeshProUGUI m_messageText;
        [SerializeField] private Image m_icon;
        [SerializeField] private GameObject m_buttonParent;
        [SerializeField] private GameObject m_buttonPrefab;

        // For keeping track of action buttons. Currently unused but may be required to unsub actions from onClick listener.
        private List<TooltipActionButton> actionButtons = new List<TooltipActionButton>();

        // private Vector3 m_WorldPos;

        public delegate void TooltipButtonDelegate();

        public override void Teardown()
        {
            base.Teardown();
            // TODO: Unsub action buttons from addListener?
        }

        public void SetData(TooltipInfo tooltip)
        {
            // Set title text
            if (m_titleText != null && tooltip.Title != null)
            {
                m_titleText.text = tooltip.Title;
            } else
            {
                m_titleText.gameObject.SetActive(false);
            }

            // Set description/message text
            if (m_messageText != null && tooltip.Message != null)
            {
                m_messageText.text = tooltip.Message;
            }
            else
            {
                m_messageText.gameObject.SetActive(false);
            }

            // Set image
            if (m_icon != null && tooltip.Icon != null)
            {
                m_icon.sprite = tooltip.Icon;
            }
            else
            {
                m_icon.gameObject.SetActive(false);
            }

            // Create action buttons -> Set actions to buttons.
            foreach (Schemas.Action action in tooltip.Actions)
            {
                var button = Instantiate(m_buttonPrefab, m_buttonParent.transform).GetComponent<TooltipActionButton>();
                button.SetAction(action);
                actionButtons.Add(button);
            }
        }
    }
}