using System;
using Gameplay;
using Schemas;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseCardView : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI m_name;
    [SerializeField] protected TextMeshProUGUI m_description;

    [SerializeField] protected Button m_button;
    [SerializeField] protected Animation m_animation;
    
    [SerializeField] protected Image m_icon;

    public Card SourceCard { get; private set; }
    
    public event Action<BaseCardView> OnCardViewPressedEvent;

    private void Awake()
    {
        m_button.onClick.AddListener(OnButtonPress);
    }

    protected virtual void OnDestroy()
    {
        m_button.onClick.RemoveListener(OnButtonPress);
    }

    public virtual void SetData(Card source)
    {
        SourceCard = source;
        m_name.SetText(source.Schema.Name);
        m_description.SetText(source.Schema.Description);
        m_icon.sprite = source.Schema.Icon;
    }

    private void OnButtonPress()
    {
        OnCardViewPressedEvent?.Invoke(this);
    }
}
