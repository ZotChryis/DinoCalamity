using UnityEngine;
using UnityEngine.UI;
using Schemas.Actions;
using Gameplay;

namespace UI
{
    public class TooltipActionButton : MonoBehaviour, IInvoker
    {
        [SerializeField] private Schemas.Action m_action;

        public Invoker Invoker { get; private set; } = new Invoker();

        public void SetAction(Schemas.Action action)
        {
            m_action = action;
            gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            // TODO: Might need to initialize Invoker somewhere.
            Debug.Log($"TooltipActionButton: OnClick triggered.");
            m_action?.Invoke(Invoker.GetDefaultContext());
        }
    }
}