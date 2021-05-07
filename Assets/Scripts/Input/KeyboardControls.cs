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
        public KeyCode QuickSlot4 { get; } = KeyCode.Alpha4;

        /// <inheritdoc />
        public KeyCode QuickSlot5 { get; } = KeyCode.Alpha5;

        /// <inheritdoc />
        public KeyCode QuickSlot6 { get; } = KeyCode.Alpha6;

        /// <inheritdoc />
        public KeyCode QuickSlot7 { get; } = KeyCode.Alpha7;

        /// <inheritdoc />
        public KeyCode QuickSlot8 { get; } = KeyCode.Alpha8;

        /// <inheritdoc />
        public KeyCode QuickSlot9 { get; } = KeyCode.Alpha9;

        /// <inheritdoc />
        public KeyCode QuickSlot10 { get; } = KeyCode.Alpha0;

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
        public KeyCode ToggleCharacterSheet { get; } = KeyCode.C;

        /// <inheritdoc />
        public KeyCode GuiClose { get; } = KeyCode.Escape;
    }
}
