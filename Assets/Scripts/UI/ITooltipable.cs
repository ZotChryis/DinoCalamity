using System.Numerics;

namespace UI
{
    public interface ITooltipable
    {
        public void OpenTooltip();

        public void CloseTooltip()
        {
            ServiceLocator.Instance.UIDisplayProcessor.PopView();
        }
    }
}