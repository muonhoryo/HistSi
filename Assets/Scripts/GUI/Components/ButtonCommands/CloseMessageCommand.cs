


namespace HistSi.GUI.Commands
{
    public sealed class CloseMessageCommand : CallMessageCommand
    {
        public sealed override void CommandRun()
        {
            GUICommandsScripts.UIScripts.CloseMessage(CallingMessage);
        }
    }
}
