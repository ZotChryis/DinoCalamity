using UnityEngine;

namespace Schemas
{
    [CreateAssetMenu]
    public class CalamitySchema : InvokerSchema
    {
        public string Name;
        public string Description;
        public VisionSchema[] Visions;
    }
}