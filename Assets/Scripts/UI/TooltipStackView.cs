using System.Collections.Generic;
using Schemas;
using UnityEngine;

namespace UI
{
    public class TooltipStackView: View
    {
        // Parent with VerticalStack component to stack tooltip items.
        [SerializeField] private Transform m_stackParent;
        
        // Single tooltip schema.
        [SerializeField] private ViewSchema m_tooltipSchema;
        
        private Vector3 m_WorldPos;

        public void SetData(List<TooltipView.TooltipInfo> tooltips, Vector3 worldPos)
        {
            foreach (var tooltip in tooltips)
            {
                var view = Instantiate(m_tooltipSchema.ViewPrefab, m_stackParent).GetComponent<TooltipView>();
                Debug.Log($"TooltipStackView: {tooltip.Title}.");
                view.SetData(tooltip);
            }
            
            m_WorldPos = worldPos;
            UpdateScreenPosition();
        }
        
        public void LateUpdate()
        {
            // Move the screen position to match the world position.
            UpdateScreenPosition();
        }

        /// <summary>
        /// Moves the tooltip stack to follow the original world position on the screen.
        /// </summary>
        private void UpdateScreenPosition()
        {
            var screenPos = Camera.main.WorldToScreenPoint(m_WorldPos);
            transform.position = screenPos;
        }
    }
}