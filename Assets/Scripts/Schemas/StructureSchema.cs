using Gameplay;
using UnityEngine;
using UI;

namespace Schemas
{
    [CreateAssetMenu]
    public class StructureSchema : InvokerSchema, ITooltipable
    {
        public Structure Prefab;
        
        
        public TooltipView.TooltipInfo TooltipInfo { get; }
    }
}
