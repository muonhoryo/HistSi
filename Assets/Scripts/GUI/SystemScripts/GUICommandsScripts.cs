
using UnityEngine;

namespace HistSi.GUI.Commands
{
    public static class GUICommandsScripts
    {
        public static class UIScripts
        {
            public static HistSiMessage CallMessage(HistSiMessage message)
            {
                return GameObject.Instantiate(message.gameObject, HistSi.UICanvas.transform).
                    GetComponent<HistSiMessage>();
            }
            public static HistSiMessage CallMessage(HistSiMessage message, Vector2 position)
            {
                HistSiMessage calledMessage = CallMessage(message);
                calledMessage.transform.position = position;
                return calledMessage;
            }
            public static void CloseMessage(HistSiMessage message)
            {
                message.Remove();
            }
            public static HistSiSubMenu TransitToSubMenu(HistSiSubMenu subMenu)
            {
                return GameObject.Instantiate(subMenu.gameObject, HistSi.UICanvas.transform).
                    GetComponent<HistSiSubMenu>();
            }
            public static HistSiSubMenu TransitToSubMenu(HistSiSubMenu subMenu, Vector2 position)
            {
                HistSiSubMenu calledSubMenu = TransitToSubMenu(subMenu);
                calledSubMenu.transform.position = position;
                return calledSubMenu;
            }
            public static void CloseSubMenu(HistSiSubMenu subMenu)
            {
                subMenu.Remove();
            }
        }
        public static class MainMenuScripts
        {
            public static void Continue()
            {
                throw new HistSiException("Haven't game to continued");
            }
            public static void NewGame()
            {
                throw new HistSiException("New game cannot be started");
            }
            public static void Exit()
            {
                Application.Quit();
            }
            public static void GoToScene(string sceneName)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            }
        }
    }
}
