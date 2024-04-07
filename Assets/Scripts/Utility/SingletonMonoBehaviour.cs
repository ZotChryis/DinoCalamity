using UnityEngine;

namespace Utility
{
    public class SingletonMonoBehaviour : MonoBehaviour
    {
        protected static SingletonMonoBehaviour InternalInstance { get; private set; }

        protected virtual void Awake()
        {
            if (InternalInstance != null && InternalInstance != this)
            {
                Destroy(this);
                return;
            }

            InternalInstance = this;
        }
    }
}