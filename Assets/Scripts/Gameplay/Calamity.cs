using System.Collections.Generic;
using Schemas;
using UnityEngine;

namespace Gameplay
{
    public class Calamity : IInvoker
    {
        public delegate void OnVision();
        public OnVision OnVisionEvent;
        
        public CalamitySchema Schema { get; private set; }
        public Invoker Invoker { get; private set; } = new Invoker();
        public List<Vision> Visions { get; private set; } = new List<Vision>();

        private List<VisionSchema> m_possibleVisionSchemas = new List<VisionSchema>();
        
        public Calamity(CalamitySchema schema)
        {
            Schema = schema;
            Invoker.Initialize(this, schema);

            m_possibleVisionSchemas.AddRange(Schema.Visions);
        }

        /// <summary>
        /// Attempts to trigger a vision. Returns whether it occurs or not.
        /// </summary>
        /// <returns></returns>
        public bool TryTriggerVision()
        {
            var visionSchema = PopRandomVisionSchema();
            if (visionSchema == null)
            {
                return false;
            }

            Vision vision = new Vision(visionSchema);
            Visions.Add(vision);
            OnVisionEvent?.Invoke();
            return true;
        }
        
        /// <summary>
        /// Returns a random vision for this calamity. Returns null if none are available.
        /// </summary>
        private VisionSchema PopRandomVisionSchema()
        {
            if (m_possibleVisionSchemas.Count == 0)
            {
                return null;
            }
            
            int index = Random.Range(0, m_possibleVisionSchemas.Count);
            var value = m_possibleVisionSchemas[index];
            m_possibleVisionSchemas.RemoveAt(index);
            return value;
        }
    }
}
