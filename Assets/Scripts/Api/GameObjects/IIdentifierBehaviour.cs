using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Api.GameObjects
{
    public interface IIdentifierBehaviour : IReadOnlyIdentifierBehaviour
    {
        new IIdentifier Id { get; set; }
    }
}