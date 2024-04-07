using System;
using Schemas.Checks;

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
    
    [Serializable]
    public class ActionEvent
    {
        public Check[] Checks;
        public Action[] Actions;
    }
}