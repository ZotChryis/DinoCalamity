using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CommitView : MonoBehaviour
    {
        [SerializeField] private Button m_button;

        private void Start()
        {
            ServiceLocator.Instance.Player.SelectedCard.OnChanged += CheckCommitStatus;
            ServiceLocator.Instance.Player.SelectedTile.OnChanged += CheckCommitStatus;
            m_button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            ServiceLocator.Instance.Player.PlaySelectedCard();
        }

        private void CheckCommitStatus()
        {
            var selectedCard = ServiceLocator.Instance.Player.SelectedCard.Value;
            m_button.interactable = selectedCard != null && selectedCard.ArePlayConditionsMet();
        }
    }
}
