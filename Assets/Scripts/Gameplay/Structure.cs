using UnityEngine;

namespace Gameplay
{
    public class Structure : MonoBehaviour, IInvoker
    {
        [HideInInspector] public Schemas.StructureSchema Schema;
        
        public Invoker Invoker { get; private set; } = new Invoker();

        public void Initialize(Tile owner, Schemas.StructureSchema schema)
        {
            Schema = schema;
            Invoker.Initialize(owner, Schema);
        }
    }
}
