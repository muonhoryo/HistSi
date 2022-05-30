
using System.Collections;
using System;
using UnityEngine;

namespace HistSi.GUI
{
    public static class Removable
    {
        /// <summary>
        /// Remove owner without animation
        /// </summary>
        /// <param name="owner"></param>
        public static void Remove(IRemovable owner)
        {
            Remove(owner,() => { });
        }
        /// <summary>
        /// Remove owner. If destroy animation not equal null-remove after an delay equal destroy animation's length.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="beforeAnimStartAction"></param>
        public static void Remove(IRemovable owner,Action beforeAnimStartAction)
        {
            if (owner.OnDestroyAnimation == null)
            {
                GameObject.Destroy(owner.DestroyedObject);
            }
            else
            {
                beforeAnimStartAction();
                owner.OnDestroyAnimation.Play();
                owner.StartCoroutine(DelayDestroy(owner));
            }
        }
        /// <summary>
        /// Destroy destroyedObject after an delay equal destroy animation length.
        /// </summary>
        /// <param name="destroyedObject"></param>
        /// <returns></returns>
        public static IEnumerator DelayDestroy(IRemovable destroyedObject)
        {
            yield return new WaitForSeconds(destroyedObject.OnDestroyAnimation.clip.length);
            GameObject.Destroy(destroyedObject.DestroyedObject);
        }
    }
}
