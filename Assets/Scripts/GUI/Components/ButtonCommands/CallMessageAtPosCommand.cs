
using UnityEngine;
using HistSi;

namespace HistSi.GUI.Commands
{
    public sealed class CallMessageAtPosCommand : CallMessageCommand
    {
        [SerializeField]
        private Vector2 Position;
        public sealed override void CommandRun()
        {
            GUICommandsScripts.UIScripts.CallMessage(CallingMessage,Position);
        }
    }
}
