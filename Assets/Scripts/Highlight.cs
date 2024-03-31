using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quick and dirty World-object mesh highlight. It works by overloading the _EMISSION and _EmissionColor overwrites
/// on the standard Unity Lit material.
/// </summary>
public class Highlight : MonoBehaviour
{
    [SerializeField] private Color color = Color.white;

    private List<Material> m_materials;

    private void Awake()
    {
        m_materials = new List<Material>();
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            m_materials.AddRange(new List<Material>(renderer.materials));
        }
    }

    public void ToggleHighlight(bool value)
    {
        foreach (var material in m_materials)
        {
            if (value)
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", color);
            }
            else
            {
                material.DisableKeyword("_EMISSION");
            }
        }
    }
}