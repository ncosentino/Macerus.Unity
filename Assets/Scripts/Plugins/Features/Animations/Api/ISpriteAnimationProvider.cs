
using ProjectXyz.Api.Framework;

namespace Assets.Scripts.Plugins.Features.Animations.Api
{
    public interface ISpriteAnimationProvider
    {
        ISpriteAnimation GetAnimationById(IIdentifier animationId);

        bool TryGetAnimationById(
            IIdentifier animationId,
            out ISpriteAnimation spriteAnimation);
    }
}
