using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
    [Header ("Base View")]
    [SerializeField]
    private List<Button> m_closeButtons;

    /// <summary>
    /// Called by the parent of this View to handle showing. 
    /// </summary>
    public virtual void Activate()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Called by the parent of this View to handle hiding. 
    /// </summary>
    public virtual void Deactivate()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// This gets called once by the UIDisplayProcessor when this is created and initialized. 
    /// </summary>
    public virtual void Setup()
    {
        foreach(var closeButton in m_closeButtons)
        {
            closeButton.onClick.AddListener(OnCloseButtonPressed);
        }
    }

    /// <summary>
    /// This gets called once by the UIDisplayProcessor before this is destroyed.
    /// </summary>
    public virtual void Teardown()
    {
        foreach (var closeButton in m_closeButtons)
        {
            closeButton.onClick.RemoveListener(OnCloseButtonPressed);
        }
    }

    private void OnCloseButtonPressed()
    {
        ServiceLocator.Instance.UIDisplayProcessor.PopView();
    }
}
