using UnityEngine;

namespace Schemas
{
    /// <summary>
    /// The data definition for a Popup View.
    /// Create new entries via the asset create menu.
    /// </summary>
    [CreateAssetMenu]
    public class ViewConfig : ScriptableObject
    {
        public View ViewPrefab;
    }
}