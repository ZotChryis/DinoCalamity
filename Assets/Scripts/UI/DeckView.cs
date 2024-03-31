using TMPro;
using UnityEngine;

namespace UI
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_count;
        
        private void Start()
        {
            ServiceLocator.Instance.Player.OnShuffleEvent += OnShuffle;
            ServiceLocator.Instance.Player.OnDrawEvent += OnDraw;
            UpdateCountLabel();
        }

        private void OnShuffle(Gameplay.Cards.Card card)
        {
            UpdateCountLabel();
        }
        
        private void OnDraw(Gameplay.Cards.Card card)
        {
            UpdateCountLabel();
        }

        private void UpdateCountLabel()
        {
            m_count.SetText(ServiceLocator.Instance.Player.Deck.CardCount.ToString());
        }
    }
}
