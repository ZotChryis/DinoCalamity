using System.Collections;
using System.Collections.Generic;
using Schemas;
using UI;
using UnityEngine;
using static Schemas.ViewMapSchema;

public class UIDisplayProcessor : MonoBehaviour
{
    [SerializeField]
    private ViewMapSchema m_viewMap;

    [SerializeField]
    private Transform m_popupParentTransform;

    private List<View> m_activeViews;

    public View TryShowView(ViewType viewType)
    {
        if (!m_viewMap.ViewConfigs.TryGetValue(viewType, out ViewSchema config))
        {
            return null;
        }

        return TryShowView(config);
    }

    public View TryShowView(ViewSchema config)
    {
        if (config.ViewPrefab == null)
        {
            return null;
        }

        var view = Instantiate(config.ViewPrefab, m_popupParentTransform);
        m_activeViews.Add(view);
        view.Setup();
        view.Activate();
        return view;
    }

    public void PopView()
    {
        if (m_activeViews.Count == 0)
        {
            return;
        }

        var indexToPop = m_activeViews.Count - 1;
        RemoveViewAtIndex(indexToPop);

    }

    private void Awake()
    {
        m_activeViews = new List<View>();
        ServiceLocator.Instance.RegisterUIDisplayProcessor(this);
    }

    private void OnDestroy()
    {
        ClearViews();
    }

    private void RemoveViewAtIndex(int index)
    {
        if(index > m_activeViews.Count - 1 || index < 0)
        {
            return;
        }

        View view = m_activeViews[index];

        view.Deactivate();
        view.Teardown();
        m_activeViews.RemoveAt(index);
        Destroy(view.gameObject);
    }

    private void ClearViews()
    {
        foreach (var baseView in m_activeViews)
        {
            baseView.Teardown();
            Destroy(baseView);
        }
        m_activeViews.Clear();
    }
}
