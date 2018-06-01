using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Api.GameObjects
{
    public interface IReadOnlyIdentifierBehaviour
    {
        IIdentifier Id { get; }
    }
}