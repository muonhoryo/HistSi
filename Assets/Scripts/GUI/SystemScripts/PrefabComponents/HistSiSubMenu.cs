
using System.Collections;
using UnityEngine;

namespace HistSi.GUI
{
    public class HistSiSubMenu : MonoBehaviour,IRemovable
    {
        [SerializeField]
        protected HistSiButton[] ChildrenButtons;
        [SerializeField]
        protected byte MenuLayer;
        [SerializeField]
        protected Animation onDestroyAnimation;
        public Animation OnDestroyAnimation => onDestroyAnimation;
        public GameObject DestroyedObject => gameObject;
        public void Remove()
        {
            Removable.Remove(this, delegate
            {
                foreach (HistSiButton button in ChildrenButtons)
                {
                    button.interactable = false;
                }
            });
        }
        protected virtual void Awake()
        {
            SubMenuesManager.AddSubMenu(MenuLayer, this);
            ChildrenButtons = GetComponentsInChildren<HistSiButton>();
        }
        Coroutine IRemovable.StartCoroutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }
    }
}
