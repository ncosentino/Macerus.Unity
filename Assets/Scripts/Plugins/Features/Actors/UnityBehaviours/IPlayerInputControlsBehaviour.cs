using Assets.Scripts.Api.Scenes.Explore;

namespace Assets.Scripts.Plugins.Features.Actors.UnityBehaviours
{
    public interface IReadOnlyPlayerInputControlsBehaviour
    {
        IKeyboardControls KeyboardControls { get; }
    }

    public interface IPlayerInputControlsBehaviour : IReadOnlyPlayerInputControlsBehaviour
    {
        new IKeyboardControls KeyboardControls { get; set; }
    }
}