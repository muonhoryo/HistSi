
using UnityEngine;

namespace HistSi.CustomEditor
{
    public sealed class PairMathOpSerializationContainer:MonoBehaviour
    {
        [HideInInspector]
        public Component FirstOperand;
        [HideInInspector]
        public Component SecondOperand;
        [HideInInspector]
        public Component Owner;
        private void OnGUI()
        {
            if (Owner == null)
            {
                Destroy(this);
            }
        }
    }
}
