
using UnityEngine;

namespace HistSi.GUI.Commands
{
    public class CallMessageCommand : ButtonCommand
    {
        [SerializeField]
        protected HistSiMessage CallingMessage;
        public override void CommandRun()
        {
            GUICommandsScripts.UIScripts.CallMessage(CallingMessage);
        }
    }
}
