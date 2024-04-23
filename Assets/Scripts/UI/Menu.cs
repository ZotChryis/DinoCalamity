using UnityEngine;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        private const string c_animShow = "Menu_Show";
        private const string c_animHide = "Menu_Hide";

        [SerializeField] private Animation m_animation;

        public void Show()
        {
            m_animation.Play(c_animShow);
        }
        
        public void Hide()
        {
            m_animation.Play(c_animHide);
        }
    }
}
