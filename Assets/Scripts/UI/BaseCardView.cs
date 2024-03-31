using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseCardView : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI m_name;
    [SerializeField] protected Image m_icon;
    [SerializeField] protected Button m_button;

    [SerializeField] protected Animation m_animation;

    protected Gameplay.Cards.Card m_source;

    public virtual void SetSource(Gameplay.Cards.Card source)
    {
        m_source = source;
        m_name.SetText(source.Data.Name);
        m_icon.sprite = source.Data.Icon;
    }
}
