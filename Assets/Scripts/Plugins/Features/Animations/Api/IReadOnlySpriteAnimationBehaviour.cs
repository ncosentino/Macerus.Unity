
using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.Animations.Api
{
    public interface IReadOnlySpriteAnimationBehaviour
    {
        IIdentifier CurrentAnimationId { get; }
    }
}
