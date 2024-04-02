using System;

namespace Schemas
{
    [Serializable]
    public abstract class Action : Schema
    {
        public abstract void Invoke();
    }
}