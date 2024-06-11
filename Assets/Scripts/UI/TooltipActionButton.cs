using UnityEngine;
using UnityEngine.UI;
using Schemas.Actions;
using Gameplay;
using TMPro;

namespace UI
{
    public class TooltipActionButton : MonoBehaviour, IInvoker
    {
        [SerializeField] private Schemas.Action m_action;
        [SerializeField] private TextMeshProUGUI m_buttonText;

        public Invoker Invoker { get; private set; } = new Invoker();

        /// <summary>
        /// Set the button's action. Subscribes to the Button component's onClick event.
        /// </summary>
        /// <param name="action"></param>
        public void SetAction(Schemas.Action action)
        {
            // TODO: Might need to initialize Invoker somewhere.
            m_action = action;
            gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        }

        /// <summary>
        /// Set the button text.
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text)
        {
            m_buttonText.text = text;
        }

        public void OnClick()
        {
            m_action?.Invoke(Invoker.GetDefaultContext());
        }
    }
}