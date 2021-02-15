using System.Collections.Generic;

using Macerus.Api.Behaviors;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IReadOnlyPlayerInteractionDetectionBehavior
    {
        IReadOnlyCollection<IInteractableBehavior> Interactables { get; }
    }
}
