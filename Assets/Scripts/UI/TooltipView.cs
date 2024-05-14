using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Schemas.Actions;

namespace UI
{
    public class TooltipView : View
    {
        [SerializeField] private TextMeshProUGUI m_titleText;
        [SerializeField] private TextMeshProUGUI m_messageText;
        //[SerializeField] private 

        public void Activate(string title, string message)
        {
            m_titleText.text = title;
            m_messageText.text = message;
        }
    }
}