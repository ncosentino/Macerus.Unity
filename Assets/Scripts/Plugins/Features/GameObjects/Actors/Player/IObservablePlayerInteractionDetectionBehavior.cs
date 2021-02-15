using System;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IObservablePlayerInteractionDetectionBehavior : IReadOnlyPlayerInteractionDetectionBehavior
    {
        event EventHandler<EventArgs> NewInteractable;
    }
}
