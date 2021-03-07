using Assets.Scripts.Input.Api;

using UnityEngine;

namespace Assets.Scripts.Input
{
    public sealed class KeyboardControls : IKeyboardControls
    {
        /// <inheritdoc />
        public KeyCode QuickSlot1 { get; } = KeyCode.Alpha1;

        /// <inheritdoc />
        public KeyCode QuickSlot2 { get; } = KeyCode.Alpha2;

        /// <inheritdoc />
        public KeyCode QuickSlot3 { get; } = KeyCode.Alpha3;

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

        /// <inheritdoc />
        public KeyCode DebugConsole { get; } = KeyCode.BackQuote;

        public KeyCode Summon { get; } = KeyCode.Semicolon;
    }
}
