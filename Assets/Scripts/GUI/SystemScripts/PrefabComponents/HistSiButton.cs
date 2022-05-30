
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HistSi.GUI
{
    public class HistSiButton : Selectable, ICommandRuner, IRemovable
    {
        [SerializeField]
        protected Animation onDestroyAnimation;
        public Animation OnDestroyAnimation => onDestroyAnimation;
        public GameObject DestroyedObject => gameObject;
        public void Remove()
        {
            Removable.Remove(this,delegate { interactable = false; });
        }
        public void RunCommand(Commands.ButtonCommand command)
        {
            command.CommandRun();
        }
        public void RunCommandList(ButtonCommandsQueue commandsList)
        {
            commandsList.ExecuteCommands();
        }
        Coroutine IRemovable.StartCoroutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }
    }
}
