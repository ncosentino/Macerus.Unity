using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IIdentifierBehaviour : IReadOnlyIdentifierBehaviour
    {
        new IIdentifier Id { get; set; }
    }
}