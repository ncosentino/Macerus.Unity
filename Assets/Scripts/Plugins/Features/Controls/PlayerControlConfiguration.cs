namespace Assets.Scripts.Plugins.Features.Controls
{
    public sealed class PlayerControlConfiguration : IPlayerControlConfiguration
    {
        public PlayerControlConfiguration()
        {
            MouseMovementEnabled = true;
            KeyboardMovementEnabled = true;
            TileRestrictedMovement = false;
            HoverTileSelection = false;
        }

        public bool MouseMovementEnabled { get; set; }

        public bool KeyboardMovementEnabled { get; set; }

        public bool TileRestrictedMovement { get; set; }

        public bool HoverTileSelection { get; set; }
    }
}