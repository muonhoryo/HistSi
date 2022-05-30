
using System.Collections.Generic;

namespace HistSi.GUI
{
    public static class SubMenuesManager
    {
        private readonly static Dictionary<byte, HistSiSubMenu> SubMenu = new Dictionary<byte, HistSiSubMenu> { };
        public static void AddSubMenu(byte menuLayer, HistSiSubMenu subMenu)
        {
            if (!SubMenu.ContainsKey(menuLayer))
            {
                SubMenu.Add(menuLayer, null);
            }
            else if (SubMenu[menuLayer] != null)
            {
                SubMenu[menuLayer].Remove();
            }
            SubMenu[menuLayer] = subMenu;
        }
        public static HistSiSubMenu GetSubMenu(byte menuLayer)
        {
            return SubMenu[menuLayer];
        }
    }
}
