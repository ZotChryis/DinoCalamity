using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CalamityView : View
    {
        [SerializeField] private TMP_Text m_title;
        [SerializeField] private TMP_Text m_description;

        [SerializeField] private Transform m_visionsRoot;
        [SerializeField] private VisionView m_visionPrefab;

        private List<VisionView> m_visions = new List<VisionView>();

        public void Start()
        {
            HandleTurn();
            HandleVisions();
            ServiceLocator.Instance.GameManager.Turn.OnChanged += HandleTurn;
            ServiceLocator.Instance.GameManager.Calamity.OnVisionEvent += HandleVisions;
        }

        public void OnDestroy()
        {
            ServiceLocator.Instance.GameManager.Turn.OnChanged -= HandleTurn;
            ServiceLocator.Instance.GameManager.Calamity.OnVisionEvent -= HandleVisions;
        }

        private void HandleVisions()
        {
            // Destructive, we clear all then rebuild. We should optimize this
            foreach (var vision in m_visions)
            {
                Destroy(vision.gameObject);
            }
            m_visions.Clear();
            
            foreach (var vision in ServiceLocator.Instance.GameManager.Calamity.Visions)
            {
                VisionView visionView = Instantiate(m_visionPrefab, m_visionsRoot);
                visionView.SetSchema(vision.Schema);
                m_visions.Add(visionView);
            }
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
