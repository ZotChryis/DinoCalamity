using Schemas;

namespace Gameplay
{
    public class Calamity : IInvoker
    {
        public Invoker Invoker { get; private set; } = new Invoker();

        private readonly CalamitySchema m_schema;
        
        public Calamity(CalamitySchema schema)
        {
            m_schema = schema;
            Invoker.Initialize(this, schema);
        }
    }
}
