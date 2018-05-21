using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Scenes.Explore.Input
{
    public class KeyboardControls : IKeyboardControls
    {
        #region Properties
        /// <inheritdoc />
        public KeyCode MoveLeft { get; } = KeyCode.A;

        /// <inheritdoc />
        public KeyCode MoveRight { get; } = KeyCode.D;

        /// <inheritdoc />
        public KeyCode MoveUp { get; } = KeyCode.W;

        /// <inheritdoc />
        public KeyCode MoveDown { get; } = KeyCode.S;

        /// <inheritdoc />
        public KeyCode Interact { get; } = KeyCode.Space;

        /// <inheritdoc />
        public KeyCode ToggleInventory { get; } = KeyCode.I;

        /// <inheritdoc />
        public KeyCode GuiClose { get; } = KeyCode.Escape;

        #endregion
    }
}
