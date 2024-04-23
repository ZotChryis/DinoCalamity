using Gameplay;
using UnityEngine;

namespace Schemas.Checks
{
    [CreateAssetMenu]
    public class CheckRandom : Check
    {
        [Range(0, 100)]
        public int Chance = 50;
        
        public override bool IsValid(Invoker.Context context)
        {
            return Random.Range(0, 101) <= Chance;
        }
    }
}
