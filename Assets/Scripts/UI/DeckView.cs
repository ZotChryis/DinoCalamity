using TMPro;
using UnityEngine;

namespace UI
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_count;
        
        private void Start()
        {
            ServiceLocator.Instance.Loadout.OnShuffleEvent += OnShuffle;
            ServiceLocator.Instance.Loadout.OnDrawEvent += OnDraw;
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
            m_count.SetText(ServiceLocator.Instance.Loadout.Deck.CardCount.ToString());
        }
    }
}
