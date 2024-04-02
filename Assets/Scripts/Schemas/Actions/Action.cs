using System;

namespace Schemas
{
    [Serializable]
    public abstract class Action : Schema
    {
        public enum EventType
        {
            OnDraw,
            OnDiscard,
            OnShuffle,
            OnPlay,
            OnGeneration
        }
        
        public abstract void Invoke();
    }
}