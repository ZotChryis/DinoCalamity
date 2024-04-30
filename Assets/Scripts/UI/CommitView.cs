using Gameplay;
using GameStates;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class CommitView : MonoBehaviour
    {
        [SerializeField] private Button m_button;

        private void Start()
        {
            // STUB - some game manager should be handling this
            ServiceLocator.Instance.StateMachine.OnStateChangedEvent += OnStateChangedEvent;
            ServiceLocator.Instance.Loadout.SelectedCard.OnChanged += CheckCommitStatus;
            ServiceLocator.Instance.Loadout.SelectedTile.OnChanged += CheckCommitStatus;
            m_button.onClick.AddListener(OnButtonClicked);
        }

        private void OnStateChangedEvent(IState state)
        {
            CheckCommitStatus();
        }

        private void OnButtonClicked()
        {
            ServiceLocator.Instance.Loadout.PlaySelectedCard();
        }

        private void CheckCommitStatus()
        {
            var isPlayState = ServiceLocator.Instance.StateMachine.GetCurrentState() is StatePlay;
            var selectedCard = ServiceLocator.Instance.Loadout.SelectedCard.Value;
            m_button.interactable = isPlayState && 
                                    selectedCard != null &&
                                    selectedCard.CanAllCostsBePaid() &&
                                    selectedCard.Invoker.AreConditionsMet(
                                        Invoker.EventType.CardOnPlay,
                                        selectedCard.Invoker.GetDefaultContext()
                                    );
        }
    }
}
