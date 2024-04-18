using Schemas;

namespace Gameplay
{
    public class Calamity : IInvoker
    {
        public CalamitySchema Schema { get; private set; }
        public Invoker Invoker { get; private set; } = new Invoker();
        
        public Calamity(CalamitySchema schema)
        {
            Schema = schema;
            Invoker.Initialize(this, schema);
        }
    }
}
