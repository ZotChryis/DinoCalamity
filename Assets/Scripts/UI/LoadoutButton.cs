using TMPro;
using UnityEngine;

namespace UI
{
    public class LoadoutButton : LoadSceneButton
    {
        public Schemas.LoadoutSchema Schema { get; private set; }

        [SerializeField] private TMP_Text m_name;
        
        public void SetSchema(Schemas.LoadoutSchema schema)
        {
            Schema = schema;
            m_name.SetText(Schema.Name);
        }
        
        protected override void OnButtonClicked()
        {
            ServiceLocator.Instance.LoadoutSchema = Schema;
            base.OnButtonClicked();
        }
    }
}
