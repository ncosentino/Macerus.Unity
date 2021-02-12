using Assets.Scripts.Unity.Threading;
using Macerus.Api.Behaviors;

namespace Assets.Scripts.Plugins.Features.GameObjects.Common.Api
{
    public interface IReadOnlyUpdateWorldLocatiobBehaviour
    {
        IObservableWorldLocationBehavior ObservableWorldLocationBehavior { get; }
    }
}