using UnityEngine;

namespace Assets.Scripts.Scenes.MainMenu.Input
{
    public interface IKeyboardControls
    {
        #region Properties
        KeyCode GuiClose { get; }

        KeyCode DebugConsole { get; }
        #endregion
    }
}