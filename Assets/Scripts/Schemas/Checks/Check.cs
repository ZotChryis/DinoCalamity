using System;

namespace Schemas.Checks
{
    [Serializable]
    public abstract class Check : Schema
    {
        public class Context
        {
            public Gameplay.Tile SelectedTile;
        }
        
        public abstract bool IsValid(Context context);
    }
}
