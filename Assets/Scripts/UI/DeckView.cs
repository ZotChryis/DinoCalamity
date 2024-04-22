using TMPro;
using UnityEngine;

namespace UI
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_count;
        
        private void Start()
        {
            ServiceLocator.Instance.Loadout.Deck.CardCount.OnChanged += UpdateCountLabel;
            UpdateCountLabel();
        }

        private void UpdateCountLabel()
        {
            m_count.SetText(ServiceLocator.Instance.Loadout.Deck.CardCount.ToString());
        }
    }
}
