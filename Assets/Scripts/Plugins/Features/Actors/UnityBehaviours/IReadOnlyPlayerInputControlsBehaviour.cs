using Assets.Scripts.Api.Scenes.Explore;
using Macerus.Api.Behaviors;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public interface IReadOnlyPlayerInputControlsBehaviour
    {
        IKeyboardControls KeyboardControls { get; }

        IMovementBehavior MovementBehavior { get; }
    }
}