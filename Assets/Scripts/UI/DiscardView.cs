using Gameplay.Cards;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DiscardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_count;

        private void Awake()
        {
            ServiceLocator.Instance.Loadout.Discard.CardCount.OnChanged += UpdateCountLabel;
            UpdateCountLabel();
        }

        private void UpdateCountLabel()
        {
            m_count.SetText(ServiceLocator.Instance.Loadout.Discard.CardCount.ToString());
        }
    }
}
