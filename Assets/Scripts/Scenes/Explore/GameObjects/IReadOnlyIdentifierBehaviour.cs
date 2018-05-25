using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Scenes.Explore.GameObjects
{
    public interface IReadOnlyIdentifierBehaviour
    {
        IIdentifier Id { get; }
    }
}