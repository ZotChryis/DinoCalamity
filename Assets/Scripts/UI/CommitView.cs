using GameStates;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class CommitView : MonoBehaviour
    {
        [SerializeField] private Button m_button;

        private void Awake()
        {
            // STUB - some game manager should be handling this
            ServiceLocator.Instance.StateMachine.OnStateChangedEvent += OnStateChangedEvent;
            ServiceLocator.Instance.Player.SelectedCard.OnChanged += CheckCommitStatus;
            ServiceLocator.Instance.Player.SelectedTile.OnChanged += CheckCommitStatus;
            m_button.onClick.AddListener(OnButtonClicked);
        }

        private void OnStateChangedEvent(IState state)
        {
            CheckCommitStatus();
        }

        private void OnButtonClicked()
        {
            ServiceLocator.Instance.Player.PlaySelectedCard();
        }

        private void CheckCommitStatus()
        {
            var isPlayState = ServiceLocator.Instance.StateMachine.GetCurrentState() is StatePlay;
            var selectedCard = ServiceLocator.Instance.Player.SelectedCard.Value;
            m_button.interactable = isPlayState && selectedCard != null && selectedCard.ArePlayConditionsMet();
        }
    }
}
