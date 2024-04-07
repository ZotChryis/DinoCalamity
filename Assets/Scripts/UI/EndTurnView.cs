using GameStates;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class EndTurnView : MonoBehaviour
    {
        [SerializeField] private Button m_button;

        private void Awake()
        {
            // STUB - some game manager should be handling this
            ServiceLocator.Instance.StateMachine.OnStateChangedEvent += OnStateChangedEvent;
            m_button.onClick.AddListener(OnButtonClicked);
        }

        private void OnStateChangedEvent(IState state)
        {
            m_button.interactable = state is StatePlay;
        }

        public void OnButtonClicked()
        {
            // STUB - some game manager should be handling this
            //ServiceLocator.Instance.StateMachine.ChangeState(new StateGeneration());
            ServiceLocator.Instance.GameManager.EndTurn();
        }
    }
}
