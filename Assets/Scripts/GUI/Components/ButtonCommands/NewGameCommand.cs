


namespace HistSi.GUI.Commands
{
    public sealed class NewGameCommand : ButtonCommand, IFinalCommand
    {
        public sealed override void CommandRun()
        {
            GUICommandsScripts.MainMenuScripts.NewGame();
        }
    }
}
