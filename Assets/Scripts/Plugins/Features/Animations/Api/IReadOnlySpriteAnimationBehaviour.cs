using Macerus.Api.Behaviors;

namespace Assets.Scripts.Plugins.Features.Animations.Api
{
    public interface IReadOnlySpriteAnimationBehaviour
    {
        IReadOnlyAnimationBehavior AnimationBehavior { get; }

        IReadOnlyDynamicAnimationBehavior DynamicAnimationBehavior { get; }
    }
}
