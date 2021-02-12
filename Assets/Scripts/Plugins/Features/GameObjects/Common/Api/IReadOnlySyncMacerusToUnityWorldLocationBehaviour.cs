using Macerus.Api.Behaviors;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common.Api
{
    public interface IReadOnlySyncMacerusToUnityWorldLocationBehaviour
    {
        IObservableWorldLocationBehavior ObservableWorldLocationBehavior { get; }
    }
}