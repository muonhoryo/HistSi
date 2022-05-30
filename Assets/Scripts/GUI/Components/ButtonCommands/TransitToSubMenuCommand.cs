
using UnityEngine;

namespace HistSi.GUI.Commands
{
    public class TransitToSubMenuCommand : ButtonCommand
    {
        [SerializeField]
        protected HistSiSubMenu CallingSubMenu;
        public override void CommandRun()
        {
            GUICommandsScripts.UIScripts.TransitToSubMenu(CallingSubMenu);
        }
    }
}
