using Schemas;

namespace Gameplay
{
    public class Vision : IInvoker
    {
        public VisionSchema Schema { get; private set; }
        public Invoker Invoker { get; private set; } = new Invoker();
        
        public Vision(VisionSchema schema)
        {
            Schema = schema;
            Invoker.Initialize(this, schema);
        }
    }
}
