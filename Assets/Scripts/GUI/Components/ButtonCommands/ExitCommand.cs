


namespace HistSi.GUI.Commands
{
    public sealed class ExitCommand : ButtonCommand,IFinalCommand
    {
        public sealed override void CommandRun()
        {
            GUICommandsScripts.MainMenuScripts.Exit();
        }
    }
}
