namespace Assets.Scripts.Plugins.Features.Controls
{
    public interface IPlayerControlConfiguration : IReadOnlyPlayerControlConfiguration
    {
        new bool MouseMovementEnabled { get; set; }

        new bool KeyboardMovementEnabled { get; set; }

        new bool TileRestrictedMovement { get; set; }

        new bool HoverTileSelection { get; set; }
    }
}