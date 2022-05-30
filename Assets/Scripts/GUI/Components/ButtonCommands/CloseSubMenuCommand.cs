

using HistSi;

namespace HistSi.GUI.Commands
{
    public sealed class CloseSubMenuCommand : TransitToSubMenuCommand
    {
        public sealed override void CommandRun()
        {
            GUICommandsScripts.UIScripts.CloseSubMenu(CallingSubMenu);
        }
    }
}
