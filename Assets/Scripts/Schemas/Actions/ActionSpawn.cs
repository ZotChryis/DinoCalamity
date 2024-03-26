using UnityEngine;

namespace Schemas.Actions
{
    [CreateAssetMenu]
    public class ActionSpawn : Action
    {
        [SerializeField] private GameObject m_prefab;

        public override void Invoke(GameObject source)
        {
            Instantiate(m_prefab, source.transform);
        }
    }
}
