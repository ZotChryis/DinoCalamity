using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DiscoverCardsView : View
    {
        [SerializeField]
        private TextMeshProUGUI m_titleText;

        [SerializeField]
        private BaseCardView m_cardPrefab;

        [SerializeField]
        private Transform m_cardsParentTransform;

        public override void Setup()
        {
            base.Setup();
        }

        public override void Teardown()
        {
            base.Teardown();
        }

        public override void Activate()
        {
            base.Activate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        public void SetData()
        {

        }



    }
}
