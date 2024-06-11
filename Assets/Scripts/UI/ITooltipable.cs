using UnityEngine;

namespace UI
{
    public interface ITooltipable
    {
        public View OpenTooltip();
        public View OpenTooltip(Vector3 offset);


        public void CloseTooltip();
    }
}