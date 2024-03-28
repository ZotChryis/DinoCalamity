using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Commit : MonoBehaviour
    {
        [SerializeField] private Button m_button;

        private void Start()
        {
            ServiceLocator.Instance.Player.SelectedCard.OnChangedValues += OnSelectedCardChanged;
            m_button.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            ServiceLocator.Instance.Player.PlaySelectedCard();
        }

        private void OnSelectedCardChanged(Gameplay.Cards.Card oldValue, Gameplay.Cards.Card newValue)
        {
            m_button.interactable = newValue != null && newValue.AreConditionsMet();
        }
    }
}
