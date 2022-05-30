
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HistSi.GUI
{
    public sealed class LockingMessage : HistSiMessage, IButtonLocker
    {
        [SerializeField] 
        private byte lockedLayer = 0;
        public byte LockedLayer => lockedLayer;
        protected sealed override void Awake()
        {
            base.Awake();
            ButtonsLocker.LockButtons(lockedLayer);
        }
        private void OnDestroy()
        {
            ButtonsLocker.UnlockButtons(lockedLayer);
        }
    }
}
