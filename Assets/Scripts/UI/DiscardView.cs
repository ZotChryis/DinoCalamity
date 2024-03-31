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
            ServiceLocator.Instance.Player.OnShuffleEvent += OnShuffle;
            ServiceLocator.Instance.Player.OnDiscardEvent += OnDiscard;
            UpdateCountLabel();
        }

        private void OnShuffle(Card card)
        {
            UpdateCountLabel();
        }

        private void OnDiscard(Gameplay.Cards.Card card)
        {
            UpdateCountLabel();
        }
        
        private void UpdateCountLabel()
        {
            m_count.SetText(ServiceLocator.Instance.Player.Discard.CardCount.ToString());
        }
    }
}
