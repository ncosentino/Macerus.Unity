
using Assets.Scripts.Console;

using UnityEngine;

namespace Assets.Scripts.Scenes.MainMenu
{
    public sealed class MainMenuSetup : IMainMenuSetup
    {
        public void Setup()
        {
            var consoleObject = new GameObject()
            {
                name = "ConsoleCommands",
            };
            consoleObject.AddComponent<GlobalConsoleCommandsBehaviour>();
        }
    }
}