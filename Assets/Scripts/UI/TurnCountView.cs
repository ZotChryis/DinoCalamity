using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Utility;
using GameStates;

public class TurnCountView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_turnCounter;

    private void Awake()
    {
        ServiceLocator.Instance.GameManager.StateMachine.OnStateChangedEvent += OnStateChangedEvent;
    }

    private void OnStateChangedEvent(IState state)
    {
        //m_bum_turnCountertton.interactable = state is StatePlay;
        m_turnCounter.text = ServiceLocator.Instance.GameManager.Turn.ToString();
    }
}
