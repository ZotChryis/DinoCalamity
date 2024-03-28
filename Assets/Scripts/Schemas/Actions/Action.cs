using System;
using UnityEngine;

namespace Schemas
{
    [Serializable]
    public abstract class Action : ScriptableObject
    {
        public abstract void Invoke();
    }
}