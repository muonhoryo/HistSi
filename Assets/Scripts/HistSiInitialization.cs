
using UnityEngine;
using MuonhoryoLibrary;
using HistSi.ValueSources;

namespace HistSi
{
    public class HistSiInitialization : MonoBehaviour,ISingltone<HistSiInitialization>,
        ISerializationCallbackReceiver
    {
        protected static HistSiInitialization singltone;
        public CustomValuesData CustomValues;
        public GameObject UICanvas;
        HistSiInitialization ISingltone<HistSiInitialization>.Singltone
        { get => singltone; set => singltone = value; }
        public void OnAfterDeserialize()
        {
            HistSi.CustomValues = CustomValues;
            HistSi.UICanvas = UICanvas;
        }
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {}
        protected virtual void Awake()
        {
            Singltone.Initialization(this, delegate { Destroy(this); }, delegate
              {
                  HistSi.CustomValues = CustomValues;
                  HistSi.UICanvas = UICanvas;
              });
        }
    }
}
