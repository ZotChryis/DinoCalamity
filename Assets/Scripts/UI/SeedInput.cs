using UnityEngine;
using TMPro;

namespace UI
{
    public class SeedInput : MonoBehaviour
    {
        [SerializeField] private TMP_InputField m_input;

        private void Start()
        {
            m_input.onValueChanged.AddListener(OnChange);
        }

        private void OnChange(string str)
        {
            Debug.Log($"SeedInput: Value changed ({str}).");
        }
    }
}