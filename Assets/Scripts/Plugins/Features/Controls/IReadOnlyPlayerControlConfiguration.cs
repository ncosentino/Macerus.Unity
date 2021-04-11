namespace Assets.Scripts.Plugins.Features.Controls
{
    public interface IReadOnlyPlayerControlConfiguration
    {
        bool MouseMovementEnabled { get; }

        bool KeyboardMovementEnabled { get; }

        bool TileRestrictedMovement { get; }
    }
}