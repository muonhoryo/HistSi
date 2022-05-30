
using System.Collections;
using UnityEngine;
using HistSi.GUI.Commands;

namespace HistSi.GUI
{
    public sealed class ButtonCommandsQueue : MonoBehaviour,IEnumerable
    {
        [SerializeField]
        private ButtonCommand[] commands;
        public IEnumerator GetEnumerator()
        {
            return commands.GetEnumerator();
        }
        public void ExecuteCommands()
        {
            foreach(var command in commands)
            {
                command.CommandRun();
            }
        }
    }
}
