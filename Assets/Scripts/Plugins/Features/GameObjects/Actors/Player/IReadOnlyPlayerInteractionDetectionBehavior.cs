using System.Collections.Generic;

using Macerus.Plugins.Features.Interactions.Api;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.Player
{
    public interface IReadOnlyPlayerInteractionDetectionBehavior
    {
        IReadOnlyCollection<IInteractableBehavior> Interactables { get; }
    }
}
