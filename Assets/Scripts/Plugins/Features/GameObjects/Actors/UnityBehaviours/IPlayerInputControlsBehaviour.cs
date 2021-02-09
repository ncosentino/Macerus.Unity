using Assets.Scripts.Api.Scenes.Explore;
using Macerus.Api.Behaviors;

namespace Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours
{
    public interface IPlayerInputControlsBehaviour : IReadOnlyPlayerInputControlsBehaviour
    {
        new IKeyboardControls KeyboardControls { get; set; }

        new IMovementBehavior MovementBehavior { get; set; }

        new ProjectXyz.Api.Logging.ILogger Logger { get; set; }
    }
}