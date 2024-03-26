using System;
using UnityEngine;

namespace Schemas
{
    [Serializable]
    public abstract class Action : ScriptableObject
    {
        public enum PlayPeriod
        {
            OnPlay
        }
        
        public abstract void Invoke(GameObject source);
    }
}