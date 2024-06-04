using Gameplay;
using UnityEngine;
using UI;

namespace Schemas
{
    [CreateAssetMenu]
    public class StructureSchema : InvokerSchema
    {
        public Structure Prefab;
        public TooltipView.TooltipDescription TooltipInfo;  // TODO: Move this somewhere else.
    }
}
