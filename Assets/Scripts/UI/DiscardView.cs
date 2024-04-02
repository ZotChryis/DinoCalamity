using Gameplay.Cards;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DiscardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_count;

        private void Start()
        {
            ServiceLocator.Instance.Player.Discard.CardCount.OnChanged += UpdateCountLabel;
            UpdateCountLabel();
        }

        private void UpdateCountLabel()
        {
            m_count.SetText(ServiceLocator.Instance.Player.Discard.CardCount.ToString());
        }
    }
}
