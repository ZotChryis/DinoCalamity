using TMPro;
using UnityEngine;

namespace UI
{
    public class DiscardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_count;

        // TEMP: Just to show that the events are working, we'll keep track of count this way.
        //       Ideally, the Discard class may have its own events? not sure, this is all off the cuff!
        private int m_cardCount = 0;
        
        private void Start()
        {
            ServiceLocator.Instance.Player.OnDiscardEvent += OnDiscard;
            UpdateCountLabel();
        }

        private void OnDiscard(Gameplay.Cards.Card card)
        {
            m_cardCount++;
            UpdateCountLabel();
        }
        
        private void UpdateCountLabel()
        {
            m_count.SetText(m_cardCount.ToString());
        }
    }
}
