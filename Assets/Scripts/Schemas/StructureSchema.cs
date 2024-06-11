using System.Collections.Generic;
using Gameplay;
using UnityEngine;

namespace Schemas
{
    [CreateAssetMenu]
    public class StructureSchema : InvokerSchema
    {
        public Structure Prefab;
        
        // Tooltip info.
        public string tooltipName;
        public string tooltipMessage;
        public Sprite tooltipIcon;
        public List<Action> tooltipActions;
    }
}
