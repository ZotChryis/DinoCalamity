using TMPro;
using UnityEngine;

namespace UI
{
    public class CalamityView : View
    {
        [SerializeField] private TMP_Text m_title;
        [SerializeField] private TMP_Text m_description;

        public void Start()
        {
            HandleTurn();
            ServiceLocator.Instance.GameManager.Turn.OnChanged += HandleTurn;
        }

        private void HandleTurn()
        {
            // hack: make a real 'discover' system with visions
            // for now, lets assume turn 20 is when you get it revealed
            var revealed = ServiceLocator.Instance.GameManager.Turn.Value >= 20;
            var calamitySchema = ServiceLocator.Instance.GameManager.Calamity.Schema;
            m_title.SetText(revealed ? calamitySchema.Name : "?????");
            m_description.SetText(revealed ? calamitySchema.Description : "Find out on turn 20...");
        }
    }
}
