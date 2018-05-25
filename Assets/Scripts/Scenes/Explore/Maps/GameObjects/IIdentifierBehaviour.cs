using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Scenes.Explore.Maps.GameObjects
{
    public interface IIdentifierBehaviour : IReadOnlyIdentifierBehaviour
    {
        new IIdentifier Id { get; set; }
    }
}