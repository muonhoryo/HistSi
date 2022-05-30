

using UnityEngine;

namespace HistSi.GUI
{
    public sealed class LockableButton : HistSiButton
    {
        [SerializeField]
        private byte lockLayer = 0;
        public byte LockLayer { get => lockLayer; }
        public void ActivateButton()
        {
            interactable = true;
        }
        public void DeactivateButton()
        {
            interactable = false;
        }
        protected override void Awake()
        {
            base.Awake();
            ButtonsLocker.AddButton(this);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            ButtonsLocker.RemoveButton(this);
        }
    }
}
