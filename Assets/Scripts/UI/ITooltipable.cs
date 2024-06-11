using UnityEngine;

namespace UI
{
    public interface ITooltipable
    {
        public void OpenTooltip();
        public void OpenTooltip(Vector3 offset);


        public void CloseTooltip();
    }
}