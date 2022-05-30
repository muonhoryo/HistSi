


namespace HistSi.GUI.Commands
{
    public sealed class ContinueCommand : ButtonCommand, IFinalCommand
    {
        public sealed override void CommandRun()
        {
            GUICommandsScripts.MainMenuScripts.Continue();
        }
    }
}