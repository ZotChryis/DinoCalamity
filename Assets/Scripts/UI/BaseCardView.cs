using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseCardView : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI m_name;
    [SerializeField] protected Image m_icon;
    [SerializeField] protected Button m_button;

    [SerializeField] protected Animation m_animation;

    public Gameplay.Cards.Card SourceCard => m_source;

    protected Gameplay.Cards.Card m_source;

    public event Action<BaseCardView> OnCardViewPressedEvent;

    private void Awake()
    {
        m_button.onClick.AddListener(OnButtonPress);
    }

    protected virtual void OnDestroy()
    {
        m_button.onClick.RemoveListener(OnButtonPress);
    }

    public virtual void SetData(Gameplay.Cards.Card source)
    {
        m_source = source;
        m_name.SetText(source.Schema.Name);
        m_icon.sprite = source.Schema.Icon;
    }

    private void OnButtonPress()
    {
        OnCardViewPressedEvent?.Invoke(this);
    }
}
