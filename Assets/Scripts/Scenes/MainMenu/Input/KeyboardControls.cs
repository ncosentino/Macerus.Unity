using UnityEngine;

namespace Assets.Scripts.Scenes.MainMenu.Input
{
    public class KeyboardControls : IKeyboardControls
    {
        #region Properties
        /// <inheritdoc />
        public KeyCode GuiClose { get; } = KeyCode.Escape;

        /// <inheritdoc />
        public KeyCode DebugConsole { get; } = KeyCode.BackQuote;

        #endregion
    }
}
