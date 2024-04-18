using Schemas;
using TMPro;
using UnityEngine;

namespace UI
{
    public class VisionView : View
    {
        [SerializeField] private TMP_Text m_title;
        [SerializeField] private TMP_Text m_description;

        public VisionSchema Schema { get; private set; }
        
        public void SetSchema(VisionSchema schema)
        {
            Schema = schema;
            m_title.SetText(Schema.Name);
            m_description.SetText(Schema.Description);
        }
    }
}
