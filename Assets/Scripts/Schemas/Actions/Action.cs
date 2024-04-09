using System;
using Gameplay;
using Schemas.Checks;

namespace Schemas
{
    [Serializable]
    public abstract class Action : Schema
    {
        public abstract void Invoke(Invoker.Context context);
    }
    
    [Serializable]
    public class ActionEvent
    {
        public Check[] Checks;
        public Action[] Actions;
    }
}